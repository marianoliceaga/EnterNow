using EnterNow.API.Models;
using EnterNow.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EnterNow.API.Controllers
{
    [Route("api/qr")]
    [ApiController]
    [Authorize]
    public class QRServiceController : Controller
    {
        private readonly IQrCodeService _qrCodeService;
        private readonly UserManager<ApplicationUser> _userManager;

        public QRServiceController(IQrCodeService qrCodeService, UserManager<ApplicationUser> userManager)
        {
            _qrCodeService = qrCodeService;
            _userManager = userManager;
        }

        [HttpGet("generate-qr")]
        public async Task<IActionResult> GenerateQr()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return Unauthorized();

            if (user.MembershipExpiry < DateTime.UtcNow || !user.IsPaymentCurrent)
                return Unauthorized("Membership expired or payment not current.");

            string qrCodeBase64 = _qrCodeService.GenerateQrCode(user.Id, user.MembershipExpiry);
            return Ok(new { QrCodeImage = $"data:image/png;base64,{qrCodeBase64}" });
        }

        [HttpPost("validate-qr")]
        public IActionResult ValidateQr([FromBody] ValidateQrRequest request)
        {
            bool isValid = _qrCodeService.ValidateQrCode(request.QrCodeData);
            return Ok(new { IsValid = isValid });
        }
    }

    public class ValidateQrRequest
    {
        public string QrCodeData { get; set; }
    }
}
