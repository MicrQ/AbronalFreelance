namespace AbronalFreelance.Shared.Models;

public class ContractStatus
{
    public int Id { get; set; }
    public int ContractId { get; set; }
    public Contract Contract { get; set; }
     public int ContractStatusTypeId { get; set; }
    public ContractStatusType ContractStatusType { get; set; }
    public DateTime DateTime { get; set; }
    public bool IsActive { get; set; }
}
