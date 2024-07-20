namespace AbronalFreelance.Shared.Models;

public class Interview
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public Application Application { get; set; }
    public DateTime DateTime { get; set; }
    public TimeSpan Duration { get; set; }
    public string LinkForMeeting { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsInterviewed { get; set; }
}
