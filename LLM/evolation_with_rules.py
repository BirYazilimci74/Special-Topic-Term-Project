import json
from sentence_transformers import SentenceTransformer, util

def load_rules(input_file="rules.json"):
    """Kuralları JSON dosyasından yükle"""
    try:
        with open(input_file, "r") as file:
            rules = json.load(file)
        return rules
    except FileNotFoundError:
        print("Kurallar dosyası bulunamadı.")
        return {}

def evaluate_student_input(sectionId, student_input, rules_file="rules.json"):
    # 1. Kuralları yükle
    rules = load_rules(rules_file)
    if sectionId not in rules:
        print(f"'{sectionId}' için kurallar bulunamadı.")
        return 0.0

    section_rules = rules[f"{sectionId}"]

    # 2. Sentence Transformers modeli
    similarity_model = SentenceTransformer('all-MiniLM-L6-v2')

    # 3. Öğrenci girdisini ve kuralları embedding'e çevir
    student_embedding = similarity_model.encode(student_input, convert_to_tensor=True)
    rule_embeddings = similarity_model.encode(section_rules, convert_to_tensor=True)

    # 4. Benzerlik skorlarını hesapla
    scores = util.cos_sim(student_embedding, rule_embeddings).max(dim=1)[0]
    
    # 5. Skorların ortalaması
    average_score = scores.mean().item() * 100  # Yüzdelik skala

    # 6. Öğrenci metninin uzunluğuna göre ceza ekle
    # Kısa metinlere ceza
    if len(student_input.split()) < 10:  # Kısa metin için limit
        average_score *= 0.5  # Puanı yarıya indir

    # 7. Fazla bilgi için ceza ekle
    # Eğer metin uzunluğu kurallardan fazla ise, ceza uygula
    rule_terms = sum([len(rule.split()) for rule in section_rules])
    student_terms = len(student_input.split())
    
    if student_terms > rule_terms * 1.5:  # Eğer öğrenci metni 1.5 kat uzun ise
        average_score -= 10  # Puanı 10 puan düşür
    
    # Skoru %100'ü geçmeyecek şekilde sınırlamak
    average_score = min(average_score, 100)
    return average_score

## Ana fonksiyon
#if __name__ == "__main__":
#    # Örnek öğrenci girdisi
#    student_input = "SQL is used for querying and manipulating data,there are four operations, they are select,delete,insert,update. delete is used for removing data,"
#    section_name = "SQL Basics"  # Bu kısımda hangi bölümü değerlendireceğimizi belirt
#    score = evaluate_student_input(section_name, student_input)
#    print(f"'{section_name}' için değerlendirme sonucu: {score:.2f}%")

## Ana fonksiyon
#if __name__ == "__main__":
#    # Örnek öğrenci girdisi
#    student_input = "SQL is used for querying and manipulating data,there are four operations, they are select,delete,insert,update. SELECT is used for retrieving data, delete is used for removing data,"
#    section_name = "SQL Basics"  # Bu kısımda hangi bölümü değerlendireceğimizi belirt
#    score = evaluate_student_input(section_name, student_input)
#    print(f"'{section_name}' için değerlendirme sonucu: {score:.2f}%")
