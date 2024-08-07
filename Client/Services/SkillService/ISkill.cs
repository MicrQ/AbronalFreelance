using AbronalFreelance.Shared.Models;

namespace AbronalFreelance.Client.Services.SkillService;

public interface ISkill
{
    Task<List<Skill>> GetAllSkillsAsync();
}
