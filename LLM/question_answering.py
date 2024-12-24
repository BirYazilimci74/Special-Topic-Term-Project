from transformers import pipeline
from sentence_transformers import SentenceTransformer, util


# Question Answering Model is loading.
qa_model = pipeline("question-answering", model="distilbert-base-cased-distilled-squad")

def generateAnswer(question,section_info):
    result = qa_model(question=question, context=section_info)
    answer = result['answer']
    return answer