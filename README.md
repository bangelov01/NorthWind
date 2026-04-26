# NorthWind app

## Setup for local dev. environment

### Backend Service
- Install .NET 10 SDK
- Restore the database from the provided `northwinddb_init.sql` file within the repo root
  - **Note**: The script will create and populate a database with the name `NorthWindDb`
- Adjust the DB connection string if needed in `appsettings.Development.json` (no user-secrets as it is a demo app)
- Adjust local ports if not free in `launchSettings.json`
- Run `dotnet-certs https --trust` to trust local dev. https certificate
- Build and run the app
- Access Swagger through `https://localhost:XXXX/Swagger/`

### Frontend Client
- Run `npm install` within `northwind-app`
- Configure backend service URL in `src/api/axiosInstance.ts`
- Run the app - `npm run dev`
- Add the frontend service URL in `appsettings.Development.json; "AllowedOrigins": [...]`

#### Notes:
- Agentic work was mainly done on the frontend. The prompts used can be found in `PROMPTS.md`
- I mapped out some general guidelines and project structure in `CLAUDE.md` for more context on the app
