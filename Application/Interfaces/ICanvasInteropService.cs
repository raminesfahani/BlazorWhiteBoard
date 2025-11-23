using BlazorWhiteBoard.Domain.Enums;
using BlazorWhiteBoard.Domain.ValueObjects;
using Microsoft.AspNetCore.Components;

namespace BlazorWhiteBoard.Application.Interfaces;

/// <summary>
/// Interface for canvas JavaScript interop operations.
/// </summary>
public interface ICanvasInteropService
{
    /// <summary>
    /// Initializes the canvas with proper context and settings.
    /// </summary>
    Task InitializeAsync(ElementReference canvasElement);

    /// <summary>
    /// Sets the current user information in JavaScript for preview labels.
    /// </summary>
    Task SetCurrentUserAsync(string userName, string userColor);

    /// <summary>
    /// Updates cursor position for a remote user.
    /// </summary>
    Task UpdateCursorAsync(ElementReference canvasElement, string connectionId, double x, double y, string userName, string userColor);

    /// <summary>
    /// Removes cursor for a disconnected user.
    /// </summary>
    Task RemoveCursorAsync(string connectionId);

    /// <summary>
    /// Removes laser point for a specific connection ID.
    /// </summary>
    Task RemoveLaserPointAsync(string connectionId);

    /// <summary>
    /// Gets the properly scaled canvas coordinates from client coordinates.
    /// </summary>
    Task<CanvasCoordinates> GetCanvasCoordinatesAsync(ElementReference canvasElement, double clientX, double clientY);

    /// <summary>
    /// Draws a line between two points.
    /// </summary>
    Task DrawLineAsync(ElementReference canvasElement, double prevX, double prevY, double x, double y, string color, int lineWidth);

    /// <summary>
    /// Draws a line between two points with user attribution.
    /// </summary>
    Task DrawLineAsync(ElementReference canvasElement, double prevX, double prevY, double x, double y, string color, int lineWidth, string? userName, string? userColor);

    /// <summary>
    /// Draws a shape preview on the canvas (temporary, not saved to permanent canvas).
    /// </summary>
    Task DrawShapePreviewAsync(ElementReference canvasElement, DrawingTool tool, double startX, double startY, double endX, double endY, string color, int lineWidth, bool filled);

    /// <summary>
    /// Draws a shape preview on the canvas with user attribution.
    /// </summary>
    Task DrawShapePreviewAsync(ElementReference canvasElement, DrawingTool tool, double startX, double startY, double endX, double endY, string color, int lineWidth, bool filled, string? userName, string? userColor);

    /// <summary>
    /// Draws a finalized shape on the canvas (saved to permanent canvas).
    /// </summary>
    Task DrawShapeAsync(ElementReference canvasElement, DrawingTool tool, double startX, double startY, double endX, double endY, string color, int lineWidth, bool filled);

    /// <summary>
    /// Draws a finalized shape on the canvas with user attribution.
    /// </summary>
    Task DrawShapeAsync(ElementReference canvasElement, DrawingTool tool, double startX, double startY, double endX, double endY, string color, int lineWidth, bool filled, string? userName, string? userColor);

    /// <summary>
    /// Draws a laser pointer point.
    /// </summary>
    Task DrawLaserPointAsync(ElementReference canvasElement, double x, double y, string color);

    /// <summary>
    /// Draws a laser pointer point with user attribution.
    /// </summary>
    Task DrawLaserPointAsync(ElementReference canvasElement, double x, double y, string color, string? userName, string? userColor);

    /// <summary>
    /// Draws a laser pointer point with user attribution and connection ID.
    /// </summary>
    Task DrawLaserPointAsync(ElementReference canvasElement, double x, double y, string color, string? userName, string? userColor, string? connectionId);

    /// <summary>
    /// Clears all laser pointer points from the canvas.
    /// </summary>
    Task ClearLaserPointsAsync(ElementReference canvasElement);

    /// <summary>
    /// Clears the entire canvas.
    /// </summary>
    Task ClearCanvasAsync(ElementReference canvasElement);

    /// <summary>
    /// Saves the current canvas state for later restoration.
    /// </summary>
    Task SaveCanvasStateAsync(ElementReference canvasElement);

    /// <summary>
    /// Restores the previously saved canvas state.
    /// </summary>
    Task RestoreCanvasStateAsync(ElementReference canvasElement);

    /// <summary>
    /// Stores a remote preview for a specific connection (for multi-user preview tracking).
    /// </summary>
    Task StoreRemotePreviewAsync(string connectionId, DrawingTool tool, double startX, double startY, double endX, double endY, string color, int lineWidth, bool filled, string? userName, string? userColor);

    /// <summary>
    /// Clears a remote preview for a specific connection.
    /// </summary>
    Task ClearRemotePreviewAsync(string connectionId);
}
