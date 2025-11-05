import { useEffect, useState } from "react";
import ForecastCard from "./components/ForecastCard";

export default function App() {
  const [forecasts, setForecasts] = useState([]);

  useEffect(() => {
    fetch("https://localhost:7084/WeatherForecast") // replace with your backend endpoint
      .then(res => res.json())
      .then(data => setForecasts(data))
      .catch(err => console.error("Failed to fetch forecast:", err));
  }, []);

  return (
    <div className="min-h-screen bg-gradient-to-br from-sky-400 to-indigo-600 flex flex-col items-center p-6 text-white">
      <h1 className="text-4xl font-bold mb-8 drop-shadow-lg">🌤 Weatherly Forecast</h1>
      <div className="grid md:grid-cols-3 sm:grid-cols-2 grid-cols-1 gap-6 w-full max-w-5xl">
        {forecasts.map((f, i) => (
          <ForecastCard key={i} forecast={f} />
        ))}
      </div>
    </div>
  );
}
