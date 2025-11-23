using BlazorWhiteBoard.Application.Interfaces;
using BlazorWhiteBoard.Domain.Entities;

namespace BlazorWhiteBoard.Application.Services;

/// <summary>
/// Service for managing whiteboard state and drawing history.
/// </summary>
public sealed class WhiteboardStateService : IWhiteboardStateService
{
    private readonly List<DrawingAction> _history = new();
    private const int MaxHistorySize = 1000;

    /// <inheritdoc/>
    public event Action? OnStateChanged;

    /// <inheritdoc/>
    public int Count => _history.Count;

    /// <inheritdoc/>
    public void AddDrawingAction(DrawingAction action)
    {
        ArgumentNullException.ThrowIfNull(action);

        _history.Add(action);

        // Keep history size manageable
        if (_history.Count > MaxHistorySize)
        {
            _history.RemoveAt(0);
        }

        NotifyStateChanged();
    }

    /// <inheritdoc/>
    public void Clear()
    {
        _history.Clear();
        NotifyStateChanged();
    }

    /// <inheritdoc/>
    public IReadOnlyList<DrawingAction> GetHistory() => _history.AsReadOnly();

    private void NotifyStateChanged() => OnStateChanged?.Invoke();
}
