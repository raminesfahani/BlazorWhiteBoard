namespace BlazorWhiteBoard.Domain.Constants;

/// <summary>
/// Constants for whiteboard configuration.
/// </summary>
public static class WhiteboardConstants
{
    /// <summary>
    /// Canvas configuration constants.
    /// </summary>
    public static class Canvas
    {
        public const int DefaultWidth = 1600;
        public const int DefaultHeight = 900;
        public const string DefaultBackgroundColor = "#FFFFFF";
    }

    /// <summary>
    /// Drawing tool configuration constants.
    /// </summary>
    public static class Drawing
    {
        public const int MinLineWidth = 1;
        public const int MaxLineWidth = 30;
        public const int DefaultLineWidth = 2;
        public const string DefaultColor = "#000000";
        public const int EraserMultiplier = 3;
        public const int DefaultFontSize = 16;
    }

    /// <summary>
    /// Laser pointer configuration constants.
    /// </summary>
    public static class Laser
    {
        public const int FadeDelayMilliseconds = 500;
    }

    /// <summary>
    /// History configuration constants.
    /// </summary>
    public static class History
    {
        public const int MaxHistorySize = 1000;
    }

    /// <summary>
    /// SignalR hub endpoint.
    /// </summary>
    public static class SignalR
    {
        public const string HubEndpoint = "/whiteboardhub";
    }
}
