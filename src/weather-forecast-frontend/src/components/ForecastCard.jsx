export default function ForecastCard({ forecast }) {
  const { date, temperature, scale, summary } = forecast;
  const formattedDate = new Date(date).toLocaleDateString("en-US", {
    weekday: "long",
    month: "short",
    day: "numeric"
  });

  return (
    <div className="bg-white/20 backdrop-blur-lg p-6 rounded-2xl shadow-lg transform transition hover:-translate-y-1 hover:shadow-2xl">
      <h2 className="text-xl font-semibold mb-2">{formattedDate}</h2>
      <p className="text-6xl font-bold mb-2">{temperature}</p>
      <p className="text-sm text-gray-200">({scale})</p>
      <p className="mt-4 text-lg italic">{summary}</p>
    </div>
  );
}
