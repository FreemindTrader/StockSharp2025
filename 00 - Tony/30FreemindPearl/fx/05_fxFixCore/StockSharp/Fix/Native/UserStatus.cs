namespace StockSharp.Fix.Native
{
    /// <summary>The user request states.</summary>
    public enum UserStatus
    {
        /// <summary>Logged in.</summary>
        LoggedIn = 1,
        /// <summary>Not logged in.</summary>
        NotLoggedIn = 2,
        /// <summary>User not found.</summary>
        UserNotRecognised = 3,
        /// <summary>Incorrect password.</summary>
        PasswordIncorrect = 4,
        /// <summary>The password was changed.</summary>
        PasswordChanged = 5,
        /// <summary>
        /// </summary>
        Other = 6,
        /// <summary>
        /// </summary>
        ForcedUserLogoutByExchange = 7,
        /// <summary>
        /// </summary>
        SessionShutdownWarning = 8,
    }
}
