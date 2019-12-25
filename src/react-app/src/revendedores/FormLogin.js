import React, { useState } from "react";
import PropTypes from "prop-types";
import { Formik, Form, Field } from "formik";
import Octicon, { Question } from "@primer/octicons-react";
import { Link, useLocation } from "react-router-dom";
import { login as loginApi } from "../api/revendedoresApi";

function FormLogin({ login }) {
  const location = useLocation();
  const [erro, setErro] = useState(null);

  let email = "";
  let mostrarMensagemCadastro = false;

  // Email do revendedor vem pela rota ao finalizar cadastro
  if (location.state && location.state.email) {
    email = location.state.email;
    mostrarMensagemCadastro = true;
  }

  return (
    <div className="container-card container-form">
      <h1 className="titulo">
        App Cashback
        <Link className="ml-2" to="/info" title="Informações do sistema">
          <Octicon icon={Question} />
        </Link>
      </h1>
      <Formik
        initialValues={{ email, senha: "" }}
        onSubmit={async valores => {
          const resultado = await loginApi(valores.email, valores.senha);
          if (resultado.erro) {
            setErro(resultado.erro);
          } else {
            login(resultado);
          }
        }}
      >
        {({ isSubmitting }) => (
          <Form>
            <Field
              type="email"
              name="email"
              className="form-control"
              placeholder="E-mail"
              required="required"
              title="E-mail"
            />
            <Field
              type="password"
              name="senha"
              className="form-control"
              placeholder="Senha"
              required="required"
              title="Senha"
            />
            <input
              className="btn btn-lg btn-primary btn-entrar"
              type="submit"
              disabled={isSubmitting}
              value="Entrar"
            />

            {erro ? (
              <div className="alert alert-warning text-center">
                <small className="d-block">{erro}</small>
              </div>
            ) : null}

            {mostrarMensagemCadastro ? (
              <div className="alert alert-success text-center">
                <small className="d-block">
                  Cadastro realizado com sucesso!
                </small>
                <small> Faça login para continuar</small>
              </div>
            ) : null}

            <Link className="d-block text-center" to="/revendedor/cadastro">
              Novo revendedor? Cadastre-se aqui
            </Link>
          </Form>
        )}
      </Formik>
    </div>
  );
}

FormLogin.propTypes = {
  login: PropTypes.func.isRequired,
};

export default FormLogin;
