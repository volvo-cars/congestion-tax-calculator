# Congestion Tax Calculator

## Questions and comments

* I couldn't understand why we've created vehicle abstracts. I removed this and created Vehicle entity with a Type property in it.

* Recreated the solution with Clean Architecture (Uncle Bob's) approach and created reusable libraries.

* Domain layer has new serializable types called TimeZone, Time. I didn't want to keep DateTime in database for time scenarios. 

* Used Entity Framework Core / InMemory Database to provide a configurable environment and configured for SE.

* Used mediator pattern (mediatr package) to keep application and facade seperate (Web API).

* Used Commands, Validators and Domain Events.

* Created an Integration Tests project to test all scenarios end to end.

* I used POST HTTP METHOD to eliminate maximum url size limit.