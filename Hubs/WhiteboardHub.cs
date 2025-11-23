using BlazorWhiteBoard.Application.DTOs;
using BlazorWhiteBoard.Application.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace BlazorWhiteBoard.Hubs;

/// <summary>
/// SignalR hub for real-time whiteboard collaboration.
/// </summary>
public sealed class WhiteboardHub : Hub
{
    private readonly IUserManagementService _userManagementService;

    public WhiteboardHub(IUserManagementService userManagementService)
    {
        _userManagementService = userManagementService ?? throw new ArgumentNullException(nameof(userManagementService));
    }

    /// <summary>
    /// Registers a user with their display name.
    /// </summary>
    /// <param name="displayName">The user's display name.</param>
    public async Task JoinSession(string displayName)
    {
        var user = await _userManagementService.AddUserAsync(Context.ConnectionId, displayName);
        
        // Notify all clients about the new user
        await Clients.All.SendAsync("UserJoined", new
        {
            user.ConnectionId,
            user.DisplayName,
            user.UserColor,
            TotalUsers = _userManagementService.GetUserCount()
        });

        // Send updated users list to ALL clients (not just the new user)
        var allUsers = _userManagementService.GetAllUsers();
        await Clients.All.SendAsync("ReceiveUsersList", allUsers);
    }

    /// <summary>
    /// Broadcasts drawing data to all clients except the sender.
    /// </summary>
    /// <param name="data">The drawing data to broadcast.</param>
    public async Task SendDrawing(DrawingDataDto data)
    {
        ArgumentNullException.ThrowIfNull(data);

        // Enrich data with user information from server
        var user = _userManagementService.GetUser(Context.ConnectionId);
        if (user != null)
        {
            data.UserName = user.DisplayName;
            data.UserColor = user.UserColor;
            data.ConnectionId = user.ConnectionId;
        }

        await Clients.Others.SendAsync("ReceiveDrawing", data);
    }

    /// <summary>
    /// Broadcasts preview data to all clients except the sender (for shapes in progress).
    /// </summary>
    /// <param name="data">The preview data to broadcast.</param>
    public async Task SendPreview(DrawingDataDto data)
    {
        ArgumentNullException.ThrowIfNull(data);

        // Enrich data with user information from server
        var user = _userManagementService.GetUser(Context.ConnectionId);
        if (user != null)
        {
            data.UserName = user.DisplayName;
            data.UserColor = user.UserColor;
            data.ConnectionId = user.ConnectionId;
            data.IsPreview = true;
        }

        await Clients.Others.SendAsync("ReceivePreview", data);
    }

    /// <summary>
    /// Broadcasts cursor position to all other clients.
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    public async Task UpdateCursorPosition(double x, double y)
    {
        var user = _userManagementService.GetUser(Context.ConnectionId);
        
        if (user != null)
        {
            var cursorData = new CursorPositionDto
            {
                ConnectionId = user.ConnectionId,
                X = x,
                Y = y,
                UserName = user.DisplayName,
                UserColor = user.UserColor
            };
            
            await Clients.Others.SendAsync("ReceiveCursorPosition", cursorData);
        }
    }

    /// <summary>
    /// Broadcasts clear command to all clients.
    /// </summary>
    public async Task ClearWhiteboard()
    {
        var user = _userManagementService.GetUser(Context.ConnectionId);
        await Clients.All.SendAsync("ClearWhiteboard", user?.DisplayName ?? "Someone");
    }

    /// <inheritdoc/>
    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }

    /// <inheritdoc/>
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await _userManagementService.RemoveUserAsync(Context.ConnectionId);
        
        // Notify all clients about the disconnection
        await Clients.All.SendAsync("UserLeft", new
        {
            ConnectionId = Context.ConnectionId,
            TotalUsers = _userManagementService.GetUserCount()
        });

        // Send updated users list to all remaining clients
        var allUsers = _userManagementService.GetAllUsers();
        await Clients.All.SendAsync("ReceiveUsersList", allUsers);

        await base.OnDisconnectedAsync(exception);
    }
}
