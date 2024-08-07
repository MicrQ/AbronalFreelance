using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using AbronalFreelance.Client;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using AbronalFreelance.Client.Services.LocationServices;
using AbronalFreelance.Client.State;
using AbronalFreelance.Client.Services.Auth;
using AbronalFreelance.Client.Services.ProfileService;
using AbronalFreelance.Client.Services.PortfolioService;
using AbronalFreelance.Client.Services.FieldService;
using AbronalFreelance.Client.Services.SkillService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// my services
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthState>();
builder.Services.AddAuthorizationCore();
// builder.Services.AddSingleton<LocationService>();
// builder.Services.AddScoped<LocationService>();


builder.Services.AddScoped<IAccount, AccountService>();
builder.Services.AddScoped<ILocation, LocationService>();
builder.Services.AddScoped<IProfile, ProfileService>();
builder.Services.AddScoped<IPortfolio, PortfolioService>();
builder.Services.AddScoped<IField, FieldService>();
builder.Services.AddScoped<ISkill, SkillService>();


builder.Services.AddBlazoredLocalStorage();

//

await builder.Build().RunAsync();
