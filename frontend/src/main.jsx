import "@fortawesome/fontawesome-free/css/all.min.css";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import "flowbite";
import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import RootLayout from "./navigation/RootLayout.jsx";
import Homepage from "./pages/Homepage.jsx";
import LoginPage from "./pages/LoginPage.jsx";
import AdminPage from "./pages/AdminPage.jsx";
import VoertuigenPage from "./pages/VoertuigenPage.jsx";
import TankkaartenPage from "./pages/TankkaartenPage.jsx";
import BustuurderPage from "./pages/BustuurderPage.jsx";
import FleetPage from "./pages/FleetPage.jsx";
import PrivateRoute from "./navigation/PrivateRoute.jsx";
import AuthContextProvider from "./authentication/AuthContext.jsx";
import ConfirmContextProvider from "./confirmation/ConfirmContext.jsx";
import DarkModeContextProvider from "../src/contexts/DarkModeContext.jsx";

const browserRouter = createBrowserRouter([
  {
    path: "/",
    element: <LoginPage />,
  },
  {
    path: "/",
    element: <RootLayout />,
    children: [
      {
        path: "/home",
        element: (
          <PrivateRoute
            component={<Homepage />}
            requiredRoles={["User", "Admin"]}
          />
        ),
      },
      {
        path: "/items/1",
        element: (
          <PrivateRoute
            component={<VoertuigenPage />}
            requiredRoles={["User", "Admin"]}
          />
        ),
      },
      {
        path: "/items/2",
        element: (
          <PrivateRoute
            component={<BustuurderPage />}
            requiredRoles={["User", "Admin"]}
          />
        ),
      },
      {
        path: "/items/3",
        element: (
          <PrivateRoute
            component={<TankkaartenPage />}
            requiredRoles={["User", "Admin"]}
          />
        ),
      },
      {
        path: "/items/4",
        element: (
          <PrivateRoute
            component={<FleetPage />}
            requiredRoles={["User", "Admin"]}
          />
        ),
      },
    ],
  },
  {
    path: "/admin",
    element: (
      <PrivateRoute component={<AdminPage />} requiredRoles={["Admin"]} />
    ),
  },
]);

ReactDOM.createRoot(document.getElementById("root")).render(
  <React.StrictMode>
    <AuthContextProvider>
      <ConfirmContextProvider>
        <DarkModeContextProvider>
          <RouterProvider router={browserRouter}></RouterProvider>
        </DarkModeContextProvider>
      </ConfirmContextProvider>
    </AuthContextProvider>
  </React.StrictMode>
);
