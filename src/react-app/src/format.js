export function data(valor) {
  return new Date(valor).toLocaleDateString("pt-BR");
}

export function dinheiro(valor) {
  return valor.toLocaleString("pt-BR", {
    style: "currency",
    currency: "BRL",
  });
}

export function percentual(valor) {
  return valor * 100 + "%";
}
