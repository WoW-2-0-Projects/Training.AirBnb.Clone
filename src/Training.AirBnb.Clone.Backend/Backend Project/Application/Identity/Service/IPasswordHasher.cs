namespace Backend_Project.Application.Identity.Service;

public interface IPasswordHasher
{
    string Hash(string password);
    bool Verify(string password, string hashedPassword);
}