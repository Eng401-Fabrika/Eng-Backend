using Eng_Backend.BusinessLayer.Interfaces;
using Eng_Backend.BusinessLayer.Utils;
using Eng_Backend.DtoLayer.Auth;
using Eng_Backend.DAL.Entities; // ApplicationUser burada
using Microsoft.AspNetCore.Identity; // UserManager ve SignInManager için şart

namespace Eng_Backend.BusinessLayer.Managers;

public class AuthManager : IAuthService
{
    // GenericService yerine Identity'nin kendi yöneticilerini kullanıyoruz
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JwtHelper _jwtHelper;

    public AuthManager(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, JwtHelper jwtHelper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtHelper = jwtHelper;
    }

    public async Task<ServiceResult> RegisterAsync(RegisterDto dto)
    {
        // 1. Kullanıcı Nesnesini Oluştur
        var user = new ApplicationUser
        {
            UserName = dto.Email, // Identity'de UserName zorunludur, email yapabiliriz
            Email = dto.Email,
            FullName = dto.FullName,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        // 2. Identity Kütüphanesi ile Kaydet (Şifreyi otomatik hashler ve string olarak saklar)
        // HashingHelper kullanmana gerek YOK.
        var result = await _userManager.CreateAsync(user, dto.Password);

        if (result.Succeeded)
        {
            return ServiceResult.Ok(null, "Kayıt başarıyla oluşturuldu.");
        }

        // Hata varsa (Örn: Şifre çok kısa, Email kullanılıyor vb.)
        // Hataları birleştirip dönüyoruz
        var errorMsg = string.Join(", ", result.Errors.Select(e => e.Description));
        return ServiceResult.Fail(errorMsg);
    }

    public async Task<ServiceResult> LoginAsync(LoginDto dto)
    {
        // 1. Kullanıcıyı Email ile bul
        var user = await _userManager.FindByEmailAsync(dto.Email);
        
        if (user == null)
            return ServiceResult.Fail("Kullanıcı bulunamadı.");

        // 2. Şifre Doğrulama (Identity kendi içinde string hash'i çözer ve kontrol eder)
        // VerifyPasswordHash metoduna gerek YOK.
        var checkPassword = await _userManager.CheckPasswordAsync(user, dto.Password);

        if (!checkPassword)
            return ServiceResult.Fail("Şifre hatalı.");

        // 3. Token Üretme
        // DİKKAT: JwtHelper sınıfının CreateToken metodunun parametresini 
        // "User" yerine "ApplicationUser" alacak şekilde güncellemen gerekebilir.
        string token = _jwtHelper.CreateToken(user);

        return ServiceResult.Ok(new { Token = token }, "Giriş başarılı.");
    }
}