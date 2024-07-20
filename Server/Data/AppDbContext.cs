using AbronalFreelance.Shared.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Client> Clients { get; set; }
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
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<ContractStatusType> ContractStatusTypes { get; set; }
    public DbSet<ContractStatus> ContractStatuses { get; set; }
    public DbSet<SkillsForJob> SkillsForJobs { get; set; }
    public DbSet<JobFields> JobFields { get; set; }
    public DbSet<ApplicationStatus> ApplicationStatuses { get; set; }
    public DbSet<JobStatus> JobStatuses { get; set; }
    public DbSet<ApprovalStatus> ApprovalStatuses { get; set; }
    public DbSet<EducationLevel> EducationLevels { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationUser>()
            .HasOne<Client>(a => a.Client)
            .WithOne(c => c.User)
            .HasForeignKey<Client>(c => c.UserId);

        modelBuilder.Entity<ApplicationUser>()
            .HasOne<Profile>(a => a.Profile)
            .WithOne(p => p.User)
            .HasForeignKey<Profile>(p => p.UserId);

        modelBuilder.Entity<ApplicationUser>()
            .HasMany<Message>(a => a.SentMessages)
            .WithOne(m => m.Sender)
            .HasForeignKey(m => m.SenderId);

        modelBuilder.Entity<ApplicationUser>()
            .HasMany<Message>(a => a.ReceivedMessages)
            .WithOne(m => m.Receiver)
            .HasForeignKey(m => m.ReceiverId);

        // Client relationships
        modelBuilder.Entity<Client>()
            .HasOne<Location>(c => c.Location)
            .WithMany()
            .HasForeignKey(c => c.LocationId);

        // FreelancerEducation relationships
        modelBuilder.Entity<FreelancerEducation>()
            .HasOne<ApplicationUser>(fe => fe.User)
            .WithMany(u => u.FreelancerEducations)
            .HasForeignKey(fe => fe.UserId);

        modelBuilder.Entity<FreelancerEducation>()
            .HasOne<Field>(fe => fe.Field)
            .WithMany()
            .HasForeignKey(fe => fe.FieldId);

        modelBuilder.Entity<FreelancerEducation>()
            .HasOne<EducationLevel>(fe => fe.EducationLevel)
            .WithMany()
            .HasForeignKey(fe => fe.EducationLevelId);

        // FreelancerPortfolio relationships
        modelBuilder.Entity<FreelancerPortfolio>()
            .HasOne<ApplicationUser>(fp => fp.Freelancer)
            .WithMany(u => u.FreelancerPortfolios)
            .HasForeignKey(fp => fp.FreelancerId);

        // FreelancerSkill relationships
        modelBuilder.Entity<FreelancerSkill>()
            .HasOne<ApplicationUser>(fs => fs.User)
            .WithMany(u => u.FreelancerSkills)
            .HasForeignKey(fs => fs.UserId);

        modelBuilder.Entity<FreelancerSkill>()
            .HasOne<Skill>(fs => fs.Skill)
            .WithMany()
            .HasForeignKey(fs => fs.SkillId);

        // FreelancerPool relationships
        modelBuilder.Entity<FreelancerPool>()
            .HasOne<ApplicationUser>(fp => fp.Client)
            .WithMany(u => u.FreelancerPoolsAsClient)
            .HasForeignKey(fp => fp.ClientId);

        modelBuilder.Entity<FreelancerPool>()
            .HasOne<ApplicationUser>(fp => fp.Freelancer)
            .WithMany(u => u.FreelancerPoolsAsFreelancer)
            .HasForeignKey(fp => fp.FreelancerId);

        // JobOffer relationships
        modelBuilder.Entity<JobOffer>()
            .HasOne<Job>(jo => jo.Job)
            .WithMany(j => j.JobOffers)
            .HasForeignKey(jo => jo.JobId);

        modelBuilder.Entity<JobOffer>()
            .HasOne<ApplicationUser>(jo => jo.Freelancer)
            .WithMany(u => u.JobOffers)
            .HasForeignKey(jo => jo.FreelancerId);

        // Message relationships
        modelBuilder.Entity<Message>()
            .HasOne<ApplicationUser>(m => m.Sender)
            .WithMany(u => u.SentMessages)
            .HasForeignKey(m => m.SenderId);

        modelBuilder.Entity<Message>()
            .HasOne<ApplicationUser>(m => m.Receiver)
            .WithMany(u => u.ReceivedMessages)
            .HasForeignKey(m => m.ReceiverId);

        // Feedback relationships
        modelBuilder.Entity<Feedback>()
            .HasOne<Contract>(f => f.Contract)
            .WithMany(c => c.Feedbacks)
            .HasForeignKey(f => f.ContractId);

        modelBuilder.Entity<Feedback>()
            .HasOne<ApplicationUser>(f => f.FromUser)
            .WithMany(u => u.FeedbacksGiven)
            .HasForeignKey(f => f.FromUserId);

        modelBuilder.Entity<Feedback>()
            .HasOne<ApplicationUser>(f => f.ToUser)
            .WithMany(u => u.FeedbacksReceived)
            .HasForeignKey(f => f.ToUserId);

        // Flag relationships
        modelBuilder.Entity<Flag>()
            .HasOne<ApplicationUser>(f => f.Reporter)
            .WithMany(u => u.FlagsReported)
            .HasForeignKey(f => f.ReporterId);

        modelBuilder.Entity<Flag>()
            .HasOne<ApplicationUser>(f => f.ReportedUser)
            .WithMany(u => u.FlagsReceived)
            .HasForeignKey(f => f.ReportedUserId);

        // Interview relationships
        modelBuilder.Entity<Interview>()
            .HasOne<Application>(i => i.Application)
            .WithMany(a => a.Interviews)
            .HasForeignKey(i => i.ApplicationId);

        // Application relationships
        modelBuilder.Entity<Application>()
            .HasOne<ApplicationUser>(a => a.Freelancer)
            .WithMany(u => u.Applications)
            .HasForeignKey(a => a.FreelancerId);

        modelBuilder.Entity<Application>()
            .HasOne<Job>(a => a.Job)
            .WithMany(j => j.Applications)
            .HasForeignKey(a => a.JobId);

        // Job relationships
        modelBuilder.Entity<Job>()
            .HasOne<ApplicationUser>(j => j.CreatedByUser)
            .WithMany(u => u.CreatedJobs)
            .HasForeignKey(j => j.CreatedBy);

        modelBuilder.Entity<Job>()
            .HasOne<Location>(j => j.Location)
            .WithMany()
            .HasForeignKey(j => j.LocationId);

        modelBuilder.Entity<Job>()
            .HasOne<PaymentType>(j => j.PaymentType)
            .WithMany()
            .HasForeignKey(j => j.PaymentTypeId);

        modelBuilder.Entity<Job>()
            .HasOne<JobType>(j => j.JobType)
            .WithMany()
            .HasForeignKey(j => j.JobTypeId);

        // Contract relationships
        modelBuilder.Entity<Contract>()
            .HasOne<Application>(c => c.Application)
            .WithMany(a => a.Contracts)
            .HasForeignKey(c => c.ApplicationId);

        // JobPool relationships
        modelBuilder.Entity<JobPool>()
            .HasOne<Job>(jp => jp.Job)
            .WithMany(j => j.JobPools)
            .HasForeignKey(jp => jp.JobId);

        modelBuilder.Entity<JobPool>()
            .HasOne<ApplicationUser>(jp => jp.Freelancer)
            .WithMany(u => u.JobPools)
            .HasForeignKey(jp => jp.FreelancerId);

        // PaymentType relationships
        modelBuilder.Entity<PaymentType>()
            .HasOne<PaymentMethod>(pt => pt.PaymentMethod)
            .WithMany()
            .HasForeignKey(pt => pt.PaymentMethodId);

        // ContractStatus relationships
        modelBuilder.Entity<ContractStatus>()
            .HasOne<Contract>(cs => cs.Contract)
            .WithMany(c => c.ContractStatuses)
            .HasForeignKey(cs => cs.ContractId);

        modelBuilder.Entity<ContractStatus>()
            .HasOne<ContractStatusType>(cs => cs.ContractStatusType)
            .WithMany()
            .HasForeignKey(cs => cs.ContractStatusTypeId);

        // SkillsForJob relationships
        modelBuilder.Entity<SkillsForJob>()
            .HasOne<Job>(sfj => sfj.Job)
            .WithMany(j => j.SkillsForJobs)
            .HasForeignKey(sfj => sfj.JobId);

        modelBuilder.Entity<SkillsForJob>()
            .HasOne<Skill>(sfj => sfj.Skill)
            .WithMany()
            .HasForeignKey(sfj => sfj.SkillId);

        // JobFields relationships
        modelBuilder.Entity<JobFields>()
            .HasOne<Job>(jf => jf.Job)
            .WithMany(j => j.JobFields)
            .HasForeignKey(jf => jf.JobId);

        modelBuilder.Entity<JobFields>()
            .HasOne<Field>(jf => jf.Field)
            .WithMany()
            .HasForeignKey(jf => jf.FieldId);

        // ApplicationStatus relationships
        modelBuilder.Entity<ApplicationStatus>()
            .HasOne<Application>(as => as.Application)
            .WithMany(a => a.ApplicationStatuses)
            .HasForeignKey(as => as.ApplicationId);

        modelBuilder.Entity<ApplicationStatus>()
            .HasOne<ApprovalStatus>(as => as.ApprovalStatus)
            .WithMany()
            .HasForeignKey(as => as.ApprovalStatusId);

        // JobStatus relationships
        modelBuilder.Entity<JobStatus>()
            .HasOne<Job>(js => js.Job)
            .WithMany(j => j.JobStatuses)
            .HasForeignKey(js => js.JobId);

        modelBuilder.Entity<JobStatus>()
            .HasOne<ApprovalStatus>(js => js.ApprovalStatus)
            .WithMany()
            .HasForeignKey(js => js.ApprovalStatusId);

        // EducationLevel relationships
        modelBuilder.Entity<EducationLevel>()
            .HasMany<FreelancerEducation>(el => el.FreelancerEducations)
            .WithOne(fe => fe.EducationLevel)
            .HasForeignKey(fe => fe.EducationLevelId);
    }
}
