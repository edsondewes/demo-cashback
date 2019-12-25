# Demo Cashback React + dotnet!  
  
Este é um aplicativo de demonstração, com objetivo de criar um sistema de cashback para revendedores.

O sistema é utiliza as seguintes tecnologias:
- .NET Core: API REST (backend)
- React: cliente web (frontend)
- Mongodb: banco de dados
- Traefik: proxy reverso


## Rodando o sistema com docker-compose

### Configurações de variáveis de ambiente
Antes de iniciar, precisamos configurar dois parâmetros necessários para a API:
1. URL da API externa de saldo acumulado
2. Token de acesso a API

Abra o arquivo **docker-compose.yml** em seu editor preferido, e altere as seguintes variáveis de ambiente:
```yml
CashbackApi__Token: "token api"
CashbackApi__Url: "url api"
```
Exemplo de configuração:
```yml
CashbackApi__Token: "Afg749FKkoaPlFKpER"
CashbackApi__Url: "https://url.api.us-east-1.amazonaws.com/v1/cashback"
```

### Executando os containers

Acesse a pasta raiz do projeto  e inicie o os serviços utilizando docker-compose:
```bash
docker-compose up -d
```
O docker-compose irá fazer o build das imagens e iniciar o sistema em http://localhost:8080.  
Também será exposta a interface de administração do Traefik (proxy) em http://localhost:8081.  
Caso queira utilizar outras portas, basta alterar o *docker-compose.yml*. 

Os módulos estarão disponíveis nos seguintes endereços:

|Url| Módulo  |
|--|--|
| / |  App React para cadastro de revendedores e compras |
| /api | API REST .NET |


Agora é só acessar http://localhost:8080 no seu navegador e testar!
