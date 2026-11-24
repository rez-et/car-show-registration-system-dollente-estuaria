# Car Show Registration System

A Car Show 2027 registration and dashboard system built with a C#/.NET 10 Minimal API backend (Vertical Slice Architecture + SQLite) and a React (Vite) frontend.

## Team

| MEMBER | ROLE |
| --- | --- |
| Cedrick Dollente | Backend Developer |
| Danilo Estuaria | Frontend Developer |

## Project Structure

```text
car-show-registration-system/
├── backend/       # C# .NET 10 Vertical Slice API (Domain, Application, Infrastructure, Api)
├── frontend/      # React + Vite + Tailwind CSS dashboard
├── docs/
│   └── screenshots/
└── README.md
```

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Node.js](https://nodejs.org/) (v20+ recommended)

## How to Run

### 1. Backend API

```bash
cd backend
dotnet restore
dotnet run --project Api
```

- API base URL: `http://localhost:5205`
- Scalar API docs: `http://localhost:5205/scalar/v1`
- Pending EF Core SQLite migrations (`carshow.db`) and initial seed data (26 sample registrations) are applied automatically on startup.

### 2. Frontend App

Open a second terminal while the backend API is running:

```bash
cd frontend
npm install
npm run dev
```

- Frontend URL: `http://localhost:5173` (vite may fallback to :5174 and so, if :5173 is occupied)
- Optional environment variable: set `VITE_API_BASE_URL` in `frontend/.env` if your API runs on a different URL (defaults to `http://localhost:5205`).
