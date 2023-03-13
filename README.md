# ASP.NET Core - MongoDb

## Installation - Setup
### MacOs
Refer to this [link](https://docs.mongodb.com/manual/tutorial/install-mongodb-on-os-x/)

1. Open a terminal and run the following command to start as a MacOs service
```bash
brew services start mongodb-community@5.0
```
2. Verify it is running
```bash
brew services list
```

3. Open a new tab and start a mongodb shell instance
```bash
mongosh
```

## Mapping
- Configure mappings before registering the services.
- Properties are included to the automapping when `AutoMap()` is called.
- However, Private readonly members are not automapped by default so include the member as follows.
```csharp
public class MyClass
{
    private readonly string _someProp;
    public string SomeProp => _someProp;
}

// Later in the code before initializing the mongodb collection
BsonClassMap.RegisterClassMap<MyClass>(cm => {
    cm.MapProperty(c => c.SomeProp);
});
```

### Id
During construction of the entities alongside with the queries, the Id member is implicitly default initialized and then the recorded value is assigned.

#### String
```csharp
BsonClassMap.RegisterClassMap<User>(cm => 
{ 
    cm.AutoMap();
    cm.MapIdMember(c => c.Id)                    
        .SetIdGenerator(StringObjectIdGenerator.Instance);
        //.SetIgnoreIfDefault(true)//to generate values on insert
        
    cm.IdMemberMap.SetSerializer(new StringSerializer(BsonType.ObjectId));
    //cm.MapMember(c => c.FullName);
});
```
### Id generation
https://stackoverflow.com/questions/42349063/use-objectid-generatenewid-or-leave-mongodb-to-create-one

https://www.code4it.dev/blog/mongodb-introduction-with-csharp


## Serialization
https://mongodb.github.io/mongo-csharp-driver/1.11/serialization/

## [Filters - Projections - UpdateDefinitions - Sort](https://mongodb.github.io/mongo-csharp-driver/2.13/reference/driver/definitions/)

### Filters
```csharp
// Type can be generic or interface or implementation
var filterBuilder = Builders<User>.Filter;
var filter = filter.Eq(doc => doc.Id, queryId);
```

### Projections
By default, queries in MongoDB return all fields in matching documents. To limit the amount of data that MongoDB sends to applications, you can include a projection document to specify or restrict fields to return.

```csharp
/*
https://docs.mongodb.com/manual/tutorial/project-fields-from-query-results/
*/
class Widget
{
    public ObjectId Id { get; set; }

    [BsonElement("x")]
    public int X { get; set; }

    [BsonElement("y")]
    public int Y { get; set; }
}

var projectionBuilder = Builders<Widget>.Projection;

// do projection
var projection = projectionBuilder.Include("X").Include("Y").Exclude("Id");
var projection = projectionBuilder.Include("x").Include("y").Exclude("_id");
var projection = projectionBuilder.Include(x => x.X).Include(x => x.Y).Exclude(x => x.Id);
var projection = projectionBuilder.Expression(x => new { X = x.X, Y = x.Y });
```

### Update definition
```csharp
/*
https://chsakell.gitbook.io/mongodb-csharp-docs/getting-started/quick-start/update-documents
https://stackoverflow.com/questions/49346654/c-sharp-mongodb-driver-how-to-use-updatedefinitionbuilder

*/
class Widget
{
    [BsonElement("x")]
    public int X { get; set; }

    [BsonElement("y")]
    public int Y { get; set; }

    [BsonElement("z")]
    public int Z { get; set; }
}

var updateDefBuilder = Builders<Widget>.Update;


var update = updateDefBuilder.Set(widget => widget.X, 1).Set(widget => widget.Y, 3).Inc(widget => widget.Z, 1);
var update = updateDefBuilder.Set("X", 1).Set("Y", 3).Inc("Z", 1);
var update = updateDefBuilder.Set("x", 1).Set("y", 3).Inc("z", 1);
```

### Sort
```csharp
class Widget
{
    [BsonElement("x")]
    public int X { get; set; }

    [BsonElement("y")]
    public int Y { get; set; }
}

var builder = Builders<Widget>.Sort;
var sort = builder.Ascending(x => x.X).Descending(x => x.Y);
var sort = builder.Ascending("X").Descending("Y");
var sort = builder.Ascending("x").Descending("y");
```

## CRUD Operations
https://mongodb.github.io/mongo-csharp-driver/2.13/reference/driver/crud/
https://mongodb.github.io/mongo-csharp-driver/2.14/reference/driver/crud/writing/
https://chsakell.gitbook.io/mongodb-csharp-docs/crud-basics/create-documents
https://darchuk.net/2018/08/31/c-and-mongo-findoneandupdateasync/

## Clean Architecture
https://medium.com/aspnetrun/build-catalog-microservice-using-asp-net-core-mongodb-and-docker-container-88b8fd4d5040
https://www.hosting.work/aspnet-core-microservice-web-api-crud-mongodb/


## Async API
- [https://www.mongodb.com/blog/post/introducing-20-net-driver](https://www.mongodb.com/blog/post/introducing-20-net-driver)

## References
- https://chsakell.gitbook.io/mongodb-csharp-docs/
- https://kevsoft.net/2020/06/25/storing-guids-as-strings-in-mongodb-with-csharp.html
- https://mongodb-documentation.readthedocs.io/en/latest/ecosystem/tutorial/serialize-documents-with-the-csharp-driver.html#gsc.tab=0
- https://www.codewrecks.com/post/old/2016/04/change-how-mongodb-c-driver-serialize-guid-in-new-driver-version/
- https://chsakell.gitbook.io/mongodb-csharp-docs/crud-basics/create-documents/id-member
- https://digitteck.com/mongo-csharp/value-object-and-mongo-serializer/
- https://docs.mongodb.com/realm/sdk/dotnet/data-types/embedded-objects/
