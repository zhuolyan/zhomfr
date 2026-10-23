namespace Zhomfr.Hash.Abstractions;

/// <summary>Defines methods for secure password hashing and verification.</summary>
public interface IHash
{
    /// <summary>Creates a secure hash for the specified password.</summary>
    /// <param name="password">The plain-text password to be hashed.</param>
    /// <returns>
    ///     A string containing the hash along with metadata (version, salt, and algorithm parameters) for subsequent
    ///     storage in a database.
    /// </returns>
    string Make(string password);

    /// <summary>Verifies that the provided password matches the stored hash.</summary>
    /// <param name="password">The plain-text password to verify.</param>
    /// <param name="storedHash">The full hash string previously generated via the <see cref="Make"/> method.</param>
    /// <returns>True if the password is correct; otherwise, false.</returns>
    /// <remarks>The implementation should use fixed-time comparison to prevent side-channel timing attacks.</remarks>
    bool Check(string password, string storedHash);

    /// <summary>Determines if the password hash needs to be updated due to changes in security settings.</summary>
    /// <param name="storedHash">The current hash from the data store.</param>
    /// <returns>
    ///     True if the system's algorithm parameters (e.g., MemorySize or Iterations) differ from those embedded in the
    ///     hash.
    /// </returns>
    /// <remarks>
    ///     If this method returns true, it is recommended to re-create the hash using <see cref="Make"/> during the
    ///     user's next successful authentication.
    /// </remarks>
    bool NeedsRehash(string storedHash);
}
