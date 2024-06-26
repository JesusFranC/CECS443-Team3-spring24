﻿namespace Team3.ThePollProject.LoggingLibrary;

public interface ILog
{
    DateTimeOffset LogTime { get; set; }
    string LogLevel { get; set; }
    string LogCategory { get; set; }
    string LogContext { get; set; }
    string LogHash { get; set; }
    string? UserHash { get; set; }

}
