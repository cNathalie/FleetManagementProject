<<<<<<< Updated upstream
import {
  Route,
  createBrowserRouter,
  createRoutesFromElements,
  RouterProvider,
} from "react-router-dom";
import "./App.css";

import Home from "./pages/Home";
import LoginPage from "./pages/LoginPage";
=======
import './App.css'
import FleetWeergave from './components/FleetWeergave'

const homePage = [
  {
    id: 1,
    title: "Voertuigen",
    text: "Een handig overzicht van alle voertuigen binnen het wagenpark. Voeg items toe aan de lijst, pas ze aan, of verwijder ze.",
    btnValue: "Ga naar voertuigen",
    imgSrc: "./Media/voertuigen.jpg",
    alt: "Foto van voertuigen"
  }, {
    id: 2,
    title: "Bestuurders",
    text: "Een handig overzicht van alle bestuurders binnen het wagenpark. Voeg items toe aan de lijst, pas ze aan, of verwijder ze.",
    btnValue: "Ga naar bestuurders",
    imgSrc: "./Media/bestuurders.jpg",
    alt: "Foto van bestuurders"
  }, {
    id: 3,
    title: "Tankkaarten",
    text: "Een handig overzicht van alle tankkaarten binnen het wagenpark. Voeg items toe aan de lijst, pas ze aan, of verwijder ze.",
    btnValue: "Ga naar tankkaarten",
    imgSrc: "./Media/tankkaart.jpg",
    alt: "Foto van tankkaarten"
  }
]
>>>>>>> Stashed changes

const router = createBrowserRouter(
  createRoutesFromElements(
    <Route path="/">
      <Route index element={<Home />} />
      <Route path="login" element={<LoginPage />} />
      {/* hier nog routes naar andere pagina's */}
    </Route>
  )
);

function App({ routes }) {
  return (
    <>
<<<<<<< Updated upstream
      <RouterProvider router={router} />
=======
    {homePage.map((h) => {
      return <FleetWeergave key={h.id} title={h.title} text={h.text} btnValue={h.btnValue} imgSrc={h.imgSrc} fleetweergave={h}/>
    })}
>>>>>>> Stashed changes
    </>
  );
}
export default App;
