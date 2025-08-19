# Support Assistant - Quick Development Reference

## 🚀 Project Quick Start

### What is Support Assistant?
Local AI-powered technical support app for Windows using Phi-3 model and RAG architecture.

### Key Technologies
- **Language**: C# (.NET 9)
- **UI Framework**: Avalonia (MVVM)
- **AI Runtime**: ONNX Runtime + DirectML
- **Model**: Microsoft Phi-3-mini (2.4GB)
- **Architecture**: RAG (Retrieval-Augmented Generation)

---

## 📅 Development Timeline

| Phase | Duration | Focus | Status |
|-------|----------|-------|---------|
| **Phase 1** | 4-6 weeks | Core Setup & Knowledge Base | ✅ Planning Complete |
| **Phase 2** | 6-8 weeks | AI Service & RAG Implementation | 🔄 In Progress |
| **Phase 3** | 6-8 weeks | UI/UX & Application Integration | ⏳ Pending |
| **Phase 4** | 8-10 weeks | Tooling & Agentic Capabilities | ⏳ Pending |

**Total Estimated Duration**: 6-8 months

---

## 🎯 Core Features Checklist

### MVP Features (Phases 1-2)
- [x] Local AI model integration (Phi-3)
- [x] Knowledge base processing pipeline
- [x] Vector embedding and search
- [x] Basic RAG implementation
- [ ] Chat interface UI
- [ ] Settings configuration

### Advanced Features (Phases 3-4)
- [ ] Modern Avalonia UI with MVVM
- [ ] Responsive chat interface
- [ ] Tool/function calling framework
- [ ] System modification capabilities
- [ ] Security and permission system
- [ ] Packaging and distribution

---

## 🛠️ Development Commands

### Environment Setup
```bash
# Clone repository
git clone https://github.com/nam20485/support-assistant2.git
cd support-assistant2

# Install .NET 9 SDK (if not installed)
# Download from: https://dotnet.microsoft.com/download/dotnet/9.0

# Restore dependencies
dotnet restore
```

### AI Model Setup
```bash
# Download Phi-3-mini model (2.4GB)
# Place in: assets/models/phi3-mini.onnx
# See docs/AI_SETUP.md for detailed instructions
```

### Build & Run
```bash
# Build project
dotnet build

# Run application
dotnet run

# Run with configuration
dotnet run --configuration Release
```

### Testing
```bash
# Run unit tests
dotnet test

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"
```

---

## 📁 Project Structure

```
support-assistant2/
├── docs/                          # Planning and documentation
│   ├── APP_PLAN.md               # This comprehensive plan
│   ├── ImplementationPlan.txt    # Detailed technical plan
│   ├── ai-new-app-template.md    # Application specification
│   ├── AI_SETUP.md               # Model setup guide
│   └── *.md                      # Additional documentation
├── src/                          # Source code (to be created)
│   ├── SupportAssistant.UI/      # Avalonia UI project
│   ├── SupportAssistant.Core/    # Core logic and services
│   ├── SupportAssistant.AI/      # AI and RAG implementation
│   └── SupportAssistant.Tools/   # Tool system framework
├── assets/                       # Static assets (to be created)
│   ├── models/                   # AI models
│   └── knowledge-base/           # Default knowledge base
├── tests/                        # Unit and integration tests
└── scripts/                     # Build and deployment scripts
```

---

## 🔧 Configuration Quick Reference

### appsettings.json Template
```json
{
  "AI": {
    "UseGpuAcceleration": true,
    "ModelPath": "",
    "MaxTokens": 512,
    "Temperature": 0.7
  },
  "KnowledgeBase": {
    "Path": "./assets/knowledge-base",
    "ChunkSize": 1000,
    "ChunkOverlap": 200
  },
  "UI": {
    "Theme": "Light",
    "StartMinimized": false
  }
}
```

### Model Locations (in priority order)
1. `{AppDirectory}/assets/models/phi3-mini.onnx`
2. `{AppDirectory}/models/phi3-mini.onnx`
3. `%LOCALAPPDATA%/SupportAssistant/models/phi3-mini.onnx`
4. `%USERPROFILE%/.cache/huggingface/hub/phi3-mini.onnx`

---

## 🚨 Common Issues & Solutions

### AI Model Issues
- **Model not found**: Check model path in appsettings.json
- **DirectML errors**: Ensure GPU drivers are updated
- **Memory issues**: Use INT4 quantized model (smaller)

### Build Issues
- **ONNX Runtime errors**: Verify NuGet package versions
- **Avalonia build fails**: Check .NET 9 SDK installation
- **Missing dependencies**: Run `dotnet restore`

### Performance Issues
- **Slow startup**: Model loading optimization needed
- **High memory usage**: Implement model sharing
- **UI freezing**: Ensure async/await patterns

---

## 📊 Performance Targets

| Metric | Target | Current | Status |
|--------|--------|---------|---------|
| Model Loading | <10s | TBD | ⏳ |
| Query Response | <2s | TBD | ⏳ |
| Memory Usage | <4GB | TBD | ⏳ |
| Startup Time | <5s | TBD | ⏳ |

---

## 🔗 Quick Links

### Documentation
- [Complete App Plan](APP_PLAN.md)
- [Implementation Details](docs/ImplementationPlan.txt)
- [AI Setup Guide](docs/AI_SETUP.md)
- [Architecture Overview](docs/ai-new-app-template.md)

### External Resources
- [Avalonia UI Documentation](https://docs.avaloniaui.net/)
- [ONNX Runtime Documentation](https://onnxruntime.ai/docs/)
- [Phi-3 Model on Hugging Face](https://huggingface.co/microsoft/Phi-3-mini-4k-instruct-onnx)
- [DirectML Documentation](https://docs.microsoft.com/en-us/windows/ai/directml/)

### Development Tools
- [Visual Studio 2022](https://visualstudio.microsoft.com/)
- [VS Code with C# Extension](https://code.visualstudio.com/)
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)

---

*Quick Reference • Last Updated: December 2024*