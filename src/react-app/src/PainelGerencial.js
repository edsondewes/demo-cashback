import React, { useContext } from "react";
import { Link, useLocation } from "react-router-dom";
import Octicon, { SignOut } from "@primer/octicons-react";
import LoginContext from "./LoginContext";
import ListCompras from "./compras/ListaCompras";
import Saldo from "./compras/Saldo";

function PainelGerencial() {
  const login = useContext(LoginContext);
  const location = useLocation();

  // Código da compra vem pela rota ao finalizar cadastro
  const codigoCompraCadastro =
    location.state && location.state.codigo ? location.state.codigo : null;

  function onSair() {
    login.logout();
  }

  return (
    <div className="container-card">
      <header className="d-flex align-items-center mb-3">
        <div className="mr-auto">
          <h4 className="mb-1">{login.usuario.nome}</h4>
          <Link to="/compra/cadastro">Cadastrar nova compra</Link>
        </div>
        <Saldo />
        <button
          className="btn btn-link py-0 ml-2"
          onClick={onSair}
          title="Sair"
        >
          <Octicon icon={SignOut} />
        </button>
      </header>
      <main>
        {codigoCompraCadastro ? (
          <div className="alert alert-success text-center">
            Compra {codigoCompraCadastro}{" "}
            {location.state.edicao ? "alterada" : "cadastrada"} com sucesso!
          </div>
        ) : null}
        <ListCompras />
      </main>
    </div>
  );
}

export default PainelGerencial;
