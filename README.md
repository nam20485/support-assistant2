# Support Assistant 🤖

> Local AI-powered technical support for Windows with complete privacy

A free, open-source Windows desktop application that provides intelligent technical assistance using a local Small Language Model (SLM) and Retrieval-Augmented Generation (RAG). All processing happens on your device - no data ever leaves your machine.

## ✨ Key Features

- **🔒 100% Private**: All AI processing occurs locally on your device
- **📱 Offline Ready**: Full functionality without internet connection  
- **🤖 Smart Assistance**: Uses Microsoft Phi-3 model for intelligent responses
- **📚 Knowledge-Based**: RAG architecture ensures accurate, factual answers
- **🔧 System Actions**: Can modify settings with your explicit permission
- **💻 Cross-Platform UI**: Modern Avalonia interface (future macOS/Linux support)

## 🚀 Quick Start

### Prerequisites
- Windows 10/11 (x64 or ARM64)
- .NET 9.0 SDK
- ~4GB available RAM
- ~3GB disk space (for AI model)

### Installation
```bash
# Clone the repository
git clone https://github.com/nam20485/support-assistant2.git
cd support-assistant2

# Download AI model (see docs/AI_SETUP.md for details)
# Place phi3-mini.onnx in assets/models/

# Build and run
dotnet build
dotnet run
```

## 📋 Development Status

This project is currently in the planning and early development phase. The application architecture and implementation plan are complete.

### Current Phase: Phase 1 - Core Setup & Knowledge Base
- [x] Comprehensive planning and documentation
- [x] Architecture design and technology selection
- [x] Development roadmap creation
- [ ] Project scaffolding and initial setup
- [ ] ONNX Runtime integration
- [ ] Knowledge base processing pipeline

## 📚 Documentation

### Quick Access
- **[📋 Complete App Plan](APP_PLAN.md)** - Comprehensive development plan and project overview
- **[⚡ Quick Reference](QUICK_REFERENCE.md)** - Essential commands and quick setup guide
- **[🤖 AI Setup Guide](docs/AI_SETUP.md)** - Model download and configuration

### Detailed Documentation
- **[🏗️ Implementation Plan](docs/ImplementationPlan.txt)** - Phase-by-phase technical implementation
- **[📐 Application Specification](docs/ai-new-app-template.md)** - Complete technical and architectural specs
- **[🏛️ Architecture Analysis](docs/Architecting%20AI%20for%20Open-Source%20Windows%20Applications.md)** - Deep technical analysis
- **[💡 Implementation Tips](docs/ImplementationTips.txt)** - Development best practices

## 🛠️ Technology Stack

| Component | Technology | Purpose |
|-----------|------------|---------|
| **UI Framework** | Avalonia UI | Cross-platform desktop interface |
| **Language** | C# (.NET 9) | Core application development |
| **AI Runtime** | ONNX Runtime + DirectML | Hardware-accelerated AI inference |
| **AI Model** | Microsoft Phi-3-mini | Small language model (2.4GB) |
| **Architecture** | RAG (Retrieval-Augmented Generation) | Accurate, knowledge-based responses |
| **Pattern** | MVVM | Clean separation of concerns |

## 🏗️ Project Architecture

```
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│   Avalonia UI   │    │   RAG Pipeline  │    │  Tool System    │
│   (MVVM)        │◄──►│  (Vector Search)│◄──►│ (Function Call) │
└─────────────────┘    └─────────────────┘    └─────────────────┘
         │                       │                       │
         ▼                       ▼                       ▼
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│  User Interface │    │ Knowledge Base  │    │ System Actions  │
│   Components    │    │   Processing    │    │ (with consent)  │
└─────────────────┘    └─────────────────┘    └─────────────────┘
                                │
                                ▼
                       ┌─────────────────┐
                       │ ONNX Runtime +  │
                       │    DirectML     │
                       │  (Phi-3 Model)  │
                       └─────────────────┘
```

## 🎯 Development Roadmap

### Phase 1: Core Setup & Knowledge Base (4-6 weeks)
- Project scaffolding and structure
- ONNX Runtime and DirectML integration
- Knowledge base processing pipeline
- Local vector storage implementation

### Phase 2: AI Service & RAG Implementation (6-8 weeks)
- Phi-3 model integration
- Complete RAG pipeline
- Performance optimization
- Error handling and reliability

### Phase 3: UI/UX & Application Integration (6-8 weeks)
- Avalonia UI implementation
- Chat interface with MVVM pattern
- Settings and configuration management
- Application packaging

### Phase 4: Tooling & Agentic Capabilities (8-10 weeks)
- Function calling framework
- System interaction tools
- Security and permission system
- Advanced features and polish

**Total Estimated Timeline**: 6-8 months

## 🤝 Contributing

We welcome contributions! This is an open-source project built for the community.

### How to Contribute
1. 🍴 Fork the repository
2. 🌟 Create a feature branch
3. 💻 Make your changes
4. ✅ Add tests if applicable
5. 📤 Submit a pull request

### Areas We Need Help With
- UI/UX design and implementation
- Performance optimization
- Additional tool implementations
- Documentation improvements
- Testing and quality assurance

## 📜 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🔗 Links

- **Project Repository**: https://github.com/nam20485/support-assistant2
- **Documentation**: [docs/](docs/)
- **Issues & Feature Requests**: [GitHub Issues](https://github.com/nam20485/support-assistant2/issues)

## 💡 Philosophy

Support Assistant embodies the principles of:
- **Privacy by Design**: Your data never leaves your device
- **Open Source**: Transparent, auditable, and community-driven
- **Local-First**: Works offline, fast and reliable
- **User Empowerment**: You control what the AI can do to your system

---

**Built with ❤️ for the Windows community**

*Ready to get started? Check out our [Complete App Plan](APP_PLAN.md) or [Quick Reference](QUICK_REFERENCE.md)*