import {
  Route,
  createBrowserRouter,
  createRoutesFromElements,
  RouterProvider,
} from "react-router-dom";
import "./App.css";
import 'flowbite';

import Homepage from "./pages/Homepage";
import LoginPage from "./pages/LoginPage";
import AdminPage from "./pages/AdminPage";
import VoertuigenPage from "./pages/VoertuigenPage";
import TankkaartenPage from "./pages/TankkaartenPage";
import BustuurderPage from "./pages/BustuurderPage";

const router = createBrowserRouter(
  createRoutesFromElements(
    <Route path="/">

      <Route index element={<LoginPage />} />
      <Route path="home" element={<Homepage />} />
      <Route path="admin" element={<AdminPage />} />
      <Route path="voertuigen" element={<VoertuigenPage/>} />
      <Route path="tankkaarten" element={<TankkaartenPage/>} />
      <Route path="bestuurders" element={<BustuurderPage/>} />
      {/* hier nog routes naar andere pagina's */}
      
    </Route>
  )
);

function App() {
  return (
    <>
      <RouterProvider router={router} />
    </>
  );
}
export default App;
