namespace Team3.ThePollProject.Models;

public interface IResponse
{
    public bool HasError { get; set; }
    public string? ErrorMessage { get; set; }
    public ICollection<object>? ReturnValue { get; set; }
    public int RetryAttempts { get; set; }
    public bool IsSafeToRetry { get; set; }
}
