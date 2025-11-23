using BlazorWhiteBoard.Domain.Entities;

namespace BlazorWhiteBoard.Application.Interfaces;

/// <summary>
/// Interface for managing whiteboard users.
/// </summary>
public interface IUserManagementService
{
    /// <summary>
    /// Event raised when a user joins or leaves.
    /// </summary>
    event Action? OnUsersChanged;

    /// <summary>
    /// Adds a new user to the session.
    /// </summary>
    Task<WhiteboardUser> AddUserAsync(string connectionId, string displayName);

    /// <summary>
    /// Removes a user from the session.
    /// </summary>
    Task RemoveUserAsync(string connectionId);

    /// <summary>
    /// Gets a user by connection ID.
    /// </summary>
    WhiteboardUser? GetUser(string connectionId);

    /// <summary>
    /// Gets all active users.
    /// </summary>
    IReadOnlyList<WhiteboardUser> GetAllUsers();

    /// <summary>
    /// Gets the total count of active users.
    /// </summary>
    int GetUserCount();

    /// <summary>
    /// Checks if a user exists by connection ID.
    /// </summary>
    bool UserExists(string connectionId);
}
