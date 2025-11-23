namespace BlazorWhiteBoard.Domain.Enums;

/// <summary>
/// Represents the type of drawing action being performed.
/// </summary>
public enum DrawingType
{
    /// <summary>
    /// Freehand drawing (pen or eraser).
    /// </summary>
    Draw,

    /// <summary>
    /// Shape drawing (rectangle, circle, line, etc.).
    /// </summary>
    Shape,

    /// <summary>
    /// Laser pointer action.
    /// </summary>
    Laser,

    /// <summary>
    /// Text annotation.
    /// </summary>
    Text
}
