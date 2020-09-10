namespace leashed.helpers
{
    public interface IDbInitializer
    {
        /// Applies any pending migrations for the context to the database.
        /// Will create the database if it does not already exist.
        void Initialize();

        /// Adds some default values to the Db
        void SeedData();       
    }
}