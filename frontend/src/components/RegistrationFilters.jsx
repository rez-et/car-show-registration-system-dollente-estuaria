export default function RegistrationFilters({
  searchTerm,
  onSearchChange,
  typeFilter,
  onTypeChange,
  statusFilter,
  onStatusChange,
  onClearFilters,
  disabled = false,
}) {
  const hasActiveFilters =
    searchTerm.trim() !== "" ||
    (typeFilter && typeFilter !== "All") ||
    (statusFilter && statusFilter !== "All");

  return (
    <section
      aria-label="Registration Filters"
      className="rounded-xl border border-slate-200 bg-white p-4 shadow-xs"
    >
      <div className="grid grid-cols-1 gap-4 md:grid-cols-12 md:items-end">
        <div className="md:col-span-5">
          <label
            htmlFor="search-participant"
            className="mb-1 block text-xs font-medium text-slate-700"
          >
            Search Participant
          </label>
          <input
            id="search-participant"
            type="text"
            value={searchTerm}
            onChange={(e) => onSearchChange(e.target.value)}
            placeholder="Search participant name..."
            disabled={disabled}
            className="w-full rounded-lg border border-slate-300 bg-white px-3 py-2 text-sm text-slate-900 placeholder:text-slate-400 focus:border-blue-500 focus:outline-none focus:ring-2 focus:ring-blue-500/20 disabled:cursor-not-allowed disabled:bg-slate-100"
          />
        </div>

        <div className="md:col-span-3">
          <label
            htmlFor="filter-type"
            className="mb-1 block text-xs font-medium text-slate-700"
          >
            Registration Type
          </label>
          <select
            id="filter-type"
            value={typeFilter}
            onChange={(e) => onTypeChange(e.target.value)}
            disabled={disabled}
            className="w-full rounded-lg border border-slate-300 bg-white px-3 py-2 text-sm text-slate-900 focus:border-blue-500 focus:outline-none focus:ring-2 focus:ring-blue-500/20 disabled:cursor-not-allowed disabled:bg-slate-100"
          >
            <option value="All">All Types</option>
            <option value="Exhibitor">Exhibitor</option>
            <option value="Competitor">Competitor</option>
            <option value="VIP">VIP</option>
          </select>
        </div>

        <div className="md:col-span-2">
          <label
            htmlFor="filter-status"
            className="mb-1 block text-xs font-medium text-slate-700"
          >
            Status
          </label>
          <select
            id="filter-status"
            value={statusFilter}
            onChange={(e) => onStatusChange(e.target.value)}
            disabled={disabled}
            className="w-full rounded-lg border border-slate-300 bg-white px-3 py-2 text-sm text-slate-900 focus:border-blue-500 focus:outline-none focus:ring-2 focus:ring-blue-500/20 disabled:cursor-not-allowed disabled:bg-slate-100"
          >
            <option value="All">All Statuses</option>
            <option value="Pending">Pending</option>
            <option value="Confirmed">Confirmed</option>
            <option value="CheckedIn">CheckedIn</option>
            <option value="Cancelled">Cancelled</option>
          </select>
        </div>

        <div className="md:col-span-2">
          <button
            type="button"
            onClick={onClearFilters}
            disabled={disabled || !hasActiveFilters}
            className="w-full cursor-pointer rounded-lg border border-slate-300 bg-slate-50 px-3 py-2 text-sm font-medium text-slate-700 transition hover:bg-slate-100 focus:outline-none focus:ring-2 focus:ring-slate-400/30 disabled:cursor-not-allowed disabled:opacity-50"
          >
            Clear filters
          </button>
        </div>
      </div>
    </section>
  );
}