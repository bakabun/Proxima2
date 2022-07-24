using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((ctx, svc) => svc
        .AddSingleton(new DiscordClient(new DiscordConfiguration
        {
            Token = ctx.Configuration["BotToken"],
            TokenType = TokenType.Bot,
            AutoReconnect = true,
            ReconnectIndefinitely = true,
            MinimumLogLevel = LogLevel.Debug
        }))
        .AddSingleton<HttpClient>())
    // .UseEnvironment("Development")
    .Build();

var client = host.Services.GetRequiredService<DiscordClient>();

var cnext = client.UseCommandsNext(new CommandsNextConfiguration
{
    EnableMentionPrefix = true,
    Services = host.Services
});

client.UseInteractivity(new InteractivityConfiguration
{
    AckPaginationButtons = true
});

await client.ConnectAsync();
await host.RunAsync();