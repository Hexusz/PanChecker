using System.Text;
using JWT.Algorithms;
using JWT.Builder;
using Newtonsoft.Json;
using PanChecker.Models;

namespace PanChecker;

public static class JwsService
{
    /// <summary>
    /// Отправляет JWT токен на указанный API, получает ответ,
    /// декодирует и десериализует её в объект JwsCardInfoResponse.
    /// </summary>
    /// <param name="apiUrl">URL API, на который будет отправлен запрос.</param>
    /// <param name="jwsToken">JWT токен, который будет отправлен в запросе.</param>
    /// <returns>Объект содержащий информацию о карте.</returns>
    public static async Task<JwsCardInfoResponse?> GetCardInfoAsync(string apiUrl, string jwsToken)
    {
        using (var client = new HttpClient())
        {
            var content = new StringContent(jwsToken, Encoding.UTF8, "application/jwt");

            HttpResponseMessage response = await client.PostAsync(apiUrl, content);
            response.EnsureSuccessStatusCode();

            // Получаем кодированный JWT
            var responseBody = await response.Content.ReadAsStringAsync();

            // Декодируем
            var json = JwtBuilder.Create()
                .WithAlgorithm(new HMACSHA256Algorithm())
                .DoNotVerifySignature()
                .Decode(responseBody);
            
            // Десериализуем
            var cardInfo = JsonConvert.DeserializeObject<JwsCardInfoResponse>(json);

            return cardInfo;
        }
    }
}