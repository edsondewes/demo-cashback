/* global __API_URL__ */
import axios from "axios";

const baseUrl = `${__API_URL__}/revendedores`;

export async function cadastrar(revendedor) {
  try {
    await axios.post(baseUrl, revendedor);
  } catch (ex) {
    return { erro: ex.response.data };
  }
}

export async function login(email, senha) {
  try {
    const response = await axios.post(`${baseUrl}/login`, {
      email,
      senha,
    });

    return response.data;
  } catch (ex) {
    return { erro: ex.response.data };
  }
}
