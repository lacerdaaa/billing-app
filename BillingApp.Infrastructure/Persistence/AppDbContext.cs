using BillingApp.Application.Abstractions;
using BillingApp.Domain.Entities;
using BillingApp.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace BillingApp.Infrastructure.Persistence;

public sealed class AppDbContext : DbContext, IUnitOfWork
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Subscription> Subscriptions => Set<Subscription>();
    public DbSet<Invoice> Invoices => Set<Invoice>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("billing");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    // IUnitOfWork: “commit”
    Task<int> IUnitOfWork.SaveChangesAsync(CancellationToken ct)
        => base.SaveChangesAsync(ct);
}