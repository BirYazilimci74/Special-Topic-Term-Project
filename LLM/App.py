from flask import Flask, request, jsonify

from question_answering import generateAnswer
from similarity import calculate_similarityScore

app = Flask(__name__)




# REST API for QA
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
        answer = generateAnswer(question=question,section_info=section_info)

        return jsonify({"success": True, "answer": answer})

    except Exception as e:
        return jsonify({"success": False, "message": str(e)}), 500


# REST API for TS
import traceback

@app.route('/api/similarityScore', methods=['POST'])
def calculate_similarity():
    try:
        # ASP.NET API'den gelen JSON verilerini al
        data = request.get_json()

        studentInput = data.get('studentInput')
        sectionInfo = data.get('sectionInfo')

        if not studentInput or not sectionInfo:
            return jsonify({"success": False, "message": "Invalid input data."}), 400

        # Modeli Kullanarak benzerliği hesapla
        score = calculate_similarityScore(sectionInfo,studentInput)
        score = round(score, 2)

        return jsonify({"success": True, "similarityScore": score})

    except Exception as e:
        print("Error occurred:", traceback.format_exc())  # Hata detaylarını loglayın
        return jsonify({"success": False, "message": str(e)}), 500



if __name__ == '__main__':
    app.run(debug=True)
