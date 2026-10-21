using Microsoft.EntityFrameworkCore;

namespace Zhomfr.Validation.Rules.Database.Tests.Stubs;

public class StubDbContext(DbContextOptions<StubDbContext> options) : DbContext(options)
{
    public DbSet<StubModel> StubEntities { get; set; }
}
