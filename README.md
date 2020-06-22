# ebank-account

## Projeto

Um microserviço para realizar transferência entre contas correntes utilizando:

- DDD + CQRS
- EF Core
- Swagger
- Shared Library : Criei uma biblioteca de CQRSHelper para ser utilizada nesse e nos próximos microserviços
  - <https://github.com/emanuelfqueiroz/cqrs-helper>
  - <https://www.nuget.org/packages/CQRSHelper/1.0.1>

- Camadas
  
    ![Estrutura](https://github.com/emanuelfqueiroz/ebank-account/raw/master/docs/Estrutura.png)

- Diagrama
    ![Digrama](
https://raw.githubusercontent.com/emanuelfqueiroz/ebank-account/master/docs/DatabaseDiagram.png)

- Índices e Restrições de Banco:
    ![Indices](https://github.com/emanuelfqueiroz/ebank-account/raw/master/docs/Unique%20_Indexes.png)

### TO DO

- Criar docker-compose.yaml 
  - container do microserviço
  - container com Sql Server + Dados carregados
    - <https://raw.githubusercontent.com/emanuelfqueiroz/ebank-account/master/data/initialize.sql>
- Criar e registrar Imagens no DockerHub
- Implementação de Segurança utilizando JWT
- Melhorar Testes da camada de aplicação
- Tratamento de Concorrência/Semáforos das contas que realizam a transferência bancária

## Desenvolvimento de Software

### DDD

Aplicações legadas sofrem em expressar as responsabilidades da aplicação. Visualizamos códigos procedurais onde as regras de negócio são distribuídas no banco de dados, em controllers e em views.

Domain-driven-design é uma abordagem que foca em evidenciar o domínio da aplicação. Ou seja, os processos do mundo real são representados em um único lugar: no domínio da aplicação.

Apresenta também as boas práticas para o domínio: liguagem ubiqua, bounded contexts, shared kernel e  independência outras estruturas como persistência, infra, controllers, serviços e views.

### Arquitetura de Microserviços

Segragação de um Sistema em contextos/domínios bem definidos. Cada domínio passa a ser aplicação interdependente que se comunica com as demais via http (geralmente).

Vantagens:

- Escalabilidade 
- Facilita a compreensão de cada domínio
- Facilita realização de testes unitários
- Facilita a evolução do sistema.  Ex: alterar banco de dados, linguagem, versão de um framework
- Pode ajudar na organização de equipes/squads
- Permite estratégias de release como Blue-green/ canary release/ testes AB

Desafios:

- Exige maior maturidade do time
- Maior complexidade na orquestração da aplicação e na realização de testes integrados 
- **Coreografia**  de Serviços
- Pode ser mais difícil compreender o sistema como um todo
- Exige novas boas práticas:
- Trackear requisições de ponta a ponta. ex: correlationalIds
- Logs e organização dessas informações
- Monitoramento dos serviços
- Service Registry / discovery
- Bancos isolados por microservicos
- Client libraries
- Shared libraries / sources
- Backups / versionamentos de apis/ Disaster Recovery/ api keys / segurança e autenticação / etc...
- Shotgun efect: quando uma simples modificação afeta multiplos microserviços

:: Microserviços não são aconselhados em pequenas/médias aplicações ou quando não a escalabilidade não é um fator essencial.

### Requisição Síncrona vs Assíncrona [ASP.NET]

Na comunicação síncrona, as threads ficam bloqueadas aguardando as respostas de IO.

Para melhorar o aproveitamento do processador e recursos do Thread pool utilizamos chamadas assincronas [seguindo o padrão *"TAP Pattern"*].
Quando a requisição precisa esperar por IO ela "hiberna" ,libera a thread e só retorna após a resposta de IO.

Geralmente se usa chamdas assincronas para melhorar o aproveitamento da "thread pool" e evitar Alto percentual de threads paradas aguardando IO.

Evita-se chamadas assíncronas quando em alguns cenários onde há custo elevado na "troca de contexto" [dados em memória], em requests que exigem focadas em CPU [baixo ou nenhum IO] ou quando a capacidade de IO é menor que o volume de requisições.
