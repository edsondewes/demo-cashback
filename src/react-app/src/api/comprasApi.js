/* global __API_URL__ */
import axios from "axios";

const baseUrl = `${__API_URL__}/compras`;

export async function atualizar(compra) {
  try {
    await axios.patch(baseUrl, compra);
  } catch (ex) {
    return { erro: ex.response.data };
  }
}

export async function cadastrar(compra) {
  try {
    await axios.post(baseUrl, compra);
  } catch (ex) {
    return { erro: ex.response.data };
  }
}

export async function excluir(codigo) {
  try {
    const response = await axios.delete(baseUrl, {
      data: { codigo },
    });
    return response.data;
  } catch (ex) {
    return { erro: ex.response.data };
  }
}

export async function listar() {
  const response = await axios.get(baseUrl);
  return response.data;
}
