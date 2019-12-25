/* global __API_URL__ */
import axios from "axios";

const baseUrl = `${__API_URL__}/saldo`;

export async function valorAcumulado() {
  const response = await axios.get(baseUrl);
  return response.data;
}
