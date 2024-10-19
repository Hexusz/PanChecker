namespace PanChecker;

class Program
{
    static void Main(string[] args)
    {
        var testPanNumbers = new List<string>()
        {
            "4111111111111111",
            "4627100101654724",
            "4486441729154030",
            "4024007123874108",
        };

        foreach (var panNumber in testPanNumbers)
        {
            CheckCardNumber(panNumber);
        }
    }

    static void CheckCardNumber(string panNumber)
    {
        string apiUrl = AppConfig.Host + "/api/testassignments/pan";

        var payload = new
        {
            CardInfo = new
            {
                Pan = panNumber
            }
        };

        // Получаем токен с объектом внутри
        var token = TokenService.GetToken(payload, AppConfig.SharedKey, AppConfig.KeyId);

        // Получаем ответ от сервера
        var responseJson = JwsService.GetCardInfoAsync(apiUrl, token).Result;

        var result = responseJson?.Status == "Success" ? "Successfully" : "Unsuccessfully";

        Console.WriteLine($"{panNumber} {result}");
    }
}