# genai-dotnet

Minimal .NET solution for experimenting with text completion and other GenAI examples.

## Projects

- **`function-call/`** — console app demonstrating function calling and weather service integration.
- **`text-completion/`** — console app demonstrating text completion usage with GenAI.
- **`text-completion-Ollama/`** — console app demonstrating text completion using Ollama for local inference.

## Build & Run

Prerequisites: .NET 9 SDK installed.

Build the solution:

```powershell
dotnet build
```

Run individual projects:

```powershell
# Function calling example
dotnet run --project function-call

# Text completion example
dotnet run --project text-completion

# Text completion with Ollama (local inference)
dotnet run --project text-completion-Ollama
```

## Status

This project is a work in progress. Basic sample code and project files are present, but the project is not yet complete. 



