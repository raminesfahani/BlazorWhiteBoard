using BlazorWhiteBoard.Domain.Enums;

namespace BlazorWhiteBoard.Domain.Entities;

/// <summary>
/// Represents a single drawing action on the whiteboard.
/// </summary>
public sealed class DrawingAction
{
    /// <summary>
    /// Gets or sets the type of drawing action.
    /// </summary>
    public DrawingType Type { get; set; }

    /// <summary>
    /// Gets or sets the drawing tool used.
    /// </summary>
    public DrawingTool Tool { get; set; }

    /// <summary>
    /// Gets or sets the current X coordinate.
    /// </summary>
    public double X { get; set; }

    /// <summary>
    /// Gets or sets the current Y coordinate.
    /// </summary>
    public double Y { get; set; }

    /// <summary>
    /// Gets or sets the previous X coordinate (for continuous drawing).
    /// </summary>
    public double PreviousX { get; set; }

    /// <summary>
    /// Gets or sets the previous Y coordinate (for continuous drawing).
    /// </summary>
    public double PreviousY { get; set; }

    /// <summary>
    /// Gets or sets the start X coordinate (for shapes).
    /// </summary>
    public double StartX { get; set; }

    /// <summary>
    /// Gets or sets the start Y coordinate (for shapes).
    /// </summary>
    public double StartY { get; set; }

    /// <summary>
    /// Gets or sets the end X coordinate (for shapes).
    /// </summary>
    public double EndX { get; set; }

    /// <summary>
    /// Gets or sets the end Y coordinate (for shapes).
    /// </summary>
    public double EndY { get; set; }

    /// <summary>
    /// Gets or sets the drawing color in hex format.
    /// </summary>
    public string Color { get; set; } = "#000000";

    /// <summary>
    /// Gets or sets the line width in pixels.
    /// </summary>
    public int LineWidth { get; set; } = 2;

    /// <summary>
    /// Gets or sets whether shapes should be filled.
    /// </summary>
    public bool IsFilled { get; set; }

    /// <summary>
    /// Gets or sets the text content for text annotations.
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// Gets or sets the font size for text annotations.
    /// </summary>
    public int FontSize { get; set; } = 16;

    /// <summary>
    /// Gets or sets whether this marks the end of a laser pointer action.
    /// </summary>
    public bool IsLaserEnd { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the action was created.
    /// </summary>
    public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// Gets or sets the user who performed this action.
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// Gets or sets the user's color for visual identification.
    /// </summary>
    public string? UserColor { get; set; }

    /// <summary>
    /// Gets or sets the connection ID of the user who performed this action.
    /// </summary>
    public string? ConnectionId { get; set; }
}
