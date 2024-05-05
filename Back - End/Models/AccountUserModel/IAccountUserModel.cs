namespace Team3.ThePollProject.Model;
public interface IAccountUserModel
{
    long UserId { get; set; }
    string UserName { get; set; }
    uint Salt { get; set; }
    string? UserHash { get; set; }
}