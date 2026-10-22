using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

namespace Zhomfr.Validation.Rules.Database;

/// <summary>
///     Validates that the value is unique within the specified database table and column. This rule fails if a record
///     with the same property value already exists.
/// </summary>
/// <typeparam name="TDbContext">The type of the <see cref="DbContext"/> to use for the check.</typeparam>
/// <typeparam name="TType">The entity type (class) in which the uniqueness is checked.</typeparam>
/// <param name="propertyName">The name of the property (column) that must contain unique values.</param>
/// ///
/// <example>
///     <code>
///         public class StubModel
///         {
///             [Unique&lt;StubDbContext,StubEntity&gt;(nameof(StubEntity.Name))]
///             public string Name { get; set; } = "user@example.com";
///         }
///     </code>
/// </example>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class UniqueAttribute<TDbContext, TType>(string propertyName) : ExistAttribute<TDbContext, TType>(propertyName)
    where TDbContext : DbContext where TType : class
{
    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        return !base.CheckCondition(value, context);
    }
}
