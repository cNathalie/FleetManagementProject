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
import Voertuigen from "./pages/Voertuigen.jsx";
import PrivateRoute from "./navigation/PrivateRoute.jsx";

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
        path: "/items/:id",
        element: (
          <PrivateRoute
            component={<Voertuigen />}
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
    <RouterProvider router={browserRouter}></RouterProvider>
  </React.StrictMode>
);
