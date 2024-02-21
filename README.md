
# Introdução do Projeto
Este projeto se trata de uma versão bem simplificada de uma Gestão de Portfólio de Carteira de Investimentos gerenciada por uma mesa de operações.

Para desenvolver o projeto, busquei novos design patterns para aprofundar um pouco mais meus conhecimentos e decidi utilizar pela primeira vez o SOLID e o Facade. Como padrão de projeto, utilizei o DDD por já ter mais prática no dia a dia.

Devido ao prazo do projeto, algumas adaptações foram feitas para que as funcionalidades se aderissem ao escopo do projeto.

A seguir, deixo um passo a passo de como executar o programa e como testar suas funcionalidades.

## Executando o Projeto

O primeiro passo para a execução do projeto é rodar o comando "docker-compose up -d" na pasta principal do projeto, onde se encontra o arquivo docker-compose.yml

Após se certificar de que tudo está rodando corretamente, basta abrir a solution "GestaoPortfolio.API.sln" que se encontra na pasta Backend e executar o projeto "GestaoPortfolio.API" diretamente pelo visual studio.

## Testando o Projeto

Quando o projeto está em execução, o banco é criado (ele é criado e apagado em todas as execuções) e uma página do Swagger abre. Os testes das funcionalidades são bem simples, porém é necessário seguir uma ordem para que as informações necessárias estejam preenchidas nas tabelas quando realizar a chamada.

1º - É necessário criar um usuário na rota "usuario/criar", e em seguida utilizar as mesmas informações para fazer o login em "usuario/login". Se tudo estiver certo, é só copiar o token e colar dentro do campo disponível na modal que irá abrir ao clicar no botão de Authorize no topo da página. Importante: o e-mail precisa ter o padrão correto, terminando com "@xxxx.xxx".

2º - É necessário utilizar as rotas de POST para incluir um cliente (cliente/inserir) e um produto (produto/criar), essas são as tabelas mais básicas e não possuem dependências.

3º - Após a criação do cliente e do produto, deve ser criada uma oferta (oferta/criar) utilizando o código do produto que foi criado anteriormente para atrelar ao papel.

4º - Com os passos anteriores, temos todas os dados necessários para realizar nossa primeira operação. Nesse caso, precisa ser obrigatóriamente de compra, pois ainda não há nenhuma posição na carteira do cliente para que a venda seja realizada. Utilizando a rota "operacao/incluir/compra" podemos enviar o codigo da oferta que foi criada, o id do cliente que está realizando a compra e a quantidade da operação.

5º - Podemos verificar no GET da carteira se a posição foi criada. Feito isso, todas as outras rotas estão livres para serem testadas, inclusive a operação de venda. (Obs.: Importante se atentar ao código da oferta e o id do cliente na hora de registrar uma operação, pois essas informações são essenciais para que tudo ocorra conforme esperado).

## Tratamento de Erros e Exceções 
Por conta do curto prazo, o unico tratamento de erro que é feito se refere às quantidades das operações, impossibilitando que um cliente venda mais quantidades do que possui em carteira e que compre mais quantidades do que está disponível na oferta.
Para os demais erros, o certo seria realizar a validação no front-end e ainda assim tratar corretamente no back caso ainda assim conseguisse passar algum dado incorreto, como o código do produto na criação de uma oferta, por exemplo.


## Documentação da API para Teste no Swagger

### Clientes
```http
  GET /cliente/listar
```
Retorna uma lista de todos os clientes cadastrados na aplicação, podendo ser filtrado por IdCliente.
```http
  POST /cliente/incluir
```
Chamada para criação de um novo cliente, o campo de idCliente pode ser desconsiderado.
```http
  PUT /cliente/alterar
```
Chamada para realizar alterações em um cliente já existente.


### Usuario

```http
  POST /usuario/criar
  POST /usuario/login
```
Esses dois métodos POST são usados para controle de acesso, com o primeiro (/criar) é possível fazer o cadastro do usuário que irá utilizar a aplicação e com o segundo (/login) recebemos o token de autenticação caso os dados enviados estejam corretos.

#### Oferta

```http
  GET /oferta/listar
```
Retorna todas as ofertas criadas, podendo ser filtrada por id.
```http
  POST /oferta/criar
```
Essa chamada é utilizada para criar uma nova oferta, podendo desconsiderar o "codigoOferta" no envio.
```http
  PUT /oferta/alterar
```
Essa chamada é utilizada para fazer alguma alteração na oferta, caso seja necessário.

#### Operacao

```http
  GET /operacao/listar
```
Retorna todas as operações armazenadas, podendo ser filtrada por IdOperacao.
```http
  POST /operacao/incluir/venda
```
Esse método cria uma operação de venda (visão cliente) na base, criando também alterações no estoque da oferta e na carteira (posição cliente). Os campos IdOperacao, valorPrecoUnitario, valorTotalOperacao, stauts e dataOperacao são desconsiderados pois são tratados pelo sistema.
```http
  POST /operacao/incluir/compra
```
Esse método cria uma operação de compra (visão cliente) na base, criando também alterações no estoque da oferta e na carteira (posição cliente). Os campos IdOperacao, valorPrecoUnitario, valorTotalOperacao, stauts e dataOperacao são desconsiderados pois são tratados pelo sistema.
```http
  PUT /operacao/alterar
```
Como a operação é um registro de conferência e histórico, o ideal seria que não fossem alterados os campos de dados sobre a compra ou venda, apenas seu status (GRAVADA, CANCELADA, ERRO, etc)


#### Carteira

```http
  GET /carteira/listar
```
Retorna todas as carteiras de clientes, podendo ser filtrada pelo campo de IdCliente para nos trazer a carteira de um único cliente.
```http
  POST /carteira/incluir
```
Insere uma nova posição quando a operação de compra é criada. Apesar de já inserir uma nova posição automaticamente quando a operação de compra de uma nova oferta é criada, disponibilizei para testes.
```http
  PUT /carteira/alterar
```
O ideal, assim como na operação, é que a posição do cliente não seja alterada diretamente por chamada, mas disponibilizei para testes. As alterações feitas quando há uma venda são tratadas diretamente pelo backend da aplicação.

```http
  DELETE /carteira/excluir/{idPosicao}
```
Novamente, método disponibilizado apenas para testes, visto que quando o cliente vende todas suas quantidades de um ativo, a posição já é removida pelo tratamento feito no backend.

#### Produto

```http
  GET /produto/listar
```
Retorna uma lista com todos os produtos cadastrados, sendo possível filtrar por id.
```http
  POST /produto/incluir
```
Cria um novo produto na base, podendo ser utilizado para criar uma nova oferta. Temos um produto cadastrado como testes, que é o CDB.
```http
  PUT /produto/alterar
```
Utilizado para alterar desde o status do produto até sua descrição.

OBS.: Algumas tabelas possuem caráter de registro e histórico, portanto optei por apenas inativar o registro ao invés de deletá-lo.