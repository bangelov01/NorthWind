# General instructions

## Think Before Coding

**Don't assume. Don't hide confusion. Surface tradeoffs.**

Before implementing:
- State your assumptions explicitly. If uncertain, ask.
- If multiple interpretations exist, present them - don't pick silently.
- If a simpler approach exists, say so. Push back when warranted.
- If something is unclear, stop. Name what's confusing. Ask.

## Simplicity First

**Minimum code that solves the problem. Nothing speculative.**

- No features beyond what was asked.
- No abstractions for single-use code.
- No "flexibility" or "configurability" that wasn't requested.
- No error handling for impossible scenarios.
- If you write 200 lines and it could be 50, rewrite it.

Ask yourself: "Would a senior engineer say this is overcomplicated?" If yes, simplify.

## Surgical Changes

When editing existing code:
- Don't "improve" adjacent code, comments, or formatting.
- Don't refactor things that aren't broken.
- Match existing style, even if you'd do it differently.
- If you notice unrelated dead code, mention it - don't delete it.

When your changes create orphans:
- Remove usages/imports/variables/functions that YOUR changes made unused.
- Don't remove pre-existing dead code unless asked.

The test: Every changed line should trace directly to the user's request.

# NorthWind .NET project architecture overview

## Solution: 
- `NorthWind.sln` (target: `net10.0`)

## Projects:

### `NorthWind.Api/` — ASP.NET Core Web API (entry point)
- `Program.cs` — app bootstrap, DI registration
- `Rest/Customer/` — api controllers ( example: `CustomersController`), request models (example: `CustomerSearchCriteria`), FluentValidation validators (example: `CustomerSearchCriteriaValidator`)
- `Middleware/` — middleware configurations (``GlobalExceptionHandler``)
- `Extensions/ServiceCollectionExtensions.cs` — API-layer DI wiring
- `appsettings*.json` — configuration

## `NorthWind.Services/` — Business Logic Layer
- `Customer/` — location for services (example: `ICustomerService`,`CustomerService`); `Dto/` for outbound dtos
- `Extensions/ServiceCollectionExtensions.cs` — service-layer DI wiring
- Internals visible to `NorthWind.Services.Tests`

## `NorthWind.Infrastructure/` — Data Access Layer
- `Persistance/Generated/` — EF Core scaffold output: `NorthWindDbContext.cs` and entity classes under `Entities/`
- `Extensions/ServiceCollectionExtensions.cs` — EF Core / DB DI wiring
- Do **not** hand-edit generated files

## `NorthWind.Services.Tests/` — Service-layer tests
- `Service/CustomerServiceTest/` — placement of test files, names mirror tested method's name (example: `GetCustomers.cs`, `GetCustomerDetails.cs`)
- `DatabaseTestBase.cs` — shared EF Core in-memory DB setup
- `EntityFactory.cs` — test entity builders

## `NorthWind.Api.Tests/` — API integration tests
- `Rest/CustomersControllerTest/` — placement of test files, names mirror tested endpoint's name (example: `GetCustomers.cs`, `GetCustomerDetails.cs`)
- `ApiTestBase.cs` — `WebApplicationFactory` base setup

## Key Conventions

- Feature areas (e.g. `Customer`) are folder-based slices within each layer, co-locating interfaces, implementations, DTOs, models, and validators.
- Each layer owns its DI registration via its own `Extensions/ServiceCollectionExtensions.cs`.
- EF Core entities and `DbContext` are scaffold-generated under `Infrastructure/Persistance/Generated/` — do not hand-edit.

## Dependancy flow

NorthWind.Api
 └── NorthWind.Services
    └── NorthWind.Infrastructure
         └── SQL Server (EF Core)

NorthWind.Services.Tests
    ├── NorthWind.Services 
    └── NorthWind.Infrastructure
        
NorthWind.Api.Tests
    ├── NorthWind.Api
    ├── NorthWind.Infrastructure
    └── NorthWind.Services.Tests