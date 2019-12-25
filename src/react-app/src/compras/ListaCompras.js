import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import Octicon, { Pencil, Trashcan } from "@primer/octicons-react";
import { excluir, listar } from "../api/comprasApi";
import { data, dinheiro, percentual } from "../format";

function edicaoHabilitada(item) {
  return item.status !== 1;
}

function status(id) {
  switch (id) {
    case 0:
      return "Em validação";
    case 1:
      return "Aprovada";
    case 2:
      return "Rejeitado";
    default:
      return "Desconhecido";
  }
}

function ListaCompras() {
  const [itens, setItens] = useState(null);

  async function fetchCompras() {
    const resultado = await listar();
    setItens(resultado);
  }

  useEffect(() => {
    fetchCompras();
  }, []);

  if (!itens) {
    return <div>Carregando...</div>;
  }

  if (!itens.length) {
    return <div className="alert alert-info">Nenhuma compra cadastrada</div>;
  }

  async function onExcluir(codigo) {
    await excluir(codigo);
    await fetchCompras();
  }

  return (
    <table className="table table-striped">
      <thead>
        <tr>
          <th>Código</th>
          <th>Valor</th>
          <th>Data</th>
          <th>% Cashback</th>
          <th>Valor Cashback</th>
          <th>Status</th>
          <th></th>
          <th></th>
        </tr>
      </thead>
      <tbody>
        {itens.map((item, index) => (
          <tr key={index}>
            <td>{item.codigo}</td>
            <td>{dinheiro(item.valor)}</td>
            <td>{data(item.data)}</td>
            <td className="text-center">
              {percentual(item.percentualCashback)}
            </td>
            <td>{dinheiro(item.valorCashback)}</td>
            <td>{status(item.status)}</td>
            <td>
              {edicaoHabilitada(item) ? (
                <Link
                  to={{
                    pathname: "/compra/cadastro",
                    state: item,
                  }}
                  title="Editar"
                >
                  <Octicon icon={Pencil} />
                </Link>
              ) : null}
            </td>
            <td>
              {edicaoHabilitada(item) ? (
                <button
                  className="btn btn-link p-0"
                  onClick={() => onExcluir(item.codigo)}
                  title="Excluir"
                >
                  <Octicon icon={Trashcan} />
                </button>
              ) : null}
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}

export default ListaCompras;
