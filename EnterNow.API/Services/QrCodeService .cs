using QRCoder;
using System.Text.Json;

namespace EnterNow.API.Services
{
    public class QrCodeService : IQrCodeService
    {
        public string GenerateQrCode(string userId, DateTime membershipExpiry)
        {
            var qrData = new
            {
                UserId = userId,
                MembershipExpiry = membershipExpiry
            };

            string jsonData = JsonSerializer.Serialize(qrData);
            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(jsonData, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new PngByteQRCode(qrCodeData);
            return Convert.ToBase64String(qrCode.GetGraphic(20));
        }

        public bool ValidateQrCode(string qrCodeData)
        {
            try
            {
                var jsonData = JsonSerializer.Deserialize<dynamic>(qrCodeData);
                DateTime expiryDate = DateTime.Parse(jsonData?.MembershipExpiry.ToString() ?? string.Empty);
                return expiryDate > DateTime.UtcNow;
            }
            catch
            {
                return false;
            }
        }
    }
}
