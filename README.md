# 🌐 Multi-Language Translation API Service

A modern, scalable backend service built with **ASP.NET Core (Clean Architecture)** that provides seamless multi-language translation using external API providers. Designed with developer-friendly patterns, robust global error handling, and ready for frontend integration.

---

## ✨ Features

- **Clean Architecture & SOLID Principles**: Decoupled domain, application, infrastructure, and API layers for maximum maintainability.
- **Dependency Injection & Options Pattern**: Clean separation of configurations using strongly typed options (`appsettings.json`).
- **Resilient HTTP Communication**: Utilizes `HttpClientFactory` for efficient API consumption and memory management.
- **Global Error Handling**: Custom middleware capturing exceptions gracefully with detailed debugging outputs in development.
- **CORS Support**: Pre-configured to allow smooth cross-origin integration with frontend applications (e.g., Live Server).
- **Interactive API Documentation**: Embedded Swagger UI for real-time endpoint testing and schema inspection.

---

## 🛠️ Tech Stack

- **Framework**: .NET 8 / .NET 9 Web API
- **Language**: C#
- **Architecture**: Clean Architecture
- **API Provider**: Top Google Translate (RapidAPI)
- **Documentation**: Swagger / OpenAPI
- **Serialization**: System.Text.Json

---

## 🏗️ Project Structure

```text
TranslationTool/
├── TranslationTool.Domain/        # Core business models & entities
├── TranslationTool.Application/   # Interfaces, DTOs & service definitions
├── TranslationTool.Infrastructure/# External API provider integrations & HttpClient setup
└── TranslationTool.WebAPI/        # Controllers, Middlewares, DI configuration & AppSettings
````
## 🚀 Getting Started

### Prerequisites

- .NET SDK (Version 8.0 or higher)
- A RapidAPI account with an active key for **Top Google Translate**.

### Installation & Setup

1. **Clone the repository:**

```bash
   git clone https://github.com/seljanzeynalovacode/CodeAlpha_LanguageTranslationTool.git
   cd CodeAlpha_LanguageTranslationTool/Backend/TranslationTool
```

2. **Configure Environment Settings:**

   Update your `appsettings.json` or `appsettings.Development.json` with your RapidAPI credentials:

```json
   {
     "GoogleTranslateOptions": {
       "ApiKey": "YOUR_RAPIDAPI_KEY_HERE",
       "ApiHost": "top-google-translate.p.rapidapi.com"
     }
   }
```

3. **Build the Project:**

```bash
   dotnet build
```

4. **Run the Application:**

```bash
   dotnet run --project TranslationTool.WebAPI
```

5. **Access Swagger UI:**

   Open your browser and navigate to:

```
   https://localhost:7084/swagger
```

## 📌 API Endpoints

### Post Translation Request

- **Endpoint:** `POST /api/translation`
- **Content-Type:** `application/json`

#### Request Body

```json
{
  "text": "salam necesen men backend developer kimi isleyirem",
  "sourceLanguage": "az",
  "targetLanguage": "en"
}
```

#### Success Response (`200 OK`)

```json
{
  "translatedText": "Hello how are you I work as a backend developer",
  "sourceLanguage": "az",
  "targetLanguage": "en"
}
```

## 🗺️ Roadmap & Upcoming Features

- [x] **Backend (.NET Web API)** — Completed
  - [x] Clean Architecture setup
  - [x] Integration with Google Translate RapidAPI
  - [x] Custom exception middleware & DTO validation
  - [x] CORS configuration for frontend clients
- [ ] **Frontend Application** — In Progress
  - [ ] Interactive User Interface for text input & selection
  - [ ] Real-time translation input debounce
  - [ ] Language selection dropdowns with auto-detection options
