using Microsoft.Maui.Storage;
using System.Threading.Tasks;

public class TokenService
{
    private const string TokenKey = "user_jwt_token";

    public async Task SaveTokenAsync(string token)
    {
        await SecureStorage.SetAsync(TokenKey, token);
    }

    public async Task<string> GetTokenAsync()
    {
        return await SecureStorage.GetAsync(TokenKey);
    }

    public void ClearToken()
    {
        SecureStorage.Remove(TokenKey);
    }
}
