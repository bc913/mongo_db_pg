using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Shine.Backend.Application.Contracts.Repositories;
using Shine.Backend.Persistence.Repositories;
using Shine.Backend.Persistence.Contracts.Settings;
using Shine.Backend.Persistence.Settings;
using Shine.Backend.Core.Entities;
using Shine.Backend.Common;

using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;


namespace Shine.Backend.Persistence
{
    public static class Registration
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {

            ConfigureEntities2();

            services.Configure<UserStoreDbSettings>(
                configuration.GetSection(nameof(UserStoreDbSettings))
            );

            services.AddSingleton<IEntityStoreMongoDbSettings>(
                sp => sp.GetRequiredService<IOptions<UserStoreDbSettings>>().Value
            );

            //MongoDB suggests Singleton registration for repositories
            //services.AddSingleton(typeof(IReadRepository<User>), typeof(UserReadRepository));
            services.AddSingleton(typeof(IReadRepository<>), typeof(MongoDbRepository<>));
            services.AddSingleton(typeof(IRepository<>), typeof(MongoDbRepository<>));

            return services;
        }

        private static void ConfigureEntities()
        {
            // Configuration of the models
            // BsonClassMap.RegisterClassMap<BaseEntity<string>>(cm => 
            // { 
            //     cm.AutoMap();
            //     cm.MapIdMember(c => c.Id)
            //         .SetIdGenerator(StringObjectIdGenerator.Instance);
            //     //.SetIgnoreIfDefault(true);//to generate values on insert

            //     cm.IdMemberMap.SetSerializer(new StringSerializer(BsonType.ObjectId));
            // });

            // BsonClassMap.RegisterClassMap<Entity<string>>(cm =>
            // {
            //     cm.UnmapMember(c => c.DomainEvents);
            // });

            BsonClassMap.RegisterClassMap<User>(cm => 
            { 
                cm.AutoMap();
                cm.MapIdMember(c => c.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance);
                //.SetIgnoreIfDefault(true);//to generate values on insert

                cm.IdMemberMap.SetSerializer(new StringSerializer(BsonType.ObjectId));
                cm.SetIgnoreExtraElements(true);
                //cm.MapMember(c => c.FullName);
            });
        }

        private static void ConfigureEntities2()
        {
            BsonClassMap.RegisterClassMap<Shine.Backend.Common.BaseEntity<string>>(cm => 
            { 
                cm.AutoMap();
                cm.MapIdMember(c => c.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance)
                    .SetIgnoreIfDefault(true);//to generate values on insert

                cm.IdMemberMap.SetSerializer(new StringSerializer(BsonType.ObjectId));
                //cm.SetIgnoreExtraElements(true);
                //cm.MapMember(c => c.FullName);
                cm.SetIsRootClass(true);
                //cm.SetDiscriminatorIsRequired(false);
            });

            BsonClassMap.RegisterClassMap<User>(cm => 
            { 
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
                cm.SetDiscriminatorIsRequired(false);
            });

            BsonSerializer.RegisterDiscriminatorConvention(typeof(User), new Conventions.NoDiscriminatorConvention());
        }
    }
}