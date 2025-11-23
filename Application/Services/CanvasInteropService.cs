using BlazorWhiteBoard.Application.Interfaces;
using BlazorWhiteBoard.Domain.Enums;
using BlazorWhiteBoard.Domain.Extensions;
using BlazorWhiteBoard.Domain.ValueObjects;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorWhiteBoard.Application.Services;

/// <summary>
/// Service for canvas JavaScript interop operations.
/// </summary>
public sealed class CanvasInteropService : ICanvasInteropService
{
    private readonly IJSRuntime _jsRuntime;
    private const string JsNamespace = "whiteboardInterop";

    public CanvasInteropService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime ?? throw new ArgumentNullException(nameof(jsRuntime));
    }

    /// <inheritdoc/>
    public async Task InitializeAsync(ElementReference canvasElement)
    {
        await _jsRuntime.InvokeVoidAsync($"{JsNamespace}.initialize", canvasElement);
    }

    /// <summary>
    /// Sets the current user information in JavaScript.
    /// </summary>
    public async Task SetCurrentUserAsync(string userName, string userColor)
    {
        await _jsRuntime.InvokeVoidAsync($"{JsNamespace}.setCurrentUser", userName, userColor);
    }

    /// <summary>
    /// Updates cursor position for a remote user.
    /// </summary>
    public async Task UpdateCursorAsync(ElementReference canvasElement, string connectionId, double x, double y, string userName, string userColor)
    {
        await _jsRuntime.InvokeVoidAsync($"{JsNamespace}.updateCursor", canvasElement, connectionId, x, y, userName, userColor);
    }

    /// <summary>
    /// Removes cursor for a user.
    /// </summary>
    public async Task RemoveCursorAsync(string connectionId)
    {
        await _jsRuntime.InvokeVoidAsync($"{JsNamespace}.removeCursor", connectionId);
    }

    /// <summary>
    /// Removes laser point for a specific connection ID.
    /// </summary>
    public async Task RemoveLaserPointAsync(string connectionId)
    {
        await _jsRuntime.InvokeVoidAsync($"{JsNamespace}.removeLaserPoint", connectionId);
    }

    /// <inheritdoc/>
    public async Task<CanvasCoordinates> GetCanvasCoordinatesAsync(ElementReference canvasElement, double clientX, double clientY)
    {
        var result = await _jsRuntime.InvokeAsync<CanvasCoordinatesDto>(
            $"{JsNamespace}.getCanvasCoordinates",
            canvasElement,
            clientX,
            clientY);

        return CanvasCoordinates.Create(result.X, result.Y);
    }

    /// <inheritdoc/>
    public async Task DrawLineAsync(ElementReference canvasElement, double prevX, double prevY, double x, double y, string color, int lineWidth)
    {
        await _jsRuntime.InvokeVoidAsync(
            $"{JsNamespace}.drawLine",
            canvasElement,
            prevX,
            prevY,
            x,
            y,
            color,
            lineWidth,
            null,  // userName
            null); // userColor
    }

    public async Task DrawLineAsync(ElementReference canvasElement, double prevX, double prevY, double x, double y, string color, int lineWidth, string? userName, string? userColor)
    {
        await _jsRuntime.InvokeVoidAsync(
            $"{JsNamespace}.drawLine",
            canvasElement,
            prevX,
            prevY,
            x,
            y,
            color,
            lineWidth,
            userName,
            userColor);
    }

    /// <inheritdoc/>
    public async Task DrawShapePreviewAsync(ElementReference canvasElement, DrawingTool tool, double startX, double startY, double endX, double endY, string color, int lineWidth, bool filled)
    {
        await _jsRuntime.InvokeVoidAsync(
            $"{JsNamespace}.drawShapePreview",
            canvasElement,
            tool.ToJsString(),
            startX,
            startY,
            endX,
            endY,
            color,
            lineWidth,
            filled,
            null,  // userName
            null); // userColor
    }

    public async Task DrawShapePreviewAsync(ElementReference canvasElement, DrawingTool tool, double startX, double startY, double endX, double endY, string color, int lineWidth, bool filled, string? userName, string? userColor)
    {
        await _jsRuntime.InvokeVoidAsync(
            $"{JsNamespace}.drawShapePreview",
            canvasElement,
            tool.ToJsString(),
            startX,
            startY,
            endX,
            endY,
            color,
            lineWidth,
            filled,
            userName,
            userColor);
    }

    /// <inheritdoc/>
    public async Task DrawShapeAsync(ElementReference canvasElement, DrawingTool tool, double startX, double startY, double endX, double endY, string color, int lineWidth, bool filled)
    {
        await _jsRuntime.InvokeVoidAsync(
            $"{JsNamespace}.drawShape",
            canvasElement,
            tool.ToJsString(),
            startX,
            startY,
            endX,
            endY,
            color,
            lineWidth,
            filled,
            null,  // userName
            null); // userColor
    }

    public async Task DrawShapeAsync(ElementReference canvasElement, DrawingTool tool, double startX, double startY, double endX, double endY, string color, int lineWidth, bool filled, string? userName, string? userColor)
    {
        await _jsRuntime.InvokeVoidAsync(
            $"{JsNamespace}.drawShape",
            canvasElement,
            tool.ToJsString(),
            startX,
            startY,
            endX,
            endY,
            color,
            lineWidth,
            filled,
            userName,
            userColor);
    }

    /// <inheritdoc/>
    public async Task DrawLaserPointAsync(ElementReference canvasElement, double x, double y, string color)
    {
        await _jsRuntime.InvokeVoidAsync(
            $"{JsNamespace}.drawLaserPoint",
            canvasElement,
            x,
            y,
            color,
            null,  // userName
            null); // userColor
    }

    public async Task DrawLaserPointAsync(ElementReference canvasElement, double x, double y, string color, string? userName, string? userColor)
    {
        await _jsRuntime.InvokeVoidAsync(
            $"{JsNamespace}.drawLaserPoint",
            canvasElement,
            x,
            y,
            color,
            userName,
            userColor,
            null); // connectionId for local user
    }

    public async Task DrawLaserPointAsync(ElementReference canvasElement, double x, double y, string color, string? userName, string? userColor, string? connectionId)
    {
        await _jsRuntime.InvokeVoidAsync(
            $"{JsNamespace}.drawLaserPoint",
            canvasElement,
            x,
            y,
            color,
            userName,
            userColor,
            connectionId);
    }

    /// <inheritdoc/>
    public async Task ClearLaserPointsAsync(ElementReference canvasElement)
    {
        await _jsRuntime.InvokeVoidAsync($"{JsNamespace}.clearLaserPoints", canvasElement);
    }

    /// <inheritdoc/>
    public async Task ClearCanvasAsync(ElementReference canvasElement)
    {
        await _jsRuntime.InvokeVoidAsync($"{JsNamespace}.clearCanvas", canvasElement);
    }

    /// <inheritdoc/>
    public async Task SaveCanvasStateAsync(ElementReference canvasElement)
    {
        await _jsRuntime.InvokeVoidAsync($"{JsNamespace}.saveCanvasState", canvasElement);
    }

    /// <inheritdoc/>
    public async Task RestoreCanvasStateAsync(ElementReference canvasElement)
    {
        await _jsRuntime.InvokeVoidAsync($"{JsNamespace}.restoreCanvasState", canvasElement);
    }

    /// <inheritdoc/>
    public async Task StoreRemotePreviewAsync(string connectionId, DrawingTool tool, double startX, double startY, double endX, double endY, string color, int lineWidth, bool filled, string? userName, string? userColor)
    {
        await _jsRuntime.InvokeVoidAsync(
            $"{JsNamespace}.storeRemotePreview",
            connectionId,
            tool.ToJsString(),
            startX,
            startY,
            endX,
            endY,
            color,
            lineWidth,
            filled,
            userName,
            userColor);
    }

    /// <inheritdoc/>
    public async Task ClearRemotePreviewAsync(string connectionId)
    {
        await _jsRuntime.InvokeVoidAsync($"{JsNamespace}.clearRemotePreview", connectionId);
    }

    // Internal DTO for JS interop
    private sealed class CanvasCoordinatesDto
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
}
