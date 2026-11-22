export default function Pagination({
  page = 1,
  pageSize = 10,
  totalCount = 0,
  totalPages = 1,
  onPageChange,
  onPageSizeChange,
  disabled = false,
}) {
  const startItem = totalCount === 0 ? 0 : (page - 1) * pageSize + 1;
  const endItem = Math.min(page * pageSize, totalCount);

  const pageNumbers = Array.from({ length: totalPages }, (_, idx) => idx + 1);

  return (
    <section
      aria-label="Pagination"
      className="flex flex-col gap-3 rounded-xl border border-slate-200 bg-white px-4 py-3 shadow-xs sm:flex-row sm:items-center sm:justify-between"
    >
      <div className="flex flex-wrap items-center gap-4 text-sm text-slate-600">
        <span>
          Showing{" "}
          <strong className="font-semibold text-slate-900">{startItem}</strong>-
          <strong className="font-semibold text-slate-900">{endItem}</strong> of{" "}
          <strong className="font-semibold text-slate-900">{totalCount}</strong>
        </span>

        <label className="inline-flex items-center gap-2 text-xs font-medium text-slate-700">
          <span>Per page</span>
          <select
            value={pageSize}
            onChange={(e) => onPageSizeChange(Number(e.target.value))}
            disabled={disabled}
            className="rounded-lg border border-slate-300 bg-white px-2.5 py-1 text-sm text-slate-900 focus:border-blue-500 focus:outline-none focus:ring-2 focus:ring-blue-500/20 disabled:cursor-not-allowed disabled:bg-slate-100"
          >
            <option value={5}>5</option>
            <option value={10}>10</option>
            <option value={20}>20</option>
          </select>
        </label>
      </div>

      <div className="flex flex-wrap items-center gap-1">
        <button
          type="button"
          onClick={() => onPageChange(page - 1)}
          disabled={disabled || page <= 1}
          className="cursor-pointer rounded-lg border border-slate-300 bg-white px-3 py-1.5 text-xs font-medium text-slate-700 transition hover:bg-slate-50 disabled:cursor-not-allowed disabled:opacity-40"
        >
          Prev
        </button>

        {pageNumbers.map((num) => {
          const isCurrent = num === page;
          return (
            <button
              key={num}
              type="button"
              onClick={() => onPageChange(num)}
              disabled={disabled}
              aria-current={isCurrent ? "page" : undefined}
              className={`cursor-pointer rounded-lg border px-3 py-1.5 text-xs font-medium transition disabled:cursor-not-allowed ${
                isCurrent
                  ? "border-blue-600 bg-blue-600 text-white"
                  : "border-slate-300 bg-white text-slate-700 hover:bg-slate-50"
              }`}
            >
              {num}
            </button>
          );
        })}

        <button
          type="button"
          onClick={() => onPageChange(page + 1)}
          disabled={disabled || page >= totalPages}
          className="cursor-pointer rounded-lg border border-slate-300 bg-white px-3 py-1.5 text-xs font-medium text-slate-700 transition hover:bg-slate-50 disabled:cursor-not-allowed disabled:opacity-40"
        >
          Next
        </button>
      </div>
    </section>
  );
}
