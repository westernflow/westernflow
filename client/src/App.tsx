import { Route, Routes, BrowserRouter } from "react-router-dom";
import HomePresenter from "./components/HomePresenter";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" Component={HomePresenter} />
        <Route path="/search/:search" element={<div>search result hi</div>} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;