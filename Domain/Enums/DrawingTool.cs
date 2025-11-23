namespace BlazorWhiteBoard.Domain.Enums;

/// <summary>
/// Represents the available drawing tools in the whiteboard application.
/// </summary>
public enum DrawingTool
{
    /// <summary>
    /// Freehand drawing tool for continuous lines.
    /// </summary>
    Pen,

    /// <summary>
    /// Eraser tool for removing drawings.
    /// </summary>
    Eraser,

    /// <summary>
    /// Temporary pointer visible to all users, fades after drawing stops.
    /// </summary>
    Laser,

    /// <summary>
    /// Straight line drawing tool.
    /// </summary>
    Line,

    /// <summary>
    /// Arrow drawing tool with arrowhead.
    /// </summary>
    Arrow,

    /// <summary>
    /// Rectangle shape drawing tool.
    /// </summary>
    Rectangle,

    /// <summary>
    /// Circle shape drawing tool.
    /// </summary>
    Circle,

    /// <summary>
    /// Ellipse shape drawing tool.
    /// </summary>
    Ellipse,

    /// <summary>
    /// Triangle shape drawing tool.
    /// </summary>
    Triangle,

    /// <summary>
    /// Text annotation tool.
    /// </summary>
    Text
}
