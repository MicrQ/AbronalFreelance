using System.ComponentModel.DataAnnotations.Schema;

namespace AbronalFreelance.Shared.Models;

public class ContractStatus
{
    public int Id { get; set; }
    public int ContractId { get; set; }
     public int ApprovalStatusId { get; set; }
    public DateTime DateTime { get; set; }

    [ForeignKey("ContractId")]
    public Contract Contract { get; set; }
    [ForeignKey("ApprovalStatusId")]
    public ApprovalStatus ApprovalStatus { get; set; }
}
