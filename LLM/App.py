from flask import Flask, request, jsonify
from transformers import pipeline

app = Flask(__name__)

# Question Answering modeli yükleniyor
qa_model = pipeline("question-answering", model="distilbert-base-cased-distilled-squad")

@app.route('/api/answer', methods=['POST'])
def provide_answer():
    try:
        # ASP.NET API'den gelen JSON verilerini al
        data = request.get_json()

        question = data.get('question')
        section_info = data.get('sectionInfo')

        if not question or not section_info:
            return jsonify({"success": False, "message": "Invalid input data."}), 400

        # QA modelini kullanarak soruyu yanıtla
        result = qa_model(question=question, context=section_info)
        answer = result['answer']

        return jsonify({"success": True, "answer": answer})

    except Exception as e:
        return jsonify({"success": False, "message": str(e)}), 500

if __name__ == '__main__':
    app.run(debug=True)
