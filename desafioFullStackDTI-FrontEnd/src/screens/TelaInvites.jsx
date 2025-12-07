import { useEffect, useState } from "react";
import { api } from "../services/api";
import CartaoLead from "../components/CartaoLead";
import "../styles/global.css";
import "../styles/layout.css";
import "../styles/cartao.css";

export default function TelaInvites() {
  const [leads, setLeads] = useState([]);

useEffect(() => {
  api.getInvitados().then((res) => {
    console.log("LEADS QUE VOLTARAM:", res);
    setLeads(res);
  });
}, []);


  function aceitar(id) {
    api.aceitarLead(id).then(() => {
      setLeads(leads.filter(l => l.id !== id));
    });
  }

  function recusar(id) {
    api.recusarLead(id).then(() => {
      setLeads(leads.filter(l => l.id !== id));
    });
  }

  return (
    <div>
      <h2>Leads Novos</h2>
      {leads.map(l => (
        <CartaoLead
          key={l.id}
          lead={l}
          aoAceitar={() => aceitar(l.id)}
          aoRecusar={() => recusar(l.id)}
        />
      ))}
    </div>
  );
}
