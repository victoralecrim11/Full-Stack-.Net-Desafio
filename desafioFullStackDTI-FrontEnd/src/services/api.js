export const api = {
  getInvitados: () => fetch("https://localhost:7150/api/Leads/invited").then(r => r.json()),
  getAceitos: () => fetch("https://localhost:7150/api/Leads/accepted").then(r => r.json()),
  aceitarLead: (id) => fetch(`https://localhost:7150/api/Leads/${id}/accept`, { method: "POST" }),
  recusarLead: (id) => fetch(`https://localhost:7150/api/Leads/${id}/decline`, { method: "POST" })
};