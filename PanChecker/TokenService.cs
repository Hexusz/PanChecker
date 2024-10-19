using JWT;
using JWT.Algorithms;
using JWT.Serializers;

namespace PanChecker;

public static class TokenService
{
    /// <summary>
    /// Генерирует JWT токен с использованием объекта и ключа.
    /// </summary>
    /// <param name="payload">Объект который будет в JWT.</param>
    /// <param name="sharedKey">Ключ для подписи JWT в формате Base64.</param>
    /// <param name="keyId">Идентификатор ключа.</param>
    /// <returns>Сгенерированный JWT токен в виде строки.</returns>
    public static string GetToken(object payload, string sharedKey, string keyId)
    {
        var key = Convert.FromBase64String(sharedKey);

        IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
        IJsonSerializer serializer = new JsonNetSerializer();
        IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
        IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

        string signdate = DateTime.UtcNow.ToString("O");

        var extraHeaders = new Dictionary<string, object>
        {
            { "kid", keyId },
            { "cty", "application/json" },
            { "signdate", signdate }
        };

        return encoder.Encode(extraHeaders, payload, key);
    }
}