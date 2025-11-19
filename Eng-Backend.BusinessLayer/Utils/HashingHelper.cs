using System.Security.Cryptography;
using System.Text;

namespace Eng_Backend.BusinessLayer.Utils;

public static class HashingHelper
{
    // Şifreyi oluştururken (Register) kullanılır
    public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key; // Rastgele bir tuz (key) oluşturur
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)); // Şifreyi karıştırır
    }

    // Giriş yaparken (Login) şifreyi doğrulamak için kullanılır
    public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512(passwordSalt); // Kayıt olurken kullanılan aynı tuzu kullanır
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        // Hesaplanan hash ile veritabanındaki hash aynı mı?
        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != passwordHash[i])
                return false;
        }
        return true;
    }
}