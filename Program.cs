using BlazorWhiteBoard.Components;
using BlazorWhiteBoard.Application.Interfaces;
using BlazorWhiteBoard.Application.Services;
using BlazorWhiteBoard.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add Razor Components with Interactive Server
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add SignalR for real-time communication
builder.Services.AddSignalR();

// Register Application Services
builder.Services.AddScoped<ICanvasInteropService, CanvasInteropService>();
builder.Services.AddSingleton<IWhiteboardStateService, WhiteboardStateService>();
builder.Services.AddSingleton<IUserManagementService, UserManagementService>();

var app = builder.Build();

// Configure HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAntiforgery();

// Map endpoints
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Map SignalR hub
app.MapHub<WhiteboardHub>("/whiteboardhub");

app.Run();
