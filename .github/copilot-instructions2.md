# Support Assistant 2 - AI-Powered Technical Support Application

**ALWAYS follow these instructions first and fallback to additional search and context gathering only when the information here is incomplete or found to be in error.**

## Project Overview

Support Assistant 2 is a Windows desktop application that provides AI-powered technical support using a local Small Language Model (SLM). The application uses:

- **AI Model**: Microsoft Phi-3-mini-4k-instruct (MIT License, ~2.4GB INT4 quantized)
- **Architecture**: Retrieval-Augmented Generation (RAG) with local knowledge base
- **Framework**: C# with Avalonia UI for cross-platform desktop applications
- **Runtime**: ONNX Runtime with DirectML execution provider for hardware acceleration
- **Target Platform**: Windows 10/11 (with future cross-platform support)

## Working Effectively

### Repository Setup
Bootstrap the development environment:
```bash
# Linux/WSL
./scripts/setup-environment.sh
# NEVER CANCEL: Setup takes 15-20 minutes. Set timeout to 30+ minutes.

# Windows
pwsh -NoProfile -ExecutionPolicy Bypass -File scripts/setup-environment.ps1
# NEVER CANCEL: Setup takes 15-20 minutes. Set timeout to 30+ minutes.
```

The setup script installs:
- .NET 9.0 SDK (required per global.json)
- Node.js 22.18.0 (exact version pinned in .nvmrc)
- PowerShell, GitHub CLI, Terraform, Ansible
- Global npm tools: firebase-tools, @angular/cli, typescript, eslint, prettier

### .NET Development Commands
```bash
# Restore packages (required first step)
dotnet restore
# Takes 5-10 seconds typically

# Build the application
dotnet build
# NEVER CANCEL: Initial build takes 30-60 seconds. Set timeout to 120+ seconds.

# Run the application (when source code exists)
dotnet run --project SupportAssistant2
# Takes 5-10 seconds to start

# Run tests (when tests exist)
dotnet test
# NEVER CANCEL: Test suite may take 2-5 minutes. Set timeout to 10+ minutes.

# Format code
dotnet format --verify-no-changes
# Takes 10-20 seconds
```

### Project Structure (When Source Code Exists)
```
src/
├── SupportAssistant2/                  # Main application project
│   ├── Models/                         # AI models and data models
│   ├── Services/                       # AI inference, RAG, and business services
│   ├── ViewModels/                     # Avalonia MVVM ViewModels
│   ├── Views/                          # Avalonia UI Views
│   ├── Assets/                         # Static assets including AI models
│   └── appsettings.json               # Configuration
├── SupportAssistant2.Core/            # Core business logic
├── SupportAssistant2.AI/              # AI and ONNX Runtime services
└── SupportAssistant2.Tests/           # Unit and integration tests
```

### Key Dependencies (To Be Added When Creating Projects)
- `Microsoft.ML.OnnxRuntime.DirectML` - GPU-accelerated AI inference
- `Avalonia` - Cross-platform UI framework
- `Avalonia.Themes.Fluent` - Modern UI theme
- `Microsoft.Extensions.Hosting` - Dependency injection and configuration
- `Microsoft.Extensions.Logging` - Logging framework

## Validation and Testing

### Manual Testing Requirements
**ALWAYS run through these scenarios after making changes:**

1. **Application Startup**:
   - Application starts without errors
   - Main window displays correctly
   - AI service initializes (may show "Mock Mode" if no model found)

2. **Core Chat Functionality**:
   - Enter a test query: "How do I update Windows?"
   - Verify response is generated (mock or real)
   - Check UI remains responsive during inference

3. **AI Model Loading** (if models are available):
   - Verify model loads from configured path
   - Test actual AI inference with technical support query
   - Confirm DirectML acceleration works (check logs)

4. **Configuration**:
   - Modify `appsettings.json` AI settings
   - Restart application and verify settings applied
   - Test with different ModelPath configurations

### Pre-commit Validation
ALWAYS run these commands before committing:
```bash
dotnet restore                          # 5-10 seconds
dotnet build                           # 30-60 seconds
dotnet test                            # 2-5 minutes - NEVER CANCEL
dotnet format --verify-no-changes      # 10-20 seconds
```

### Common CI Validation Steps
The GitHub Actions workflows (.github/workflows/) validate:
- Setup scripts work on Linux and Windows
- Environment setup completes successfully
- Basic .NET project creation and build

## Common Tasks and Tips

### Creating the Initial Project Structure
When starting development:
```bash
# Create solution
dotnet new sln -n SupportAssistant2

# Create main application (when Avalonia templates available)
dotnet new avalonia.mvvm -n SupportAssistant2 -o src/SupportAssistant2

# Create class libraries
dotnet new classlib -n SupportAssistant2.Core -o src/SupportAssistant2.Core
dotnet new classlib -n SupportAssistant2.AI -o src/SupportAssistant2.AI
dotnet new xunit -n SupportAssistant2.Tests -o src/SupportAssistant2.Tests

# Add projects to solution
dotnet sln add src/SupportAssistant2/SupportAssistant2.csproj
dotnet sln add src/SupportAssistant2.Core/SupportAssistant2.Core.csproj
dotnet sln add src/SupportAssistant2.AI/SupportAssistant2.AI.csproj
dotnet sln add src/SupportAssistant2.Tests/SupportAssistant2.Tests.csproj
```

### AI Model Setup
1. Download Phi-3-mini ONNX model (~2.4GB):
   ```
   https://huggingface.co/microsoft/Phi-3-mini-4k-instruct-onnx/resolve/main/cpu_and_mobile/cpu-int4-rtn-block-32-acc-level-4/phi3-mini-4k-instruct-cpu-int4-rtn-block-32-acc-level-4.onnx
   ```

2. Place in: `src/SupportAssistant2/Assets/models/phi3-mini.onnx`

3. Configure in `appsettings.json`:
   ```json
   {
     "AI": {
       "UseGpuAcceleration": true,
       "ModelPath": "",
       "MaxTokens": 512,
       "Temperature": 0.7
     }
   }
   ```

### Implementation Architecture
- **RAG Pipeline**: Local knowledge base → Vector search → Context augmentation → AI inference
- **UI Pattern**: MVVM with reactive commands for async AI operations
- **AI Service**: Singleton service managing ONNX Runtime session with DirectML provider
- **Error Handling**: Graceful fallback to mock mode when AI models unavailable

### Troubleshooting
- **Build Issues**: Ensure .NET 9.0 SDK installed per global.json
- **AI Issues**: Check docs/AI_SETUP.md for DirectML troubleshooting
- **Performance**: Use GPU acceleration when available, INT4 quantized models for speed
- **Dependencies**: Run setup scripts if tools are missing

## Development Environment Notes
- Repository is currently in planning/documentation phase
- No source code exists yet - create following the structure above
- Setup scripts prepare environment for development
- Use docs/ folder for implementation guidance and planning
- Follow docs/ai-new-app-template.md for detailed application specification

## Additional AI Instruction Module Files

(They are not references.)
The links below are part of a modular instruction system.
Each instruction module file contains additional instructions focused on the topic specified by its title.
Any (uncommented) file linked below should be read in fully and considered a part of the complete instruction set.

[ai-quick-reference.md](../ai_instruction_modules/ai-quick-reference.md)
[ai-terminal-management.md](../ai_instruction_modules/ai-terminal-management.md)
[ai-workflow-config.md](../ai_instruction_modules/ai-workflow-config.md)
[ai-deployment-environment.md](../ai_instruction_modules/ai-deployment-environment.md)
[ai-local-environment.md](../ai_instruction_modules/ai-local-environment.md)
[ai-testing-validation.md](../ai_instruction_modules/ai-testing-validation.md)
[ai-tools-config.md](../ai_instruction_modules/ai-tools-config.md)
[ai-task-based-workflow.md](../ai_instruction_modules/ai-task-based-workflow.md)
[ai-workflow-roles.md](../ai_instruction_modules/ai-workflow-roles.md)
[ai-workflow-assignments.md](../ai_instruction_modules/ai-workflow-assignments.md)
[ai-application-guidelines.md](../ai_instruction_modules/ai-application-guidelines.md)
[ai-new-app-template.md](../ai_instruction_modules/ai-new-app-template.md)
[ai-design-principles.md](../ai_instruction_modules/ai-design-principles.md)
[ai-instructions-aspnet-abp.md](../ai_instruction_modules/ai-instructions-aspnet-abp.md)
<!--
[ai-retrospective-evolving-memory.md](../ai_instruction_modules/ai-retrospective-evolving-memory.md)
[ai-deployment-process.md](/ai_instruction_modules/ai-deployment-process.md)
[ai-current-task.md](../ai_instruction_modules/ai-current-task.md)
 -->
