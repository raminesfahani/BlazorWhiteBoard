using BlazorWhiteBoard.Domain.Enums;

namespace BlazorWhiteBoard.Domain.Extensions;

/// <summary>
/// Extension methods for <see cref="DrawingType"/> enum.
/// </summary>
public static class DrawingTypeExtensions
{
    /// <summary>
    /// Converts string to DrawingType enum.
    /// </summary>
    public static DrawingType ToDrawingType(this string typeName) => typeName.ToLowerInvariant() switch
    {
        "draw" => DrawingType.Draw,
        "shape" => DrawingType.Shape,
        "laser" => DrawingType.Laser,
        "text" => DrawingType.Text,
        _ => DrawingType.Draw
    };

    /// <summary>
    /// Converts DrawingType enum to lowercase string for JavaScript interop.
    /// </summary>
    public static string ToJsString(this DrawingType type) => type.ToString().ToLowerInvariant();

    /// <summary>
    /// Gets the display name for the drawing type.
    /// </summary>
    public static string GetDisplayName(this DrawingType type) => type switch
    {
        DrawingType.Draw => "Drawing",
        DrawingType.Shape => "Shape",
        DrawingType.Laser => "Laser Pointer",
        DrawingType.Text => "Text",
        _ => type.ToString()
    };
}
