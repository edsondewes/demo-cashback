import React from "react";
import { Formik, Form, Field, ErrorMessage } from "formik";
import { Link, useLocation, useHistory } from "react-router-dom";
import { atualizar, cadastrar } from "../api/comprasApi";

function FormCompra() {
  const location = useLocation();
  const history = useHistory();

  const modoEdicao = !!location.state;

  return (
    <div className="container-card container-form">
      <h1 className="titulo">Cadastro de compras</h1>
      <Formik
        initialValues={
          modoEdicao
            ? {
                codigo: location.state.codigo,
                valor: location.state.valor,
                // Data é recebida no formato ISO
                // e o input de data só reconhece a parte de datas.
                // Então utilizamos somente a primeira parte
                data: location.state.data.split("T")[0],
              }
            : { codigo: "", valor: "", data: "" }
        }
        onSubmit={async (valores, { setErrors }) => {
          const resultado = await (modoEdicao
            ? atualizar(valores)
            : cadastrar(valores));

          if (resultado && resultado.erro) {
            setErrors(resultado.erro);
          } else {
            history.push("/", { codigo: valores.codigo, edicao: modoEdicao });
          }
        }}
      >
        {({ isSubmitting }) => (
          <Form>
            <div className="form-group">
              <label htmlFor="codigo">Código</label>
              <Field
                type="text"
                name="codigo"
                id="codigo"
                className="form-control"
                required="required"
                maxLength="10"
                title="Código"
                disabled={modoEdicao}
              />
              <ErrorMessage
                name="codigo"
                component="small"
                className="form-text text-muted"
              />
            </div>
            <div className="form-group">
              <label htmlFor="valor">Valor</label>
              <Field
                type="number"
                name="valor"
                id="valor"
                className="form-control"
                required="required"
                title="Valor"
                min="1"
                step="0.01"
              />
            </div>
            <div className="form-group">
              <label htmlFor="data">Data</label>
              <Field
                type="date"
                name="data"
                id="data"
                className="form-control"
                required="required"
                title="Data da compra"
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

export default FormCompra;
