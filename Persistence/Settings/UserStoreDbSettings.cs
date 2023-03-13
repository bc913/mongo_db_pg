using Shine.Backend.Persistence.Contracts.Settings;

namespace Shine.Backend.Persistence.Settings
{
    public class UserStoreDbSettings : IEntityStoreMongoDbSettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}