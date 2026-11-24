import KpiCard from "./KpiCard";

function formatPhp(amount) {
  const num = Number(amount ?? 0);
  return `₱${num.toLocaleString("en-PH", {
    minimumFractionDigits: 2,
    maximumFractionDigits: 2,
  })}`;
}

function formatNumber(val) {
  return Number(val ?? 0).toLocaleString("en-PH");
}

export default function KpiCards({ summary, isLoading = false }) {
  const data = summary || {
    totalRegistrations: 0,
    totalPayments: 0,
    uncollectedPayments: 0,
    confirmed: 0,
    checkedIn: 0,
  };

  return (
    <section
      aria-label="Key Performance Indicators"
      className="grid grid-cols-1 gap-4 sm:grid-cols-2 lg:grid-cols-5"
    >
      <KpiCard
        title="Total Registrations"
        value={formatNumber(data.totalRegistrations)}
        isLoading={isLoading}
      />
      <KpiCard
        title="Total Payments"
        value={formatPhp(data.totalPayments)}
        subtitle="Collected"
        isLoading={isLoading}
      />
      <KpiCard
        title="Uncollected Payments"
        value={formatPhp(data.uncollectedPayments)}
        isLoading={isLoading}
      />
      <KpiCard
        title="Confirmed"
        value={formatNumber(data.confirmed)}
        isLoading={isLoading}
      />
      <KpiCard
        title="Checked In"
        value={formatNumber(data.checkedIn)}
        isLoading={isLoading}
      />
    </section>
  );
}