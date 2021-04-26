# DanskeCloudEngineerBankingChallenge

This repository is home to Danske Banking Challenge API and associated projects

## Requirements
- A git tool, like [SourceTree](https://www.sourcetreeapp.com/), to clone this repo.
- An IDE for .NET development: [Visual Studio](https://visualstudio.microsoft.com/downloads/) or [VSCode](https://code.visualstudio.com/download).
- The [.NET Core SDK (3.1+)](https://dotnet.microsoft.com/download) to build the application.

### Run the API
From [Visual Studio](https://visualstudio.microsoft.com/downloads/), you can directly run it and test it on Swagger, since no authorization is required.

### Run the Unit Testing
To test the Challenge, run the test on Danske.Loan.Business.Tests unit test project.

### Unit Tests guidelines:
- Project on the same level of the project that has the method/s class/es to be tested. 
- i.e. Danske.Loan.Business.Managers, added on the same level an xUnit project: Danske.Loan.Business.Tests
- When creating a new test project, FluentAssertions can be added th (through nuget packages) for more natural language assertions (i.e. firstName.Should().Be("John"))
- We use AAA convention for the parts of the test (Arrange, Act, Assert) https://docs.microsoft.com/en-us/visualstudio/test/unit-test-basics?view=vs-2019
- We SUT a.k.a. "System under test" https://en.wikipedia.org/wiki/System_under_test
- Naming of test methods as Should_ExpectedBehavior_When_StateUnderTest (5th on): https://dzone.com/articles/7-popular-unit-test-naming
