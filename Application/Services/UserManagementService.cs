using BlazorWhiteBoard.Application.Interfaces;
using BlazorWhiteBoard.Domain.Entities;
using System.Collections.Concurrent;

namespace BlazorWhiteBoard.Application.Services;

/// <summary>
/// Service for managing whiteboard users.
/// </summary>
public sealed class UserManagementService : IUserManagementService
{
    private readonly ConcurrentDictionary<string, WhiteboardUser> _users = new();

    /// <inheritdoc/>
    public event Action? OnUsersChanged;

    /// <inheritdoc/>
    public async Task<WhiteboardUser> AddUserAsync(string connectionId, string displayName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionId);
        ArgumentException.ThrowIfNullOrWhiteSpace(displayName);

        var user = WhiteboardUser.Create(connectionId, displayName);
        _users.TryAdd(connectionId, user);

        NotifyUsersChanged();

        return await Task.FromResult(user);
    }

    /// <inheritdoc/>
    public async Task RemoveUserAsync(string connectionId)
    {
        if (_users.TryRemove(connectionId, out _))
        {
            NotifyUsersChanged();
        }

        await Task.CompletedTask;
    }

    /// <inheritdoc/>
    public WhiteboardUser? GetUser(string connectionId)
    {
        _users.TryGetValue(connectionId, out var user);
        return user;
    }

    /// <inheritdoc/>
    public IReadOnlyList<WhiteboardUser> GetAllUsers()
    {
        return _users.Values.Where(u => u.IsActive).ToList().AsReadOnly();
    }

    /// <inheritdoc/>
    public int GetUserCount()
    {
        return _users.Count(kvp => kvp.Value.IsActive);
    }

    /// <inheritdoc/>
    public bool UserExists(string connectionId)
    {
        return _users.ContainsKey(connectionId);
    }

    private void NotifyUsersChanged()
    {
        OnUsersChanged?.Invoke();
    }
}
