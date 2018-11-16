# orderite
Proof of concept order management API using Web API.

## Technologies
.NET Core 2.1 and Web API, using NSwag for documentation and Docker for runtime. Unit and Functional tests use MSTest.

## To run
**Using docker** : `docker-compose up`

**Using VS** : Run Web project

Provided a console app to allow programatic access to the API in addition to Postman collection. If using the Postman collection ensure the port number is mapped.

## Notes
I've approached this project as if it is a pure proof of concept that may be demo'd internally as part of user testing, but crucially would not make it to a production server. For this reason I've gone for a generated client allowing the dev team to rapidly evolve the contract. In a production environment I'd either wrap this generated client or build my own for testability. Possibly may use PACT testing too.

CatalogueService I assumed would be built by another team. Repos are stubs as per specification.
