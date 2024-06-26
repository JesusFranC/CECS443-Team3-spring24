﻿namespace Team3.ThePollProject.Model;
public class AccountUserModel : IAccountUserModel
{
    public long UserId { get; set; }
    public string UserName { get; set;}
    public uint Salt { get; set; } = 0;
    public string? UserHash { get; set; } = null;
    
    public AccountUserModel(string userName)
    {
        UserName = userName;
    }
}
