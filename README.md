# 🎨 Real-time Collaborative Blazor Whiteboard

A real-time collaborative whiteboard application built with **Blazor Server** and **SignalR**. Draw, sketch, and collaborate with multiple users simultaneously with live cursor tracking, shape previews, and instant synchronization.

![.NET Version](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)
![Blazor](https://img.shields.io/badge/Blazor-Server-512BD4?logo=blazor)
![SignalR](https://img.shields.io/badge/SignalR-Real--time-00ADD8)
![License](https://img.shields.io/badge/license-MIT-green)

## ✨ Features

### 🎨 Drawing Tools
- **Pen** - Freehand drawing with customizable colors and sizes
- **Eraser** - Remove unwanted drawings with adjustable eraser size
- **Laser Pointer** - Temporary animated pointer with trail effects that fades automatically
- **Geometric Shapes** - Draw precise shapes:
  - Lines
  - Arrows with dynamic arrowheads
  - Rectangles
  - Circles
  - Ellipses
  - Triangles
- **Fill Option** - Toggle between filled and outlined shapes
- **Color Picker** - Choose from any color for your drawings
- **Adjustable Size** - Control line width (1-30px) with real-time preview

### 👥 Real-time Collaboration
- **Multi-user Support** - Unlimited simultaneous users with unique colors
- **Live Cursor Tracking** - See realistic cursor icons for each user in real-time
- **Shape Previews** - Watch shapes being drawn by others before they're finalized
- **User Identification** - Each user has a unique color and name badge displayed on their drawings
- **Active User List** - Dropdown panel showing who's currently connected
- **Join/Leave Notifications** - Toast notifications when users join or leave
- **Connection Status Indicator** - Visual indicator showing your connection state
- **User Name Labels** - Each drawing shows the artist's name temporarily

### 🔄 Real-time Synchronization
- **Instant Drawing Sync** - All drawings appear on all connected clients immediately via SignalR
- **Canvas Clearing** - Clear the entire whiteboard for everyone with one click
- **Auto-reconnection** - Automatic reconnection when connection is lost
- **Preview Broadcasting** - Live shape previews are broadcast and throttled for performance (50ms intervals)
- **Laser Trail Rendering** - Smooth animated laser trails with fade effects

### 📱 Responsive Design
- **Touch Support** - Full support for touch devices (tablets, smartphones)
- **Fullscreen Canvas** - Optimized fullscreen drawing experience (1600x900 canvas)
- **Floating Toolbar** - Clean, non-intrusive toolbar with smooth animations
- **Mobile-friendly** - Adaptive UI that works seamlessly on all screen sizes
- **Custom Scrollbars** - Styled scrollbars for a polished look

### 🎯 Advanced Features
- **Triple-Canvas Rendering** - Separate canvases for permanent drawings, local previews, and remote previews
- **Rendering Loop** - 60 FPS animation loop for smooth cursor and laser pointer rendering
- **User Color Assignment** - Automatic unique color generation for each user
- **Stale Data Cleanup** - Automatic removal of inactive cursors, laser points, and previews
- **Cursor Icon Rendering** - Realistic mouse cursor icons with user color accents
- **Responsive User Interface** - Modal prompts, dropdown menus, and toast notifications
- **Keyboard Support** - Enter key to join sessions quickly

## 🚀 Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) or later
- A modern web browser (Chrome, Firefox, Edge, Safari)
- WebSocket support (required for SignalR)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/BlazorWhiteBoard.git
   cd BlazorWhiteBoard
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Build the project**
   ```bash
   dotnet build
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```

5. **Open your browser**
   Navigate to `https://localhost:5001` or `http://localhost:5000` (or the URL shown in the console)

## 🛠️ Technology Stack

| Technology | Purpose |
|-----------|---------|
| **.NET 10** | Modern cross-platform framework |
| **Blazor Server** | Interactive web UI with C# and minimal JavaScript |
| **SignalR** | Real-time WebSocket communication for collaboration |
| **HTML5 Canvas** | High-performance 2D drawing surface |
| **JavaScript Interop** | Canvas manipulation and DOM interaction |
| **Bootstrap 5** | Responsive UI components and grid system |
| **Font Awesome** | Beautiful icons for tools and UI elements |

## 📦 Key Dependencies

- **Microsoft.AspNetCore.SignalR.Client** (v10.0.0) - Real-time communication

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request. For major changes, please open an issue first to discuss what you would like to change.

See [CONTRIBUTING.md](CONTRIBUTING.md) for detailed guidelines.

### Development Guidelines

1. Follow Clean Architecture principles
2. Write descriptive commit messages using conventional commits
3. Add XML documentation for all public APIs
4. Ensure all code follows C# coding conventions (.editorconfig)
5. Test your changes thoroughly on multiple browsers
6. Test touch functionality on mobile devices

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🙏 Acknowledgments

- Built with [Blazor](https://blazor.net) by Microsoft
- Real-time communication powered by [SignalR](https://dotnet.microsoft.com/apps/aspnet/signalr)
- Icons by [Font Awesome](https://fontawesome.com)
- UI components by [Bootstrap](https://getbootstrap.com)

## 🐛 Known Issues

- Canvas size is fixed at 1600x900 (future: make it responsive)
- No persistent storage (drawings are lost on refresh)
- No undo/redo functionality yet
- Text tool is defined but not yet implemented

## 🔮 Future Enhancements

- Undo/Redo functionality
- Text annotation tool
- Export canvas as PNG/SVG
- Persistent storage with database
- Room-based sessions
- Authentication and user accounts
- Drawing permissions and roles
- Canvas zoom and pan
- Mobile app using .NET MAUI
- Collaborative object selection and manipulation

## 📧 Contact

For questions, feedback, or bug reports, please open an issue on GitHub.

---

