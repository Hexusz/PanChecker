using PanChecker.Models.JWS;

namespace PanChecker.Models;

public class JwsCardInfoResponse
{
    public string Id { get; set; }
    public CardInfo CardInfo { get; set; }
    public string Status { get; set; }
}