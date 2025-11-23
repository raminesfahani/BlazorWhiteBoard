using BlazorWhiteBoard.Domain.Entities;

namespace BlazorWhiteBoard.Application.Interfaces;

/// <summary>
/// Interface for managing whiteboard state and drawing history.
/// </summary>
public interface IWhiteboardStateService
{
    /// <summary>
    /// Event raised when the whiteboard state changes.
    /// </summary>
    event Action? OnStateChanged;

    /// <summary>
    /// Adds a drawing action to the history.
    /// </summary>
    /// <param name="action">The drawing action to add.</param>
    void AddDrawingAction(DrawingAction action);

    /// <summary>
    /// Clears all drawing actions from the history.
    /// </summary>
    void Clear();

    /// <summary>
    /// Gets the complete drawing history.
    /// </summary>
    /// <returns>Read-only list of drawing actions.</returns>
    IReadOnlyList<DrawingAction> GetHistory();

    /// <summary>
    /// Gets the number of actions in the history.
    /// </summary>
    int Count { get; }
}
