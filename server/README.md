### IMPROVEMENTS

The project is a .NET Core Web API project
Converting the project into a .NET Core Web API was done to make it simpler and more robust.
Cross-platform capabailities, better performance and scalability, and containerization are one of the major technical reasons.
And because the role I am applying for is a .NET Core focused role, this hopefully shows how .NET Framework projects can be converted into .NET Core

Clean Architecture was used to structure the solution with Domain Driven Design serving as an inspiration.
The aim for me was to try and create a common language between technical teams and business teams and this architecture is the perfect for it.
Having a common language allows for better communication (removes barriers), and not just in IT but in all walks of life too.
When tech and non-tech teams discuss, this allows them to talk in domain concepts (Core) instead of translating business terms into technical terms and vice-versa

There are 4 layers: Web, Infrastructure, Core, Unit.Tests
Core is where the domain (business logic) lies
Infrastructure houses the data access layer 
Web is where the API controllers are, including the request/response objects
Unit.Tests is where unit tests can be found. At the moment, only a unit tests for ProductController is present but we would add more here as needed

The architecture dependencies look like as follows (excluding Unit.Tests):

Web -> Core, Infrastructure
Infrastructure -> Core
Core -> N/A

### THINGS THAT NEED TO BE DONE

1. ProductOption should probably be a ValueObject and not an Entity, but will need some discussions with business to confirm what its actual role is
2. Implementing a full-blown Repository-UnitOfWork pattern would take a bit of time to do so only Repositories were created for this example.
   That being said, it would be great to discuss with the team what other options we can use to implement the pattern as creating it from scratch is a difficult task.
   Would it be wise to use full-blown ORMs instead?
   This is especially significant when dealing with banking integrations as all transactions mmust exhibit ACID (Atomicity, Consistency, Isolation, Durability)
3. Request and Response objects could have been done differently and a little bit better.
   Perhaps using AutoMapper would help but it would be great to discuss other options and possibilities.
4. API versioning is also something that needs to be taken a look at.
   We have a couple of options such as creating new controllers for new endpoints or updating version number of a route but using the same controller
   Discussing this with the team would be the best as it would probably be wise to go with the current standard being implemented in a project
5. Implementing Swagger would be also be good for testing endpoints
6. GetProductById and GetProductOptionByProductOptionId needs to return an empty JSON object {} instead of default response values
7. More documentation (docblocks, etc.) needs to be done

### NUGET PACKAGES USED

1. Dapper (database)
2. XUnit (unit tests)
3. FakeItEasy (unit tests)

### NOTES

1. Some documentation is present on important parts of the app such as class summaries explaining its purpose and intent
2. Some lines are marked with TODO, indicating that this must be changed/improved
3. Some lines are marked with NOTE, explaining why a specific code is being done
4. APIs start with /api so instead of /products, it becomes /api/products (good practice to do this to distinguish API endpoints)