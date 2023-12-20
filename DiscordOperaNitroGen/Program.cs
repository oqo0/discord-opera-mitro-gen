using DiscordOperaNitroGen;

var tokenGenerator = new TokenGenerator();

string url = "https://discord.com/billing/partner-promotions/1180231712274387115/";

while (true)
{
    Console.WriteLine(url + await tokenGenerator.CreateAsync());
}