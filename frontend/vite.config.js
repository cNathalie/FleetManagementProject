import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";

const isDocker = process.env.VITE_DOCKER === "true";

const dockerHost = {
  plugins: [react()],
  server: {
    host: "0.0.0.0",
    port: 5173,
  },
  host: true
};

const localHost = {
  plugins: [react()],
  server: {
    port: 5173,
  },
};

// https://vitejs.dev/config/
export default defineConfig(isDocker ? dockerHost : localHost);
