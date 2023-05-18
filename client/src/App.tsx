import { Route, Routes, BrowserRouter } from "react-router-dom";
import HomePresenter from "./components/HomePresenter";
import { SearchContext } from "./contexts/SearchContext";
import { useContext } from "react";

function App() {

  return (
    <SearchContext>
      <BrowserRouter>
        <Routes>
          <Route path="/" Component={HomePresenter} />
          <Route path="/search/:search" element={<div>search result hi</div>} />
        </Routes>
      </BrowserRouter>  
    </SearchContext>
  );
}

export default App;