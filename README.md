# 📄 DocuSummarizer

WPF 기반의 이미지 문서 스캔, OCR(문자 인식), 자동 요약 기능을 제공하는 스마트 문서 요약 툴입니다.

---

## 🖼️ 소개

**DocuSummarizer**는 스캔 이미지나 사진으로 된 문서를 자동으로 분석하고,  
텍스트를 추출하여 요약해주는 데스크탑 응용 프로그램입니다.  
보고서 정리, 공부 자료 요약, 문서 보관 자동화 등에 사용할 수 있습니다.

---

## ✨ 주요 기능

- ✅ 이미지 파일 드래그 앤 드롭으로 업로드
- ✅ 이미지 전처리 (흑백화, 기울기 보정, 노이즈 제거 등)
- ✅ OCR 문자 인식 (Tesseract 엔진)
- ✅ 요약 기능 (키워드 추출 또는 문장 요약 선택 가능)
- ✅ 결과 미리보기 및 Markdown 저장/복사

---

## 🛠 기술 스택

| 범주 | 사용 기술 |
|------|------------|
| 프레임워크 | [.NET 6+](https://dotnet.microsoft.com/) |
| UI | WPF (MVVM 패턴 적용) |
| OCR 엔진 | [Tesseract 4.x](https://github.com/tesseract-ocr/tesseract) |
| 이미지 처리 | [OpenCvSharp4](https://github.com/shimat/opencvsharp) |
| 요약 모델 | ONNX Runtime + HuggingFace BART/T5 |
| 텍스트 뷰어 | FlowDocument / Markdown Viewer |

---

## 📂 폴더 구조


---

## 🚀 실행 방법

