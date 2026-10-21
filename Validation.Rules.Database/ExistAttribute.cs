using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules.Database;

/// <summary>
///     Validates that a record with the specified property value exists in the database. Supports any entity type and
///     property that can be resolved via <see cref="EF.Property{T}"/>.
/// </summary>
/// <typeparam name="TDbContext">The type of the <see cref="DbContext"/> to use for the check.</typeparam>
/// <typeparam name="TType">The entity type (class) in which to look for the value.</typeparam>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class ExistAttribute<TDbContext, TType> : AbstractRule where TDbContext : DbContext where TType : class
{
    private readonly string _propertyName;

    /// <summary>
    ///     Validates that a record with the specified property value exists in the database. Supports any entity type and
    ///     property that can be resolved via <see cref="EF.Property{T}"/>.
    /// </summary>
    /// <param name="propertyName">The name of the property (column) to check against.</param>
    public ExistAttribute(string propertyName)
    {
        this._propertyName = propertyName;

        this.MessageArgs.Add($"\"{propertyName}\"");
    }

    private static string ExceptionMessage => $"{typeof(TDbContext).FullName} does not exist in the service provider.";

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException">If not exists DbContext.</exception>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        TDbContext dbContext = context.GetService<TDbContext>() ?? throw new ArgumentNullException(ExistAttribute<TDbContext, TType>.ExceptionMessage);

        return dbContext.Set<TType>().Any(e => EF.Property<object>(e, this._propertyName).Equals(value));
    }
}
