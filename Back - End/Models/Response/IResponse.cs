namespace Team3.ThePollProject.Models.Response;

public interface IResponse
{
    public bool HasError { get; set; }
    public string? ErrorMessage { get; set; }
    public ICollection<object>? ReturnValue { get; set; }
    public int RetryAttempts { get; set; }
    public bool IsSafeToRetry { get; set; }
}
