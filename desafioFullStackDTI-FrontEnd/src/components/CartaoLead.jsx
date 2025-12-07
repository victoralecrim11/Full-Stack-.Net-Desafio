export default function CartaoLead({ lead, aoAceitar, aoRecusar, mostrarContato }) {
  return (
    <div className="cartao-lead">
      <h3>{lead.firstName}</h3>
      <p><b>ID:</b> {lead.id}</p>
      <p><b>Data:</b> {new Date(lead.createdAt).toLocaleDateString()}</p>
      <p><b>Bairro:</b> {lead.suburb}</p>
      <p><b>Categoria:</b> {lead.category}</p>
      <p><b>Preço:</b> ${lead.price}</p>
      <p>{lead.description}</p>

      {mostrarContato && (
        <>
          <p><b>Contato:</b> {lead.firstName} {lead.lastName}</p>
          <p><b>Email:</b> {lead.email}</p>
          <p><b>Telefone:</b> {lead.phone}</p>
        </>
      )}

      {!mostrarContato && (
        <div className="botoes">
          <button onClick={aoAceitar}>Aceitar</button>
          <button onClick={aoRecusar}>Recusar</button>
        </div>
      )}
    </div>
  );
}
