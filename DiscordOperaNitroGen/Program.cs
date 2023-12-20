using System.Text.Json;

namespace DiscordOperaNitroGen;

public static class DiscordOperaNitroGen
{
    public static void Main()
    {
        while (true)
        {
            GenerateLink();
        }
    }
    
    private static async void GenerateLink()
    {
        using HttpClient client = new HttpClient();

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://api.discord.gx.games/v1/direct-fulfillment"),
            Headers =
            {
                { "authority", "api.discord.gx.games" },
                { "accept", "*/*" },
                { "accept-language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7" },
                { "origin", "https://www.opera.com" },
                { "referer", "https://www.opera.com/" },
                { "sec-ch-ua", "\"Opera GX\";v=\"105\", \"Chromium\";v=\"119\", \"Not?A_Brand\";v=\"24\"" },
                { "sec-ch-ua-mobile", "?0" },
                { "sec-ch-ua-platform", "\"Windows\"" },
                { "sec-fetch-dest", "empty" },
                { "sec-fetch-mode", "cors" },
                { "sec-fetch-site", "cross-site" },
                { "user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) " +
                                "AppleWebKit/537.36 (KHTML, like Gecko) " +
                                "Chrome/119.0.0.0 Safari/537.36 " +
                                "OPR/105.0.0.0 (Edition Yx GX)" }
            },
            Content = new StringContent("{\"partnerUserId\":\"0\"}")
        };

        if (request.Content.Headers.ContentType != null)
            request.Content.Headers.ContentType.MediaType = "application/json";

        var response = await client.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            var fulfillmentResponse = JsonSerializer.Deserialize<FulfillmentResponse>(responseBody);
    
            Console.WriteLine("https://discord.com/billing/partner-promotions/1180231712274387115/" + fulfillmentResponse.Token.Replace("\n", ""));
        }
        else
        {
            Console.WriteLine($"Request failed. Status code: {response.StatusCode}");
        }
    }
}

