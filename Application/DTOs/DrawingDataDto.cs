using BlazorWhiteBoard.Domain.Entities;
using BlazorWhiteBoard.Domain.Enums;
using BlazorWhiteBoard.Domain.Extensions;

namespace BlazorWhiteBoard.Application.DTOs;

/// <summary>
/// Data Transfer Object for drawing data over SignalR.
/// </summary>
public sealed class DrawingDataDto
{
    public string Type { get; set; } = string.Empty;
    public double X { get; set; }
    public double Y { get; set; }
    public double PrevX { get; set; }
    public double PrevY { get; set; }
    public double StartX { get; set; }
    public double StartY { get; set; }
    public double EndX { get; set; }
    public double EndY { get; set; }
    public string Color { get; set; } = "#000000";
    public int LineWidth { get; set; } = 2;
    public string Tool { get; set; } = "pen";
    public bool Filled { get; set; }
    public string? Text { get; set; }
    public int FontSize { get; set; } = 16;
    public bool IsLaserEnd { get; set; }
    
    // User information
    public string? UserName { get; set; }
    public string? UserColor { get; set; }
    public string? ConnectionId { get; set; }

    // Preview flag
    public bool IsPreview { get; set; }

    /// <summary>
    /// Converts DTO to domain entity.
    /// </summary>
    public DrawingAction ToEntity() => new()
    {
        Type = Type.ToDrawingType(),
        Tool = Tool.ToDrawingTool(),
        X = X,
        Y = Y,
        PreviousX = PrevX,
        PreviousY = PrevY,
        StartX = StartX,
        StartY = StartY,
        EndX = EndX,
        EndY = EndY,
        Color = Color,
        LineWidth = LineWidth,
        IsFilled = Filled,
        Text = Text,
        FontSize = FontSize,
        IsLaserEnd = IsLaserEnd,
        UserName = UserName,
        UserColor = UserColor,
        ConnectionId = ConnectionId
    };

    /// <summary>
    /// Creates DTO from domain entity.
    /// </summary>
    public static DrawingDataDto FromEntity(DrawingAction action, bool isPreview = false) => new()
    {
        Type = action.Type.ToJsString(),
        Tool = action.Tool.ToJsString(),
        X = action.X,
        Y = action.Y,
        PrevX = action.PreviousX,
        PrevY = action.PreviousY,
        StartX = action.StartX,
        StartY = action.StartY,
        EndX = action.EndX,
        EndY = action.EndY,
        Color = action.Color,
        LineWidth = action.LineWidth,
        Filled = action.IsFilled,
        Text = action.Text,
        FontSize = action.FontSize,
        IsLaserEnd = action.IsLaserEnd,
        UserName = action.UserName,
        UserColor = action.UserColor,
        ConnectionId = action.ConnectionId,
        IsPreview = isPreview
    };
}
