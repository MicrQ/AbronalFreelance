namespace AbronalFreelance.Shared.DTOs;

class ClientProfileDTO {
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? UserName { get; set; }
    public string? Headline { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    // public string? Location { get; set; }

    //

    public string UserId { get; set; }
    public int CompanyLocationId { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string? TinNo { get; set; }
    public int? LocationId { get; set; }
    public string? LocationString { get; set; }
    public DateTime? EstablishedDate { get; set; }

    public bool Flag { get; set; } = false;
    public string? Message { get; set; }
}