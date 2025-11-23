namespace BlazorWhiteBoard.Application.DTOs;

/// <summary>
/// DTO for cursor position data.
/// </summary>
public sealed class CursorPositionDto
{
    public string ConnectionId { get; set; } = string.Empty;
    public double X { get; set; }
    public double Y { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string UserColor { get; set; } = string.Empty;
}
