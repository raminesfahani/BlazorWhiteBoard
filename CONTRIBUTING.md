# Contributing to BlazorWhiteBoard

First off, thank you for considering contributing to BlazorWhiteBoard! It's people like you that make this project such a great tool.

## Table of Contents

- [Code of Conduct](#code-of-conduct)
- [How Can I Contribute?](#how-can-i-contribute)
  - [Reporting Bugs](#reporting-bugs)
  - [Suggesting Enhancements](#suggesting-enhancements)
  - [Pull Requests](#pull-requests)
- [Development Setup](#development-setup)
- [Style Guidelines](#style-guidelines)
  - [Git Commit Messages](#git-commit-messages)
  - [C# Style Guide](#c-style-guide)
  - [Documentation Style Guide](#documentation-style-guide)
- [Project Structure](#project-structure)

## Code of Conduct

This project and everyone participating in it is governed by our commitment to providing a welcoming and inspiring community for all. Please be respectful and constructive in your interactions.

### Our Standards

- Using welcoming and inclusive language
- Being respectful of differing viewpoints and experiences
- Gracefully accepting constructive criticism
- Focusing on what is best for the community
- Showing empathy towards other community members

## How Can I Contribute?

### Reporting Bugs

Before creating bug reports, please check the existing issues to avoid duplicates. When you create a bug report, include as many details as possible:

**Bug Report Template:**

```markdown
**Describe the bug**
A clear and concise description of what the bug is.

**To Reproduce**
Steps to reproduce the behavior:
1. Go to '...'
2. Click on '...'
3. Scroll down to '...'
4. See error

**Expected behavior**
A clear description of what you expected to happen.

**Screenshots**
If applicable, add screenshots to help explain your problem.

**Environment:**
 - OS: [e.g. Windows 11, macOS 14, Ubuntu 22.04]
 - Browser: [e.g. Chrome 120, Firefox 121]
 - .NET Version: [e.g. .NET 10.0]

**Additional context**
Add any other context about the problem here.
```

### Suggesting Enhancements

Enhancement suggestions are tracked as GitHub issues. When creating an enhancement suggestion, include:

**Enhancement Template:**

```markdown
**Is your feature request related to a problem?**
A clear description of what the problem is.

**Describe the solution you'd like**
A clear description of what you want to happen.

**Describe alternatives you've considered**
Any alternative solutions or features you've considered.

**Additional context**
Add any other context or screenshots about the feature request.
```

### Pull Requests

1. **Fork the repository** and create your branch from `main`
2. **Make your changes** following the style guidelines
3. **Add tests** if you've added code that should be tested
4. **Update documentation** if you've changed APIs or added features
5. **Ensure the test suite passes** by running `dotnet test`
6. **Run the build** with `dotnet build` to check for compilation errors
7. **Create a pull request** with a clear title and description

**Pull Request Template:**

```markdown
## Description
Brief description of the changes

## Type of Change
- [ ] Bug fix (non-breaking change which fixes an issue)
- [ ] New feature (non-breaking change which adds functionality)
- [ ] Breaking change (fix or feature that would cause existing functionality to not work as expected)
- [ ] Documentation update

## How Has This Been Tested?
Describe the tests you ran to verify your changes.

## Checklist
- [ ] My code follows the style guidelines of this project
- [ ] I have performed a self-review of my code
- [ ] I have commented my code, particularly in hard-to-understand areas
- [ ] I have made corresponding changes to the documentation
- [ ] My changes generate no new warnings
- [ ] I have added tests that prove my fix is effective or that my feature works
- [ ] New and existing unit tests pass locally with my changes
```

## Development Setup

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/)

### Local Setup

1. **Clone your fork:**
   ```bash
   git clone https://github.com/YOUR-USERNAME/BlazorWhiteBoard.git
   cd BlazorWhiteBoard
   ```

2. **Add upstream remote:**
   ```bash
   git remote add upstream https://github.com/ORIGINAL-OWNER/BlazorWhiteBoard.git
   ```

3. **Restore dependencies:**
   ```bash
   dotnet restore
   ```

4. **Build the project:**
   ```bash
   dotnet build
   ```

5. **Run the application:**
   ```bash
   dotnet run
   ```

6. **Run tests:**
   ```bash
   dotnet test
   ```

### Keeping Your Fork Updated

```bash
git fetch upstream
git checkout main
git merge upstream/main
```

## Style Guidelines

### Git Commit Messages

- Use the present tense ("Add feature" not "Added feature")
- Use the imperative mood ("Move cursor to..." not "Moves cursor to...")
- Limit the first line to 72 characters or less
- Reference issues and pull requests liberally after the first line

**Commit Message Format:**

```
<type>: <subject>

<body>

<footer>
```

**Types:**
- `feat`: A new feature
- `fix`: A bug fix
- `docs`: Documentation only changes
- `style`: Code style changes (formatting, missing semicolons, etc.)
- `refactor`: Code refactoring without changing functionality
- `perf`: Performance improvements
- `test`: Adding or modifying tests
- `chore`: Changes to build process or auxiliary tools

**Example:**

```
feat: Add support for text annotations

- Implement text tool in drawing toolbar
- Add text input modal for canvas annotations
- Update SignalR hub to broadcast text data
- Add tests for text rendering

Closes #123
```

### C# Style Guide

This project follows the [Microsoft C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions).

**Key Points:**

- Use 4 spaces for indentation (no tabs)
- Use PascalCase for class names, method names, and public members
- Use camelCase for local variables and private fields
- Prefix private fields with underscore: `_fieldName`
- Use `var` when the type is obvious from the right side
- Use XML documentation comments for public APIs
- Keep methods focused and small (ideally < 20 lines)
- Follow SOLID principles

**Example:**

```csharp
namespace BlazorWhiteBoard.Application.Services;

/// <summary>
/// Provides canvas drawing operations through JavaScript interop.
/// </summary>
public sealed class CanvasInteropService : ICanvasInteropService
{
    private readonly IJSRuntime _jsRuntime;

    public CanvasInteropService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime ?? throw new ArgumentNullException(nameof(jsRuntime));
    }

    /// <inheritdoc/>
    public async Task DrawLineAsync(
        ElementReference canvas,
        double x1, double y1,
        double x2, double y2,
        string color,
        int lineWidth)
    {
        await _jsRuntime.InvokeVoidAsync(
            "whiteboardCanvas.drawLine",
            canvas, x1, y1, x2, y2, color, lineWidth);
    }
}
```

### Documentation Style Guide

- Use clear, concise language
- Include code examples where helpful
- Use proper markdown formatting
- Keep line length reasonable (80-100 characters for prose)
- Use XML documentation for all public APIs

**XML Documentation Example:**

```csharp
/// <summary>
/// Draws a line on the canvas between two points.
/// </summary>
/// <param name="canvas">The canvas element reference.</param>
/// <param name="x1">Starting X coordinate.</param>
/// <param name="y1">Starting Y coordinate.</param>
/// <param name="x2">Ending X coordinate.</param>
/// <param name="y2">Ending Y coordinate.</param>
/// <param name="color">Hex color code (e.g., "#FF0000").</param>
/// <param name="lineWidth">Width of the line in pixels.</param>
/// <returns>A task representing the asynchronous operation.</returns>
/// <exception cref="ArgumentNullException">Thrown when canvas is null.</exception>
```

## Project Structure

```
BlazorWhiteBoard/
??? Domain/                    # Core business logic (no dependencies)
?   ??? Entities/             # Domain entities
?   ??? Enums/                # Enumerations
?   ??? ValueObjects/         # Value objects (immutable)
?   ??? Constants/            # Domain constants
?   ??? Extensions/           # Domain extension methods
??? Application/               # Application logic (depends on Domain)
?   ??? Interfaces/           # Service abstractions
?   ??? Services/             # Service implementations
?   ??? DTOs/                 # Data transfer objects
??? Hubs/                      # SignalR hubs (Infrastructure)
??? Components/                # Blazor UI components
?   ??? Pages/                # Routable pages
?   ??? Layout/               # Layout components
??? wwwroot/                   # Static files
    ??? js/                   # JavaScript files
    ??? css/                  # Stylesheets
```

### Layer Responsibilities

- **Domain**: Pure business logic, no external dependencies
- **Application**: Use cases, orchestration, service interfaces
- **Infrastructure** (Hubs): External concerns (SignalR, database, etc.)
- **Presentation** (Components): UI layer, Blazor components

**Dependency Rule**: Inner layers should not depend on outer layers.

## Questions?

Feel free to open an issue with the label `question` if you have any questions about contributing.

## Recognition

Contributors will be recognized in the project README. Thank you for your contributions!

---

**Happy Contributing! ??**
