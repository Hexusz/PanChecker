namespace PanChecker;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            return;
        }

        foreach (var panNumber in args)
        {
            // Если есть не цифры, то пропускаем
            if (!panNumber.All(char.IsDigit))
            {
                continue;
            }
            
            CheckCardNumber(panNumber.Trim());
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