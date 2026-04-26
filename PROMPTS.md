You are presented with the following requirements: `{fileWithTaskRequirements}`

A backend for this solution is already created and endpoints to fetch the data from are available. They will be given to you when the app reaches the stage to request them. You will perform the implementation in steps:
• The application will live in the current directory `northwind-app`
* We will use `Vite` build tool to create the initial template. Fetch the required `npm` command to create the react template and run `npm install` and `npm run dev` afterwards.
* The following key dependencies should be installed after the initial react template is created: `react-router-dom`, `@tanstack/react-query`, `axios`
* The project structure should be the following: on top-most level in `northwind-app` folder add: `src` folder: `src/api`, `src/components`, `src/pages`, `src/types`
These are the initial steps. We will continue after these steps are successfully completed. Plan everything, ask if uncertain, present the steps one by one after each is done.

Everything seems to be running so far template wise. I cleaned up `html/css` code from the initial template, so the components are empty.
Your next step is to configure `axios`:
- You will be provided with a Swagger endpoint schema for the endpoints. `{swaggerSchema}`
- Create a `axiosInstance.ts` file under `api/`
- The base` url` of the backend is `https://localhost:7097`
- Set up only an `axios` `apiClient` in `axiosInstance.ts` according to the schema.

Now when the `axiosClient` is created, the next step is to generate the types returned by the endpoints from the Swagger schema i provided earlier. Create them in `types/index.ts`.

Check the types again and think more carefully which are required. Do not add `| null` to required types.

Configure the `axios` API calls to the endpoints. Do so in `api/customers.ts`. Reference the Swagger schema i provided earlier.

The next step is to set up an error interpreter for `axios`. Handle this in `axiosInstance.ts`

Re-do your planned changes, please use `import type` when importing types and do not create unnecessary classes/variables. Fetch the `axios` documentation and re-do the step.

Your next task in the plan is to set up react router in `App.tsx`

Now lets set up the query client provider from `@tanstack` in `main.tsx`

Before we continue, we will use `MUI/MaterialUI` for the styling and components
- run `@mui/material @emotion/react @emotion/styled`

You should now focus on `CustomersPage.ts`. Knowing the initial requirements/requirements so far/swagger schema structure the page accordingly using components from ``@mui/material`:
- Set up API calls first with adequate `IsValid` and `IsError` handling
- The search field/functionality should have a `debounce` of 300ms, you can use `use-debounce` `npm` package
- Create separate react components for the search and the table in `components/` named `CustomerSearch`, `CustomerTable`