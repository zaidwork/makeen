using AgencyManager.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgencyManager.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<TeamMember> TeamMembers => Set<TeamMember>();
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Contract> Contracts => Set<Contract>();
    public DbSet<ContractItem> ContractItems => Set<ContractItem>();
    public DbSet<TaskItem> TaskItems => Set<TaskItem>();
    public DbSet<PaymentRecord> PaymentRecords => Set<PaymentRecord>();
    public DbSet<VaultCredential> VaultCredentials => Set<VaultCredential>();
    public DbSet<SubscriptionItem> SubscriptionItems => Set<SubscriptionItem>();
    public DbSet<CompanyExpense> CompanyExpenses => Set<CompanyExpense>();
    public DbSet<BankCheck> BankChecks => Set<BankCheck>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ضبط علاقة العقد بالعميل
        modelBuilder.Entity<Contract>()
            .HasOne(c => c.Client)
            .WithMany(cl => cl.Contracts)
            .HasForeignKey(c => c.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        // ضبط علاقة بنود العقد
        modelBuilder.Entity<ContractItem>()
            .HasOne(ci => ci.Contract)
            .WithMany(c => c.Items)
            .HasForeignKey(ci => ci.ContractId)
            .OnDelete(DeleteBehavior.Cascade);

        // ضبط علاقة المهام بالعقد
        modelBuilder.Entity<TaskItem>()
            .HasOne(t => t.Contract)
            .WithMany(c => c.GeneratedTasks)
            .HasForeignKey(t => t.ContractId)
            .OnDelete(DeleteBehavior.SetNull);

        // ضبط علاقة المهام بعضو الفريق المكلف
        modelBuilder.Entity<TaskItem>()
            .HasOne(t => t.AssignedToUser)
            .WithMany(u => u.AssignedTasks)
            .HasForeignKey(t => t.AssignedToUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // ضبط علاقة سندات الدفع بالعميل
        modelBuilder.Entity<PaymentRecord>()
            .HasOne(p => p.Client)
            .WithMany(cl => cl.Payments)
            .HasForeignKey(p => p.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        // ضبط علاقة سندات الدفع بالعقد
        modelBuilder.Entity<PaymentRecord>()
            .HasOne(p => p.Contract)
            .WithMany(c => c.Payments)
            .HasForeignKey(p => p.ContractId)
            .OnDelete(DeleteBehavior.SetNull);

        // خزنة كلمات المرور مع العميل (اختياري)
        modelBuilder.Entity<VaultCredential>()
            .HasOne(v => v.Client)
            .WithMany(cl => cl.Credentials)
            .HasForeignKey(v => v.ClientId)
            .OnDelete(DeleteBehavior.SetNull);

        // مصاريف الشركة وعضو الفريق
        modelBuilder.Entity<CompanyExpense>()
            .HasOne(e => e.TeamMember)
            .WithMany(m => m.MemberExpenses)
            .HasForeignKey(e => e.TeamMemberId)
            .OnDelete(DeleteBehavior.SetNull);

        // ضبط علاقة الشيكات المصرفية
        modelBuilder.Entity<BankCheck>()
            .HasOne(b => b.Client)
            .WithMany(cl => cl.Checks)
            .HasForeignKey(b => b.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<BankCheck>()
            .HasOne(b => b.Contract)
            .WithMany(c => c.Checks)
            .HasForeignKey(b => b.ContractId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
