import { BrowserRouter, Routes, Route } from "react-router-dom";
import TelaInvites from "../screens/TelaInvites";
import TelaInvitesAceitos from "../screens/TelaInvitesAceitos";

export default function Rotas() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<TelaInvites />} />
        <Route path="/aceitos" element={<TelaInvitesAceitos />} />
      </Routes>
    </BrowserRouter>
  );
}
