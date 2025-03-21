# API de Jogo de Baralho

## Descri��o
Esta API foi desenvolvida para gerenciar um jogo de baralho, permitindo criar, embaralhar, distribuir cartas, comparar cartas entre jogadores e finalizar o jogo. A API � constru�da utilizando .NET 8 e segue as melhores pr�ticas de desenvolvimento.

## Endpoints

### Criar Baralho
- **M�todo**: POST
- **Rota**: `/jogo/criar-baralho`
- **Descri��o**: Cria um novo baralho embaralhado.
- **Resposta**: JSON com os dados do baralho criado.

### Distribuir Cartas
- **M�todo**: POST
- **Rota**: `/jogo/distribuir-cartas`
- **Descri��o**: Distribui cartas para os jogadores.
- **Par�metros**: `deckId` - ID do baralho, `numeroDeJogadores` - N�mero de jogadores.
- **Resposta**: JSON com a lista de jogadores e suas cartas.

### Embaralhar Cartas
- **M�todo**: POST
- **Rota**: `/jogo/embaralhar-cartas`
- **Descri��o**: Embaralha as cartas de um baralho existente.
- **Par�metros**: `deckId` - ID do baralho.
- **Resposta**: JSON com os dados do baralho embaralhado.

### Comparar Cartas
- **M�todo**: POST
- **Rota**: `/jogo/comparar-cartas`
- **Descri��o**: Compara as cartas dos jogadores para determinar o vencedor.
- **Corpo da Requisi��o**: JSON com a lista de jogadores e suas cartas.
- **Resposta**: JSON com os vencedores e o resultado.

### Finalizar Jogo
- **M�todo**: POST
- **Rota**: `/jogo/finalizar-jogo`
- **Descri��o**: Finaliza o jogo e retorna o baralho ao estado inicial.
- **Par�metros**: `deckId` - ID do baralho.
- **Resposta**: JSON com os dados do baralho finalizado.

-------------------------------------------------------------------------------------------------------------------------------------------------------------
## Testes Unit�rios
Os testes unit�rios s�o implementados para garantir que cada parte da API funcione conforme o esperado. Utilizamos o framework de testes xUnit para criar e executar os testes. Os testes cobrem os seguintes aspectos:

### Testes de Controladores
- **Descri��o**: Verificam se os endpoints da API est�o respondendo corretamente.
- **Exemplo**: Testar se o endpoint de cria��o de baralho retorna o status 200 (OK) e os dados corretos.

### Testes de Servi�os
- **Descri��o**: Validam a l�gica de neg�cios implementada nos servi�os.
- **Exemplo**: Testar se o servi�o de distribui��o de cartas distribui corretamente as cartas para os jogadores.

### Testes de Reposit�rios
- **Descri��o**: Garantem que as opera��es de banco de dados est�o funcionando corretamente.
- **Exemplo**: Testar se o reposit�rio de baralhos retorna o baralho correto ao consultar o banco de dados.

-------------------------------------------------------------------------------------------------------------------------------------------------------------
## Executando a API e os Testes com Docker

### Pr�-requisitos
- Docker
- Docker Compose

### Executando a API

1. **Construa e inicie os servi�os Docker:**

   No diret�rio raiz do projeto, execute o seguinte comando para construir e iniciar os servi�os Docker:

   docker-compose up --build

2. **Acessando a API:**

   A API estar� dispon�vel em `http://localhost:5000`.

3. **Acessando o Swagger:**

   O Swagger da API estar� dispon�vel em `http://localhost:5000/swagger`.

### Executando os Testes

1. **Construa e inicie os servi�os Docker:**

   No diret�rio raiz do projeto, execute o seguinte comando para construir e iniciar os servi�os Docker:

   docker-compose up --build

2. **Acessando os Resultados dos Testes:**

   Ap�s a execu��o dos testes, os resultados ser�o gerados em um arquivo `test_results.trx` na pasta `TestResults` no diret�rio raiz do projeto no host.

   Voc� pode acessar o arquivo de resultados de teste em:
   ./TestResults/test_results.trx

3. **Exibindo os Resultados dos Testes no Console:**

   Para exibir o conte�do do arquivo `test_results.trx` no console, voc� pode usar o seguinte comando