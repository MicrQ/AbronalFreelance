using AbronalFreelance.Shared.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AbronalFreelance.Server.Data;
public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Clientt> Clients { get; set; }
    public DbSet<FreelancerEducation> FreelancerEducations { get; set; }
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<FreelancerPortfolio> FreelancerPortfolios { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<FreelancerSkill> FreelancerSkills { get; set; }
    public DbSet<FreelancerPool> FreelancerPools { get; set; }
    public DbSet<JobOffer> JobOffers { get; set; }
    public DbSet<Field> Fields { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<LocationType> LocationTypes { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<Flag> Flags { get; set; }
    public DbSet<Interview> Interviews { get; set; }
    public DbSet<Application> Applications { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<JobType> JobTypes { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<JobPool> JobPools { get; set; }
    public DbSet<PaymentType> PaymentTypes { get; set; }
    public DbSet<ContractStatusType> ContractStatusTypes { get; set; }
    public DbSet<ContractStatus> ContractStatuses { get; set; }
    public DbSet<SkillsForJob> SkillsForJobs { get; set; }
    public DbSet<JobFields> JobFields { get; set; }
    public DbSet<ApplicationStatus> ApplicationStatuses { get; set; }
    public DbSet<JobStatus> JobStatuses { get; set; }
    public DbSet<ApprovalStatus> ApprovalStatuses { get; set; }
    public DbSet<EducationLevel> EducationLevels { get; set; }
    public DbSet<FreelancerField> FreelancerFields { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
         modelBuilder.Entity<Flag>()
            .HasKey(f => f.Id);

        modelBuilder.Entity<Flag>()
            .HasOne(f => f.Reporter)
            .WithMany()
            .HasForeignKey(f => f.ReporterId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Flag>()
            .HasOne(f => f.ReportedUser)
            .WithMany()
            .HasForeignKey(f => f.ReportedUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Feedback>()
            .HasKey(f => f.Id);

        modelBuilder.Entity<Feedback>()
            .HasOne(f => f.Contract)
            .WithMany()
            .HasForeignKey(f => f.ContractId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Feedback>()
            .HasOne(f => f.FromUser)
            .WithMany()
            .HasForeignKey(f => f.FromUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Feedback>()
            .HasOne(f => f.ToUser)
            .WithMany()
            .HasForeignKey(f => f.ToUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<FreelancerPool>()
            .HasKey(fp => fp.Id);

        modelBuilder.Entity<FreelancerPool>()
            .HasOne(fp => fp.Client)
            .WithMany()
            .HasForeignKey(fp => fp.ClientId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<FreelancerPool>()
            .HasOne(fp => fp.Freelancer)
            .WithMany()
            .HasForeignKey(fp => fp.FreelancerId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Message>()
            .HasKey(m => m.Id);

        modelBuilder.Entity<Message>()
            .HasOne(m => m.Sender)
            .WithMany()
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Message>()
            .HasOne(m => m.Receiver)
            .WithMany()
            .HasForeignKey(m => m.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Clientt>()
        .HasOne(c => c.User)
        .WithMany()
        .HasForeignKey(c => c.UserId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<Clientt>()
        .HasOne(c => c.CompanyLocation)
        .WithMany()
        .HasForeignKey(c => c.CompanyLocationId)
        .OnDelete(DeleteBehavior.NoAction);

    modelBuilder.Entity<Application>()
        .HasOne(a => a.Job)
        .WithMany()
        .HasForeignKey(a => a.JobId)
        .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<JobOffer>()
        .HasOne(jo => jo.Freelancer)
        .WithMany()
        .HasForeignKey(jo => jo.FreelancerId)
        .OnDelete(DeleteBehavior.NoAction);

    modelBuilder.Entity<JobPool>()
        .HasOne(jo => jo.Freelancer)
        .WithMany()
        .HasForeignKey(jo => jo.FreelancerId)
        .OnDelete(DeleteBehavior.NoAction);

    modelBuilder.Entity<JobPool>()
        .HasOne(jp => jp.Freelancer)
        .WithMany()
        .HasForeignKey(jp => jp.FreelancerId)
        .OnDelete(DeleteBehavior.Restrict);


        base.OnModelCreating(modelBuilder);
    }


}