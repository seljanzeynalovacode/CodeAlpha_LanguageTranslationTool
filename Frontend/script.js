// script.js

async function translateText() {
    const text = document.getElementById("inputText").value.trim();
    const source = document.getElementById("sourceLang").value;
    const target = document.getElementById("targetLang").value;
    const outputBox = document.getElementById("outputText");
    const loading = document.getElementById("loading");
    const btn = document.getElementById("translateBtn");

    if (!text) return;

    // Show the loading indicator while the request is in progress.
    loading.style.display = "flex";
    btn.disabled = true;
    btn.classList.add("opacity-75", "cursor-not-allowed");

    try {
        // API endpoint
        const API_URL = 'https://localhost:7104/api/Translation'; 
        
        // Send the request in the format expected by the .NET API.
        const response = await fetch(API_URL, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                Text: text,
                SourceLanguage: source,
                TargetLanguage: target
            })
        });

        if (!response.ok) {
            throw new Error("The API request failed with status: " + response.status);
        }

        const data = await response.json();
        
        // .NET serializes the response property as translatedText.
        outputBox.value = data.translatedText || "No translation was returned.";

    } catch (error) {
        console.error("Translation error:", error);
        outputBox.value = "Translation failed. Please make sure the API is running and try again.";
    } finally {
        // Restore the button and loading indicator.
        loading.style.display = "none";
        btn.disabled = false;
        btn.classList.remove("opacity-75", "cursor-not-allowed");
    }
}

function copyText() {
    const text = document.getElementById("outputText").value;
    if(!text) return;
    navigator.clipboard.writeText(text).then(() => {
        alert("Translation copied!");
    });
}

function speakText() {
    const text = document.getElementById("outputText").value;
    const targetLang = document.getElementById("targetLang").value;
    if(!text) return;
    const utterance = new SpeechSynthesisUtterance(text);
    utterance.lang = targetLang;
    window.speechSynthesis.speak(utterance);
}

function clearText() {
    document.getElementById("inputText").value = "";
    document.getElementById("outputText").value = "";
    document.getElementById("inputText").focus();
}

// === DARK / LIGHT MODE LOGIC ===
function toggleTheme() {
    const html = document.documentElement;
    const themeIcon = document.getElementById('themeIcon');
    
    if (html.classList.contains('dark')) {
        // Switch to light mode.
        html.classList.remove('dark');
        localStorage.setItem('theme', 'light');
        themeIcon.classList.replace('fa-sun', 'fa-moon');
    } else {
        // Switch to dark mode.
        html.classList.add('dark');
        localStorage.setItem('theme', 'dark');
        themeIcon.classList.replace('fa-moon', 'fa-sun');
    }
}

// Restore the saved theme when the page loads.
window.addEventListener('DOMContentLoaded', () => {
    const savedTheme = localStorage.getItem('theme');
    const html = document.documentElement;
    const themeIcon = document.getElementById('themeIcon');

    // Use the saved preference, or the system preference when none is saved.
    if (savedTheme === 'dark' || (!savedTheme && window.matchMedia('(prefers-color-scheme: dark)').matches)) {
        html.classList.add('dark');
        if (themeIcon) {
            themeIcon.classList.replace('fa-moon', 'fa-sun');
        }
    }
});
