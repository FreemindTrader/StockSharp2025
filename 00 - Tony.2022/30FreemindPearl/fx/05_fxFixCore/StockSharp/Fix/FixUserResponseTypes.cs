namespace StockSharp.Fix
{
    /// <summary>Response types.</summary>
    public enum FixUserResponseTypes
    {
        /// <summary>Password changed.</summary>
        PasswordChanged,
        /// <summary>User found.</summary>
        UserFound,
        /// <summary>User logged off.</summary>
        UserLoggedOff,
        /// <summary>User not found.</summary>
        UserNotFound,
        /// <summary>User blocked.</summary>
        UserBlocked,
        /// <summary>Old password is incorrect.</summary>
        OldPasswordIncorrect,
        /// <summary>New password is incorrect.</summary>
        NewPasswordIncorrect,
        /// <summary>Not supported.</summary>
        NotSupported,
        /// <summary>Unknown server error.</summary>
        UnknownError,
    }
}
