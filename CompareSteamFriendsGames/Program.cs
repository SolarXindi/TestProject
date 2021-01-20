using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CompareSteamFriendsGames
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Open",
                    policy => policy
                        .WithOrigins("http://api.steampowered.com", "*")
                        .AllowAnyMethod()
                        .WithHeaders(HeaderNames.AccessControlAllowOrigin)
                        .SetIsOriginAllowedToAllowWildcardSubdomains());
            });

            var defaultHttpClient = new HttpClient
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            };
            defaultHttpClient.DefaultRequestHeaders.Add(HeaderNames.AccessControlAllowOrigin, "http://api.steampowered.com");
            defaultHttpClient.DefaultRequestHeaders.Add(HeaderNames.ContentType, "application/json");

            builder.Services.AddScoped(sp => defaultHttpClient);

            await builder.Build().RunAsync();
        }
    }
}
