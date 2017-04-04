namespace Wilson.Companies.Core.Interfaces
{
    /// <summary>
    /// Contains settings for the application.
    /// </summary>
    public interface ISettings
    {
        /// <summary>
        /// Checks if database is installed.
        /// </summary>
        bool IsDatabaseInstalled { get; set; }

        /// <summary>
        /// Contains the application Main/Home Company ID.
        /// </summary>
        string HomeCompanyId { get; set; }
    }
}
