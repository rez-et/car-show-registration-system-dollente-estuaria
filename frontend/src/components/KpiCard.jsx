export default function KpiCard({ title, value, subtitle, isLoading = false }) {
  return (
    <div className="rounded-xl border border-slate-200 bg-white p-4 shadow-xs">
      <p className="text-xs font-medium uppercase tracking-wide text-slate-500">
        {title}
      </p>

      {isLoading ? (
        <div className="mt-2 h-8 w-28 animate-pulse rounded bg-slate-200" />
      ) : (
        <p className="mt-2 text-2xl font-bold text-slate-900">{value}</p>
      )}

      {subtitle && (
        <p className="mt-1 text-xs text-slate-500">{subtitle}</p>
      )}
    </div>
  );
}