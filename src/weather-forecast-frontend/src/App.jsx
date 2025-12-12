import { useEffect, useState } from "react";
import ForecastCard from "./components/ForecastCard";
import { loadConfig } from './configLoader';

export default function App() {
  const [forecasts, setForecasts] = useState([]);
  const [config, setConfig] = useState(null);
  const [err, setErr] = useState(null);

  useEffect(() => {
    loadConfig()
      .then(setConfig)
      .catch(e => setErr(e.message));
  }, []);

  if (err) return <div>Error loading config: {err}</div>;
  if (!config) return <div>Loading config...</div>;

  // ✅ Access backend_url here
  const backendUrl = config.backend_url;

  const handleCheckWeather = async () => {
    try {
      console.log("Fetching from:", `${backendUrl}/WeatherForecast`);

      const response = await fetch(`${backendUrl}/WeatherForecast`);

      // Log response details
      console.log("Response status:", response.status);
      console.log("Response headers:", response.headers);

      // Get response as text first to see what we're getting
      const responseText = await response.text();
      console.log("Response text:", responseText);

      // Try to parse as JSON
      const data = JSON.parse(responseText);
      setForecasts(data);

    } catch (err) {
      console.error("Full error details: ", err);
      console.error("Failed to fetch forecast: ", err.message);
    }
  };

  return (
    <div className="min-h-screen bg-gradient-to-br from-sky-400 to-indigo-600 flex flex-col items-center p-6 text-white">
      <h1>Frontend connected to: {backendUrl}</h1>
      <button onClick={handleCheckWeather}>Check Weather</button>
      <h1 className="text-4xl font-bold mb-8 drop-shadow-lg">🌤 Weatherly Forecast</h1>
      <div className="grid md:grid-cols-3 sm:grid-cols-2 grid-cols-1 gap-6 w-full max-w-5xl">
        {forecasts.map((f, i) => (
          <ForecastCard key={i} forecast={f} />
        ))}
      </div>
    </div>
  );
}
