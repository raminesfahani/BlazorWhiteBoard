namespace BlazorWhiteBoard.Domain.Entities;

/// <summary>
/// Represents a connected user in the whiteboard session.
/// </summary>
public sealed class WhiteboardUser
{
    /// <summary>
    /// Gets or sets the unique connection identifier.
    /// </summary>
    public required string ConnectionId { get; init; }

    /// <summary>
    /// Gets or sets the user's display name.
    /// </summary>
    public required string DisplayName { get; init; }

    /// <summary>
    /// Gets or sets the user's assigned color for visual identification.
    /// </summary>
    public required string UserColor { get; init; }

    /// <summary>
    /// Gets or sets when the user joined the session.
    /// </summary>
    public DateTimeOffset JoinedAt { get; init; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// Gets or sets whether the user is currently active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Creates a new user with a random color.
    /// </summary>
    public static WhiteboardUser Create(string connectionId, string displayName)
    {
        return new WhiteboardUser
        {
            ConnectionId = connectionId,
            DisplayName = displayName,
            UserColor = GenerateRandomColor()
        };
    }

    private static string GenerateRandomColor()
    {
        var colors = new[]
        {
            "#FF6B6B", "#4ECDC4", "#45B7D1", "#FFA07A", "#98D8C8",
            "#F7DC6F", "#BB8FCE", "#85C1E9", "#F8B739", "#52C563",
            "#FF8B94", "#A8E6CF", "#FFD3B6", "#FFAAA5", "#FF8D89"
        };
        
        return colors[Random.Shared.Next(colors.Length)];
    }
}
