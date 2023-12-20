using System.Text.Json;

namespace DiscordOperaNitroGen;

public class TokenGenerator
{
    private readonly HttpClient _httpClient = new();

    public async Task<string?> CreateAsync()
    {
        var request = GetHttpRequestMessage();
        
        request.Content!.Headers.ContentType!.MediaType = "application/json";

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"Request failed. Status code: {response.StatusCode}");
        
        var responseBody = await response.Content.ReadAsStringAsync();
        var fulfillmentResponse = JsonSerializer.Deserialize<FulfillmentResponse>(responseBody);

        if (fulfillmentResponse is null)
            throw new JsonException("Could not deserialize response");
        
        return fulfillmentResponse.Token;
    }

    private HttpRequestMessage GetHttpRequestMessage()
    {
        return new HttpRequestMessage
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
    }
}