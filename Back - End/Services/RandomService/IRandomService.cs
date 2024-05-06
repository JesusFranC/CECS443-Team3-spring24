namespace Team3.ThePollProject.Services;

public interface IRandomService
{
    public  uint GenerateUnsignedInt();
    public  int GenerateSignedInt();
    string GenerateRandomString(int size); 
}
