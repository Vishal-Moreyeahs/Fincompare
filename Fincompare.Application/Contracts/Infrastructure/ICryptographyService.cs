namespace Fincompare.Application.Contracts.Infrastructure
{
    public interface ICryptographyService
    {
        byte[] EncryptPassword(string password);

        bool ValidatePassword(byte[] p1, byte[] p2);
    }
}
