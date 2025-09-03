Bank API:
A small REST API that manages accounts and simple money operations.

Tech stack:
	.NET 8 / ASP.NET Core Web API (controllers)
	In‑memory repository (no DB/ORM)
	xUnit tests (Application layer unit tests)

Why no DB/ORM?:
For a test project, an in‑memory store is the fastest to set up and easy to swap later. The repository interface lets you plug in EF Core (or anything else) without touching controllers or services.
Limitations: data is not persisted across runs.

Concurrency model:
Per‑account locking in the transactions service. Different accounts can process concurrently.

Tests:
Bank.Tests contains unit tests for Application layer.

Run:
- Visual Studio / Rider
- Set Bank.Api as startup project.
- Debug/Run. Swagger UI opens automatically.