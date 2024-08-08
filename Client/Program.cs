using Blazored.LocalStorage;
using AbronalFreelance.Client;
using AbronalFreelance.Client.State;
using Microsoft.AspNetCore.Components.Web;
using AbronalFreelance.Client.Services.Auth;
using AbronalFreelance.Client.Services.SkillService;
using AbronalFreelance.Client.Services.FieldService;
using Microsoft.AspNetCore.Components.Authorization;
using AbronalFreelance.Client.Services.CompanyService;
using AbronalFreelance.Client.Services.ProfileService;
using AbronalFreelance.Client.Services.LocationServices;
using AbronalFreelance.Client.Services.PortfolioService;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

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
builder.Services.AddScoped<ICompany, CompanyService>();


builder.Services.AddBlazoredLocalStorage();

//

await builder.Build().RunAsync();
