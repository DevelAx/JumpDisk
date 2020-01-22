using System;
using System.Globalization;

public class GameException : Exception
{
    public GameException() : base() { }

    public GameException(string message) : base(message) { }

    public GameException(string formattedMessage, params object[] args)
        : base(string.Format(CultureInfo.CurrentCulture, formattedMessage, args))
    { }
}