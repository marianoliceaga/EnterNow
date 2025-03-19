using System.Net.Http.Json;
using System.Threading.Tasks;
using ZXing.Net.Maui;

public class QrScannerService
{
    private readonly HttpClient _httpClient;

    public QrScannerService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> ValidateQrCodeAsync(string qrData)
    {
        var response = await _httpClient.PostAsJsonAsync("qr/validate-qr", new { QrCodeData = qrData });
        var result = await response.Content.ReadFromJsonAsync<dynamic>();
        return result?.IsValid ?? false;
    }
}
