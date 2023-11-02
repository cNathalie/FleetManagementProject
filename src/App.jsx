import {
  Route,
  createBrowserRouter,
  createRoutesFromElements,
  RouterProvider,
} from "react-router-dom";
import "./App.css";

import Homepage from "./pages/Homepage";
import LoginPage from "./pages/LoginPage";
import AdminPage from "./pages/AdminPage";

const router = createBrowserRouter(
  createRoutesFromElements(
    <Route path="/">
      <Route index element={<LoginPage />} />
      <Route path="home" element={<Homepage />} />
      <Route path="admin" element={<AdminPage />} />

      {/* hier nog routes naar andere pagina's */}
    </Route>
  )
);

function App({ routes }) {
  return (
    <>
      <RouterProvider router={router} />
    </>
  );
}
export default App;
