import React, { useState } from "react";
import { Switch, Route } from "react-router-dom";
import axios from "axios";
import LoginContext from "./LoginContext";

import InfoSistema from "./InfoSistema";
import FormLogin from "./revendedores/FormLogin";
import FormRevendedor from "./revendedores/FormRevendedor";
import FormCompra from "./compras/FormCompra";
import PainelGerencial from "./PainelGerencial";

const authTokenKey = "authToken";

function setAxiosDefaults(token) {
  if (token) {
    axios.defaults.headers.common["authorization"] = `Bearer ${token}`;
  } else {
    delete axios.defaults.headers.common["authorization"];
  }
}

function App() {
  const [usuario, setUsuario] = useState(() =>
    JSON.parse(sessionStorage.getItem(authTokenKey)),
  );

  setAxiosDefaults(usuario ? usuario.token : null);

  function login(obj) {
    sessionStorage.setItem(authTokenKey, JSON.stringify(obj));
    setUsuario(obj);
  }

  function logout() {
    sessionStorage.removeItem(authTokenKey);
    setUsuario(null);
  }

  return (
    <LoginContext.Provider value={{ usuario, logout }}>
      {usuario ? (
        <Switch>
          <Route path="/compra/cadastro">
            <FormCompra />
          </Route>
          <Route path="/">
            <PainelGerencial />
          </Route>
        </Switch>
      ) : (
        <Switch>
          <Route path="/info">
            <InfoSistema />
          </Route>
          <Route path="/revendedor/cadastro">
            <FormRevendedor />
          </Route>
          <Route path="/">
            <FormLogin login={login} />
          </Route>
        </Switch>
      )}
    </LoginContext.Provider>
  );
}

export default App;
