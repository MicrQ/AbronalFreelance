using System.Net.Http.Json;
using AbronalFreelance.Shared.DTOs;

namespace AbronalFreelance.Client.Services.ProfileService;

public class ProfileService : IProfile
{
    private readonly HttpClient _http;
    private readonly string _url = "api/user/profile?UserId=";
    public ProfileService(HttpClient http)
    {
        _http = http;
    }

    public async Task<ProfileDTO> GetUserProfileAsync(string UserId)
    {
        ProfileDTO profileDTO = await _http.GetFromJsonAsync<ProfileDTO>(_url + UserId);
        return profileDTO;
    }

    public async Task UpdateProfileAsync(ProfileDTO profileDTO, string UserId)
    {

        Console.WriteLine($"\n\n\n\n\n {profileDTO} \n\n\n\n\n");
        await _http.PutAsJsonAsync(_url + UserId, profileDTO);
    }
}
