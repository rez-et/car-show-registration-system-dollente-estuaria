import StatusBadge from "./StatusBadge";
import PaymentBadge from "./PaymentBadge";

function formatPhp(amount) {
  const num = Number(amount ?? 0);
  return `₱${num.toLocaleString("en-PH", {
    minimumFractionDigits: 2,
    maximumFractionDigits: 2,
  })}`;
}

function formatDate(dateString) {
  if (!dateString) return "-";
  const date = new Date(dateString);
  if (Number.isNaN(date.getTime())) return dateString;

  return date.toLocaleString("en-US", {
    month: "short",
    day: "2-digit",
    year: "numeric",
    hour: "2-digit",
    minute: "2-digit",
    hour12: true,
  });
}

export default function RegistrationTable({
  items = [],
  isLoading = false,
  error = "",
  onRetry,
  onClearFilters,
}) {
  if (isLoading) {
    return (
      <div className="rounded-xl border border-slate-200 bg-white p-8 text-center shadow-xs">
        <p className="text-sm font-medium text-slate-600">
          Loading registrations...
        </p>
      </div>
    );
  }

  if (error) {
    return (
      <div className="rounded-xl border border-rose-200 bg-rose-50/60 p-8 text-center shadow-xs">
        <p className="text-sm font-semibold text-rose-800">{error}</p>
        {onRetry && (
          <button
            type="button"
            onClick={onRetry}
            className="mt-4 cursor-pointer rounded-lg bg-rose-600 px-4 py-2 text-sm font-medium text-white transition hover:bg-rose-700 focus:outline-none focus:ring-2 focus:ring-rose-500/30"
          >
            Try again
          </button>
        )}
      </div>
    );
  }

  if (!items.length) {
    return (
      <div className="rounded-xl border border-slate-200 bg-white p-8 text-center shadow-xs">
        <p className="text-sm font-medium text-slate-700">
          No registrations match your filters
        </p>
        {onClearFilters && (
          <button
            type="button"
            onClick={onClearFilters}
            className="mt-4 cursor-pointer rounded-lg border border-slate-300 bg-white px-4 py-2 text-sm font-medium text-slate-700 transition hover:bg-slate-50 focus:outline-none focus:ring-2 focus:ring-slate-400/30"
          >
            Clear filters
          </button>
        )}
      </div>
    );
  }

  return (
    <div className="overflow-hidden rounded-xl border border-slate-200 bg-white shadow-xs">
      <div className="overflow-x-auto">
        <table className="min-w-full divide-y divide-slate-200 text-left text-sm">
          <thead className="bg-slate-50 text-xs font-semibold uppercase tracking-wider text-slate-600">
            <tr>
              <th scope="col" className="px-4 py-3">
                Registration No.
              </th>
              <th scope="col" className="px-4 py-3">
                Date
              </th>
              <th scope="col" className="px-4 py-3">
                Participant
              </th>
              <th scope="col" className="px-4 py-3">
                Car
              </th>
              <th scope="col" className="px-4 py-3">
                Type
              </th>
              <th scope="col" className="px-4 py-3">
                Status
              </th>
              <th scope="col" className="px-4 py-3 text-right">
                Fee / Paid
              </th>
              <th scope="col" className="px-4 py-3">
                Payment
              </th>
            </tr>
          </thead>
          <tbody className="divide-y divide-slate-200 bg-white text-slate-800">
            {items.map((reg) => (
              <tr key={reg.id} className="transition hover:bg-slate-50/80">
                <td className="whitespace-nowrap px-4 py-3 font-semibold text-slate-900">
                  {reg.registrationNumber}
                </td>
                <td className="whitespace-nowrap px-4 py-3 text-slate-600">
                  {formatDate(reg.registrationDate)}
                </td>
                <td className="px-4 py-3">
                  <div className="font-medium text-slate-900">
                    {reg.participantName}
                  </div>
                  <div className="text-xs text-slate-500">
                    {reg.contactNumber || reg.email}
                  </div>
                </td>
                <td className="px-4 py-3">
                  <div className="font-medium text-slate-900">
                    {reg.carMakeModel}
                  </div>
                  <div className="text-xs text-slate-500">
                    {reg.plateNumber}
                  </div>
                </td>
                <td className="whitespace-nowrap px-4 py-3 text-slate-700">
                  {reg.registrationType}
                </td>
                <td className="whitespace-nowrap px-4 py-3">
                  <StatusBadge status={reg.status} />
                </td>
                <td className="whitespace-nowrap px-4 py-3 text-right font-mono text-xs sm:text-sm text-slate-900">
                  {formatPhp(reg.registrationFee)} / {formatPhp(reg.amountPaid)}
                </td>
                <td className="whitespace-nowrap px-4 py-3">
                  <PaymentBadge status={reg.paymentStatus} />
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}