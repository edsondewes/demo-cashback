import React from "react";
import { Formik, Form, Field, ErrorMessage } from "formik";
import { Link, useHistory } from "react-router-dom";
import { cadastrar } from "../api/revendedoresApi";

function validarCpf(valor) {
  let erro;
  if (valor && !/^\d+$/i.test(valor)) {
    erro = "CPF deve conter apenas números";
  }
  return erro;
}

function FormRevendedor() {
  const history = useHistory();

  return (
    <div className="container-card container-form">
      <h1 className="titulo">Cadastro de novo revendedor</h1>
      <Formik
        initialValues={{ email: "", cpf: "", nome: "", senha: "" }}
        onSubmit={async (valores, { setErrors }) => {
          const resultado = await cadastrar(valores);
          if (resultado && resultado.erro) {
            setErrors(resultado.erro);
          } else {
            history.push("/", { email: valores.email });
          }
        }}
      >
        {({ isSubmitting }) => (
          <Form>
            <div className="form-group">
              <label htmlFor="email">E-mail</label>
              <Field
                type="email"
                name="email"
                id="email"
                className="form-control"
                required="required"
                maxLength="50"
                title="E-mail"
              />
              <ErrorMessage
                name="email"
                component="small"
                className="form-text text-muted"
              />
            </div>
            <div className="form-group">
              <label htmlFor="cpf">CPF</label>
              <Field
                type="text"
                name="cpf"
                id="cpf"
                className="form-control"
                required="required"
                title="CPF"
                maxLength="11"
                validate={validarCpf}
              />
              <ErrorMessage
                name="cpf"
                component="small"
                className="form-text text-muted"
              />
            </div>
            <div className="form-group">
              <label htmlFor="nome">Nome completo</label>
              <Field
                type="text"
                name="nome"
                id="nome"
                className="form-control"
                required="required"
                maxLength="50"
                title="Nome completo"
              />
            </div>
            <div className="form-group">
              <label htmlFor="senha">Senha</label>
              <Field
                type="password"
                name="senha"
                id="senha"
                className="form-control"
                required="required"
                maxLength="20"
                title="Senha"
              />
            </div>
            <input
              className="btn btn-lg btn-primary btn-entrar"
              type="submit"
              disabled={isSubmitting}
              value="Salvar"
            />
            <Link className="d-block text-center" to="/">
              Voltar
            </Link>
          </Form>
        )}
      </Formik>
    </div>
  );
}

export default FormRevendedor;
