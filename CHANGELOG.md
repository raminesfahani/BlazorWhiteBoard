# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Planned Features
- Text annotation tool
- Undo/Redo functionality
- Export canvas as image (PNG/SVG)
- Persistent canvas storage
- Room-based sessions
- Authentication and user accounts
- Drawing permissions and roles
- Mobile app (MAUI)

## [1.0.0] - 2024-01-XX

### Added
- Initial release of BlazorWhiteBoard
- Real-time collaborative whiteboard functionality
- Multiple drawing tools:
  - Pen for freehand drawing
  - Eraser for removing drawings
  - Laser pointer with auto-fade
  - Line tool
  - Arrow tool
  - Rectangle tool (filled and outlined)
  - Circle tool (filled and outlined)
  - Ellipse tool (filled and outlined)
  - Triangle tool (filled and outlined)
- Color picker for customizable drawing colors
- Adjustable line width (1-30px)
- Real-time cursor tracking for all users
- Live shape preview during drawing
- User session management with display names
- Unique color assignment per user
- Active users list with connection count
- Join/leave notifications
- Canvas clearing functionality (synchronized across all users)
- Touch support for mobile devices
- Auto-reconnection on connection loss
- Connection status indicator
- Clean Architecture implementation with Domain, Application, and Infrastructure layers
- SignalR hub for real-time communication
- Dependency Injection for all services
- XML documentation for public APIs

### Technical Details
- Built with .NET 10 and Blazor Server
- SignalR for WebSocket-based real-time communication
- HTML5 Canvas for high-performance rendering
- JavaScript Interop for canvas operations
- Bootstrap 5 for responsive UI
- Font Awesome icons

### Infrastructure
- Clean separation of concerns (Domain, Application, Infrastructure layers)
- Repository pattern via service abstractions
- DTO pattern for data transfer
- Async/await throughout for non-blocking operations
- SOLID principles adherence

---

## Version History Template

Use this template for future releases:

```markdown
## [X.Y.Z] - YYYY-MM-DD

### Added
- New features

### Changed
- Changes in existing functionality

### Deprecated
- Features that will be removed in upcoming releases

### Removed
- Removed features

### Fixed
- Bug fixes

### Security
- Security improvements or fixes
```

---

## Release Notes Guidelines

### Version Numbers

We follow [Semantic Versioning](https://semver.org/):

- **MAJOR** version (X.0.0): Incompatible API changes
- **MINOR** version (0.X.0): New functionality in a backward-compatible manner
- **PATCH** version (0.0.X): Backward-compatible bug fixes

### Categories

- **Added**: New features
- **Changed**: Changes in existing functionality
- **Deprecated**: Soon-to-be removed features
- **Removed**: Now removed features
- **Fixed**: Bug fixes
- **Security**: Security vulnerability fixes

---

[Unreleased]: https://github.com/yourusername/BlazorWhiteBoard/compare/v1.0.0...HEAD
[1.0.0]: https://github.com/yourusername/BlazorWhiteBoard/releases/tag/v1.0.0
