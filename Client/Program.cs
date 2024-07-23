using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using AbronalFreelance.Client;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using AbronalFreelance.Client.Services;
using AbronalFreelance.Client.State;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// my services
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthState>();
builder.Services.AddAuthorizationCore();
builder.Services.AddSingleton<LocationService>();

builder.Services.AddScoped<IAccount, AccountService>();
builder.Services.AddBlazoredLocalStorage();

//

await builder.Build().RunAsync();
