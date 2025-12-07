import { useEffect, useState } from "react";
import { api } from "../services/api";
import CartaoLead from "../components/CartaoLead";

export default function TelaInvitesAceitos() {
  const [leads, setLeads] = useState([]);

  useEffect(() => {
    api.getAceitos().then(setLeads);
  }, []);

  return (
    <div>
      <h2>Leads Aceitos</h2>
      {leads.map(l => (
        <CartaoLead
          key={l.id}
          lead={l}
          mostrarContato={true}
        />
      ))}
    </div>
  );
}
