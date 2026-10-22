using Microsoft.EntityFrameworkCore;

namespace Zhomfr.Validation.Rules.Database.Tests.Stubs;

public class StubNotExistDbContext(DbContextOptions<StubDbContext> options) : DbContext(options);
