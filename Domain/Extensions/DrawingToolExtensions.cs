using BlazorWhiteBoard.Domain.Enums;

namespace BlazorWhiteBoard.Domain.Extensions;

/// <summary>
/// Extension methods for <see cref="DrawingTool"/> enum.
/// </summary>
public static class DrawingToolExtensions
{
    /// <summary>
    /// Determines whether the tool is a shape tool requiring start and end coordinates.
    /// </summary>
    public static bool IsShapeTool(this DrawingTool tool) => tool switch
    {
        DrawingTool.Rectangle or
        DrawingTool.Circle or
        DrawingTool.Ellipse or
        DrawingTool.Line or
        DrawingTool.Arrow or
        DrawingTool.Triangle => true,
        _ => false
    };

    /// <summary>
    /// Determines whether the tool is a freehand drawing tool.
    /// </summary>
    public static bool IsFreehandTool(this DrawingTool tool) => tool switch
    {
        DrawingTool.Pen or DrawingTool.Eraser => true,
        _ => false
    };

    /// <summary>
    /// Gets the display name for the tool.
    /// </summary>
    public static string GetDisplayName(this DrawingTool tool) => tool switch
    {
        DrawingTool.Pen => "Pen",
        DrawingTool.Eraser => "Eraser",
        DrawingTool.Laser => "Laser Pointer",
        DrawingTool.Line => "Line",
        DrawingTool.Arrow => "Arrow",
        DrawingTool.Rectangle => "Rectangle",
        DrawingTool.Circle => "Circle",
        DrawingTool.Ellipse => "Ellipse",
        DrawingTool.Triangle => "Triangle",
        DrawingTool.Text => "Text",
        _ => tool.ToString()
    };

    /// <summary>
    /// Gets the Font Awesome icon class for the tool.
    /// </summary>
    public static string GetIconClass(this DrawingTool tool) => tool switch
    {
        DrawingTool.Pen => "fa-solid fa-pen",
        DrawingTool.Eraser => "fa-solid fa-eraser",
        DrawingTool.Laser => "fa-solid fa-arrow-pointer",
        DrawingTool.Line => "fa-solid fa-minus",
        DrawingTool.Arrow => "fa-solid fa-arrow-right",
        DrawingTool.Rectangle => "fa-regular fa-square",
        DrawingTool.Circle => "fa-regular fa-circle",
        DrawingTool.Ellipse => "fa-solid fa-solid fa-circle-half-stroke",
        DrawingTool.Triangle => "fa-solid fa-play fa-rotate-270",
        DrawingTool.Text => "fa-solid fa-font",
        _ => "fa-solid fa-pen"
    };

    /// <summary>
    /// Converts string to DrawingTool enum.
    /// </summary>
    public static DrawingTool ToDrawingTool(this string toolName) => toolName.ToLowerInvariant() switch
    {
        "pen" => DrawingTool.Pen,
        "eraser" => DrawingTool.Eraser,
        "laser" => DrawingTool.Laser,
        "line" => DrawingTool.Line,
        "arrow" => DrawingTool.Arrow,
        "rectangle" => DrawingTool.Rectangle,
        "circle" => DrawingTool.Circle,
        "ellipse" => DrawingTool.Ellipse,
        "triangle" => DrawingTool.Triangle,
        "text" => DrawingTool.Text,
        _ => DrawingTool.Pen
    };

    /// <summary>
    /// Converts DrawingTool enum to lowercase string for JavaScript interop.
    /// </summary>
    public static string ToJsString(this DrawingTool tool) => tool.ToString().ToLowerInvariant();
}
