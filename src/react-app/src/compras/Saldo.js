import React, { useEffect, useState } from "react";
import { valorAcumulado } from "../api/saldoApi";
import { dinheiro } from "../format";

function Saldo() {
  const [saldo, setSaldo] = useState(null);

  useEffect(() => {
    valorAcumulado().then(valor => setSaldo(valor));
  }, []);

  return (
    <div>
      <small>Saldo acumulado</small>
      <strong className="d-block">{saldo ? dinheiro(saldo) : "..."}</strong>
    </div>
  );
}

export default Saldo;
