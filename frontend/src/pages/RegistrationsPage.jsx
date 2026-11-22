import { useCallback, useEffect, useState } from "react";
import KpiCards from "../components/KpiCards";
import RegistrationFilters from "../components/RegistrationFilters";
import RegistrationTable from "../components/RegistrationTable";
import Pagination from "../components/Pagination";
import { useDebounce } from "../hooks/useDebounce";
import { getRegistrations, getSummary } from "../api/sample-data";
// Kapag handa na ang real C# API, palitan ang line sa itaas ng:
// import { getRegistrations, getSummary } from "../api/registrationsApi";

export default function RegistrationsPage() {
  const [searchTerm, setSearchTerm] = useState("");
  const [typeFilter, setTypeFilter] = useState("All");
  const [statusFilter, setStatusFilter] = useState("All");
  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(10);

  const [registrationsData, setRegistrationsData] = useState({
    items: [],
    page: 1,
    pageSize: 10,
    totalCount: 0,
    totalPages: 1,
  });
  const [summaryData, setSummaryData] = useState(null);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState("");

  const debouncedSearch = useDebounce(searchTerm, 400);

  const loadDashboard = useCallback(async () => {
    setIsLoading(true);
    setError("");

    try {
      const [regsResult, summaryResult] = await Promise.all([
        getRegistrations({
          participantName: debouncedSearch,
          type: typeFilter === "All" ? "" : typeFilter,
          status: statusFilter === "All" ? "" : statusFilter,
          page,
          pageSize,
        }),
        getSummary(),
      ]);

      setRegistrationsData(regsResult);
      setSummaryData(summaryResult);
    } catch (err) {
      setError(err?.message || "Cannot reach the API.");
    } finally {
      setIsLoading(false);
    }
  }, [debouncedSearch, typeFilter, statusFilter, page, pageSize]);

  useEffect(() => {
    loadDashboard();
  }, [loadDashboard]);

  const handleSearchChange = (value) => {
    setSearchTerm(value);
    setPage(1);
  };

  const handleTypeChange = (value) => {
    setTypeFilter(value);
    setPage(1);
  };

  const handleStatusChange = (value) => {
    setStatusFilter(value);
    setPage(1);
  };

  const handlePageSizeChange = (newSize) => {
    setPageSize(newSize);
    setPage(1);
  };

  const handleClearFilters = () => {
    setSearchTerm("");
    setTypeFilter("All");
    setStatusFilter("All");
    setPage(1);
  };

  return (
    <div className="min-h-screen bg-slate-50 text-slate-900">
      <header className="border-b border-slate-200 bg-white">
        <div className="mx-auto flex max-w-7xl flex-col gap-1 px-4 py-5 sm:px-6 lg:px-8">
          <div className="flex flex-wrap items-center justify-between gap-2">
            <div>
              <h1 className="text-2xl font-bold tracking-tight text-slate-900">
                Car Show 2027
              </h1>
              <p className="text-sm text-slate-600">
                AutoVista Events • Registration Dashboard
              </p>
            </div>
            <span className="inline-flex items-center rounded-full bg-slate-100 px-3 py-1 text-xs font-medium text-slate-700">
              Sorsogon City Coliseum
            </span>
          </div>
        </div>
      </header>

      <main className="mx-auto flex max-w-7xl flex-col gap-5 px-4 py-6 sm:px-6 lg:px-8">
        <KpiCards summary={summaryData} isLoading={isLoading && !summaryData} />

        <RegistrationFilters
          searchTerm={searchTerm}
          onSearchChange={handleSearchChange}
          typeFilter={typeFilter}
          onTypeChange={handleTypeChange}
          statusFilter={statusFilter}
          onStatusChange={handleStatusChange}
          onClearFilters={handleClearFilters}
          disabled={isLoading}
        />

        <RegistrationTable
          items={registrationsData.items}
          isLoading={isLoading}
          error={error}
          onRetry={loadDashboard}
          onClearFilters={handleClearFilters}
        />

        {!isLoading && !error && registrationsData.totalCount > 0 && (
          <Pagination
            page={registrationsData.page}
            pageSize={registrationsData.pageSize}
            totalCount={registrationsData.totalCount}
            totalPages={registrationsData.totalPages}
            onPageChange={setPage}
            onPageSizeChange={handlePageSizeChange}
            disabled={isLoading}
          />
        )}
      </main>
    </div>
  );
}