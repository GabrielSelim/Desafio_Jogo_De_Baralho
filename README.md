# API de Jogo de Baralho

## Descrição
Esta API foi desenvolvida para gerenciar um jogo de baralho, permitindo criar, embaralhar, distribuir cartas, comparar cartas entre jogadores e finalizar o jogo. A API é construída utilizando .NET 8 e segue as melhores práticas de desenvolvimento.

## Endpoints

### Criar Baralho
- **Método**: POST
- **Rota**: `/jogo/criar-baralho`
- **Descrição**: Cria um novo baralho embaralhado.
- **Resposta**: JSON com os dados do baralho criado.

### Distribuir Cartas
- **Método**: POST
- **Rota**: `/jogo/distribuir-cartas`
- **Descrição**: Distribui cartas para os jogadores.
- **Parâmetros**: `deckId` - ID do baralho, `numeroDeJogadores` - Número de jogadores.
- **Resposta**: JSON com a lista de jogadores e suas cartas.

### Embaralhar Cartas
- **Método**: POST
- **Rota**: `/jogo/embaralhar-cartas`
- **Descrição**: Embaralha as cartas de um baralho existente.
- **Parâmetros**: `deckId` - ID do baralho.
- **Resposta**: JSON com os dados do baralho embaralhado.

### Comparar Cartas
- **Método**: POST
- **Rota**: `/jogo/comparar-cartas`
- **Descrição**: Compara as cartas dos jogadores para determinar o vencedor.
- **Corpo da Requisição**: JSON com a lista de jogadores e suas cartas.
- **Resposta**: JSON com os vencedores e o resultado.

### Finalizar Jogo
- **Método**: POST
- **Rota**: `/jogo/finalizar-jogo`
- **Descrição**: Finaliza o jogo e retorna o baralho ao estado inicial.
- **Parâmetros**: `deckId` - ID do baralho.
- **Resposta**: JSON com os dados do baralho finalizado.

-------------------------------------------------------------------------------------------------------------------------------------------------------------
## Testes Unitários
Os testes unitários são implementados para garantir que cada parte da API funcione conforme o esperado. Utilizamos o framework de testes xUnit para criar e executar os testes. Os testes cobrem os seguintes aspectos:

### Testes de Controladores
- **Descrição**: Verificam se os endpoints da API estão respondendo corretamente.
- **Exemplo**: Testar se o endpoint de criação de baralho retorna o status 200 (OK) e os dados corretos.

### Testes de Serviços
- **Descrição**: Validam a lógica de negócios implementada nos serviços.
- **Exemplo**: Testar se o serviço de distribuição de cartas distribui corretamente as cartas para os jogadores.

### Testes de Repositórios
- **Descrição**: Garantem que as operações de banco de dados estão funcionando corretamente.
- **Exemplo**: Testar se o repositório de baralhos retorna o baralho correto ao consultar o banco de dados.