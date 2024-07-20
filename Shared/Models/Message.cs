namespace AbronalFreelance.Shared.Models;

public class Message
{
    public int Id { get; set; }
    public string Content { get; set; }
    public string SenderId { get; set; }
    public User Sender { get; set; }
    public string ReceiverId { get; set; }
    public User Receiver { get; set; }
    public DateTime Timestamp { get; set; }
    public bool IsSeen { get; set; }
}
