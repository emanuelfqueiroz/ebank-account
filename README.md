# ebank-account

## Projeto

Um microserviço para realizar transferência entre contas correntes utilizando:

- DDD + CQRS
- EF Core
- Swagger
- Shared Library `CQRSHelper` 
  - biblioteca de minha autoria para uso nos microserviços.
    - <https://github.com/emanuelfqueiroz/cqrs-helper>
    - <https://www.nuget.org/packages/CQRSHelper/1.0.1>


- Camadas
  
    ![Estrutura](https://github.com/emanuelfqueiroz/ebank-account/raw/master/docs/Estrutura.png)

- Diagrama
    ![Digrama](
https://raw.githubusercontent.com/emanuelfqueiroz/ebank-account/master/docs/DatabaseDiagram.png)

- Índices e Restrições de Banco

![Indices](https://github.com/emanuelfqueiroz/ebank-account/raw/master/docs/Unique%20_Indexes.png)

### TO DO

- Criar docker-compose.yaml 
  - container do microserviço
  - container com Sql Server + Dados carregados
    - <https://raw.githubusercontent.com/emanuelfqueiroz/ebank-account/master/data/initialize.sql>
- Criar e registrar Imagens no DockerHub
- Implementação de Segurança utilizando JWT
- Melhorar Testes da camada de aplicação
- Transação desde a leitura das contas;
  - usar Database Isolation level serializable?
  - pesquisar solução

### PROJETO SIMILAR

Segue um projeto pessoal que encontra a menor rota possível utilizando o algoritmo de Dijkstra. 
[disponibilizado com **Docker**]

https://gitlab.com/emanuelfqueiroz/routes-project

![Estrutura](https://gitlab.com/emanuelfqueiroz/routes-project/-/raw/master/doc/images//project_struture.png)


## Desenvolvimento de Software

### DDD

Aplicações legadas sofrem em expressar as responsabilidades da aplicação. Encontramos códigos procedurais com regras de negócio distribuídas em banco de dados, controllers, views e em qualquer outra camada.

Domain-driven-design é uma abordagem que foca em evidenciar o domínio da aplicação. Ou seja, os processos do mundo real são representados especificamente na camada de domínio utilizando boas práticas como: liguagem ubiqua, bounded contexts, shared kernel e, principalmente, independência de outras camadas como por exemplo: persistência.

### Arquitetura de Microserviços

A arquitetura de `microservices` segrega um sistema corporativo em diversas aplicações com contextos/domínios bem definidos. 
Cada aplicação é estruturalmente independente porém atua de forma colaborativa com as demais aplicações.

Vantagens:

- Escalabilidade 
- Facilita a compreensão de cada domínio
- Facilita realização de testes unitários
- Facilita a evolução do sistema.  Ex: alterar banco de dados, linguagem, versão de um framework
- Pode ajudar na organização de equipes/squads
- Permite estratégias de release como Blue-green/ canary release/ testes AB
- Desconstroi os problemáticos "monólitos"...

Desafios:

- Exige maior maturidade do time/squad
- Maior complexidade na orquestração da aplicação e na realização de testes integrados 
- Exige **Coreografia**  entre os serviços
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

:: Microserviços não são aconselhados em pequenas/médias aplicações ou quando a escalabilidade não é um fator essencial.

### Requisição Síncrona vs Assíncrona [ASP.NET]

Na comunicação síncrona, as threads ficam bloqueadas aguardando respostas de IO.

Ao utilizar chamadas assíncronas, a requisição não precisa esperar por IO [bloqueando a thread]: ela "hiberna", libera a thread para outras requisições e só retorna após a respectiva resposta.

Geralmente se usa chamdas assincronas para melhorar o aproveitamento da "thread pool" e evitar alto percentual de threads paradas aguardando IO.

Evita-se chamadas assíncronas nos cenários onde há custo elevado na "troca de contexto" [dados em memória], em requests que são exclusivamente de CPU [baixo ou nenhum IO] ou quando a capacidade de IO é menor que o volume de requisições.
