from sentence_transformers import SentenceTransformer, util

def calculate_similarityScore(lesson_content, student_input):
    # SBERT modelini yükle
    model = SentenceTransformer('all-MiniLM-L6-v2')  # Hızlı ve hafif bir model

    # Metinleri vektörlere dönüştür
    lesson_embedding = model.encode(lesson_content, convert_to_tensor=True)
    student_embedding = model.encode(student_input, convert_to_tensor=True)

    # Cosine similarity hesapla
    similarity_score = util.cos_sim(lesson_embedding, student_embedding)

    # Skoru 0-100 arasında normalize et
    score = similarity_score.item() * 100

    return round(score, 2)