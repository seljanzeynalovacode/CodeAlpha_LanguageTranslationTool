# 🌐 Multi-Language Translation API & Web App

A modern, enterprise-grade translation solution featuring a scalable backend built with **ASP.NET Core (Clean Architecture)** and a responsive, premium **Frontend (Tailwind CSS)** supporting Dark/Light mode switching, text-to-speech, and clipboard utilities.

---

## ✨ Features & Technologies Used

### 🛠️ Backend (.NET Web API)
- **Clean Architecture & SOLID Principles**: Decoupled domain, application, infrastructure, and API layers for maximum maintainability and testability.
- **Dependency Injection & Options Pattern**: Clean configuration management using strongly typed options (`appsettings.json`).
- **Resilient HTTP Communication**: Utilizes `HttpClientFactory` for efficient API consumption and memory management.
- **Global Error Handling**: Custom middleware capturing exceptions gracefully with detailed error responses.
- **CORS Support**: Pre-configured cross-origin resource sharing for seamless frontend-backend communication.
- **Interactive API Documentation**: Embedded Swagger UI for real-time endpoint testing and schema inspection.

### 🎨 Frontend (HTML / CSS / JavaScript)
- **Modern B2B UI/UX**: Designed with **Tailwind CSS** featuring Glassmorphism effects and soft gradients.
- **Dark & Light Mode**: Fully integrated theme switcher with user preference persistence using `localStorage`.
- **Enhanced Usability**: 
  - Instant text translation via .NET API integration.
  - One-click **Clipboard Copy** functionality.
  - **Text-to-Speech (TTS)** feature using the Web Speech API.
  - Native browser **Spellcheck** support (`spellcheck="true"`).
  - Responsive layout optimized for all screen sizes.

---

## 🛠️ Tech Stack

- **Backend**: .NET 8 / .NET 9 Web API, C#, Clean Architecture
- **Frontend**: HTML5, JavaScript (ES6+), Tailwind CSS, FontAwesome
- **API Provider**: Google Translate (RapidAPI)
- **Documentation**: Swagger / OpenAPI

---

## 🏗️ Project Structure

```text
TranslationTool/
├── Backend/
│   ├── TranslationTool.Domain/        # Core business models & entities
│   ├── TranslationTool.Application/   # Interfaces, DTOs & service definitions
│   ├── TranslationTool.Infrastructure/# External API provider integrations & HttpClient setup
│   └── TranslationTool.WebAPI/        # Controllers, Middlewares, DI configuration & AppSettings
└── Frontend/
    ├── index.html                     # Premium B2B User Interface (Dark/Light mode support)
    ├── style.css                      # Custom styles, animations & custom scrollbar
    └── script.js                      # API integration, Text-to-Speech & Clipboard logic
```

## 🚀 Getting Started

### Prerequisites

- .NET SDK (Version 8.0 or higher)
- A RapidAPI account with an active key for **Top Google Translate**.
- Live Server (or any static local server) to run the frontend.

### Installation & Setup

1. **Clone the repository:**

```bash
   git clone https://github.com/seljanzeynalovacode/CodeAlpha_LanguageTranslationTool
   cd CodeAlpha_LanguageTranslationTool
```

2. **Configure Backend Environment Settings:**

   Navigate to the backend configuration and update your `appsettings.json` or `appsettings.Development.json` with your RapidAPI credentials:

```json
   {
     "GoogleTranslateOptions": {
       "ApiKey": "YOUR_RAPIDAPI_KEY_HERE",
       "ApiHost": "top-google-translate.p.rapidapi.com"
     }
   }
```

3. **Run the Backend (.NET Web API):**

```bash
   cd Backend/TranslationTool/TranslationTool.WebAPI
   dotnet run
```

   *(The API will run on `https://localhost:7104`)*

4. **Run the Frontend:**

   Open the `Frontend/index.html` file using Live Server in VS Code. Make sure the API base URL in `script.js` matches your running backend port (`https://localhost:7104/api/Translation`).

## 📌 API Endpoints

### Post Translation Request

- **Endpoint:** `POST /api/Translation`
- **Content-Type:** `application/json`

#### Request Body

```json
{
  "text": "salam necesen",
  "sourceLanguage": "az",
  "targetLanguage": "en"
}
```

#### Success Response (`200 OK`)

```json
{
  "translatedText": "Hello how are you",
  "sourceLanguage": "az",
  "targetLanguage": "en"
}
```
