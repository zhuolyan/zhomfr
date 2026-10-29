using Zhomfr.Storage.Abstractions;
using Zhomfr.Validation.Rules;

namespace Zhomfr.Storage.Ftp;

/// <inheritdoc/>
public class FtpStorageConfig : IConfig
{
    /// <summary>Base url for access from web to files in storage.</summary>
    [ActiveUrl]
    public string BaseUrl { get; set; } = string.Empty;

    /// <summary>Root dir for storage.</summary>
    [Required]
    [StartsWith("/")]
    [DoesntEndWith("/")]
    public string RootDir { get; set; } = string.Empty;

    /// <summary>Storage FTP host.</summary>
    [Required]
    [Url("ftp", "ftps")]
    public string Host { get; set; } = string.Empty;

    /// <summary>FTP connection port.</summary>
    [Required]
    [Min("1")]
    public int Port { get; set; }

    /// <summary>FTP username..</summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>FTP password.</summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>The retry attempts allowed when a verification failure occurs during download or upload.</summary>
    [Min("1")]
    public int RetryAttempts { get; set; } = 3;

    /// <summary>The length of time in seconds the data channel should wait for the server to send data.</summary>
    [Min("5")]
    public int DataConnectionReadTimeout { get; set; } = 60;

    /// <summary>The length of time in seconds for a data connection to be established before giving up.</summary>
    [Min("5")]
    public int DataConnectionConnectTimeout { get; set; } = 15;

    /// <summary>The length of time wait in seconds for data to be read from the underlying stream.</summary>
    [Min("5")]
    public int ReadTimeout { get; set; } = 60;

    /// <summary>The length of time in seconds to wait for a connection attempt to succeed before giving up.</summary>
    [Min("5")]
    public int ConnectTimeout { get; set; } = 15;

    /// <summary>
    ///     Accept any SSL certificate received from the server and skip performing the validation using the
    ///     ValidateCertificate callback. This applies to data connections too, which leaves transfers open to interception.
    ///     Useful for Powershell users.
    /// </summary>
    public bool ValidateAnyCertificate { get; set; } = true;
}
