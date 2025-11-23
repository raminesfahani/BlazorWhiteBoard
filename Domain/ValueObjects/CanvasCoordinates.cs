namespace BlazorWhiteBoard.Domain.ValueObjects;

/// <summary>
/// Represents coordinates on the canvas with proper scaling.
/// </summary>
public sealed record CanvasCoordinates
{
    /// <summary>
    /// Gets the X coordinate on the canvas.
    /// </summary>
    public required double X { get; init; }

    /// <summary>
    /// Gets the Y coordinate on the canvas.
    /// </summary>
    public required double Y { get; init; }

    /// <summary>
    /// Creates a new instance of canvas coordinates.
    /// </summary>
    public static CanvasCoordinates Create(double x, double y) => new() { X = x, Y = y };
}
