import React from "react";
import { Link } from "react-router-dom";

function InfoSistema() {
  return (
    <div className="container-card container-form">
      <h1 className="titulo">Informações do sistema</h1>

      <strong>Desenvolvedor</strong>
      <p>Edson Miguel Dewes</p>

      <strong>Cidade</strong>
      <p>Curitiba/PR</p>

      <strong>E-mail</strong>
      <p>edson_dewes@hotmail.com</p>

      <Link className="d-block text-center" to="/">
        Voltar
      </Link>
    </div>
  );
}

export default InfoSistema;
