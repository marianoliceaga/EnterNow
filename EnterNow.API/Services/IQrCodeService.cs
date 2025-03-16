namespace EnterNow.API.Services
{
    public interface IQrCodeService
    {
        string GenerateQrCode(string userId, DateTime membershipExpiry);
        bool ValidateQrCode(string qrCodeData);
    }
}
