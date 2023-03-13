using System;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization.Conventions;

namespace Shine.Backend.Persistence.Conventions
{
    /*
    https://stackoverflow.com/questions/63603439/how-to-disable-discriminator-field-in-mongodb-c-driver
    https://github.com/mongodb/mongo-csharp-driver/blob/v1.x/MongoDB.DriverUnitTests/Samples/MagicDiscriminatorTests.cs
    https://www.titanwolf.org/Network/q/dc186bdb-4cc8-4f3a-a178-086022a2cd25/y
    */
    public class NoDiscriminatorConvention : IDiscriminatorConvention
    {
        public string ElementName => null;

        public Type GetActualType(IBsonReader bsonReader, Type nominalType)
        {
            return nominalType;
        }

        public BsonValue GetDiscriminator(Type nominalType, Type actualType)
        {
            return null;
        }
    }
}