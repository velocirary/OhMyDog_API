# OhMyDog_API - Documentação da API <img src="https://github.com/velocirary/OhMyDog_API/assets/88410319/0fac442a-68ce-4b6b-9a42-d0b2da22925e" width="80">

## Descrição
Este é o repositório da API do projeto OhMyDog, um site de petshop em desenvolvimento como parte do projeto de graduação da faculdade. A API foi desenvolvida em C# e é responsável pelo backend do site.

## Como Começar
Siga as instruções abaixo para configurar e executar a API em sua máquina local para fins de desenvolvimento e teste.

### Pré-requisitos
Certifique-se de ter as seguintes ferramentas instaladas em sua máquina:
- [Visual Studio](https://visualstudio.microsoft.com/): Recomendamos a versão mais recente.
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads): Para armazenar dados relacionais.

### Instalação
1. Clone este repositório para sua máquina local:
   
3. Abra o projeto no Visual Studio.

4. Configure a string de conexão com o banco de dados local para corresponder à sua instância do SQL Server.

5. Inicie a API pressionando F5 ou usando o comando:

A API agora estará em execução localmente em `https://localhost:5001`.

## Uso da API
 Exemplos da utilização de um GET:

- Endpoint: `/api/pets`
- Método: GET
- Descrição: Retorna uma lista de todos os pets disponíveis.
- Parâmetros da consulta: Nenhum
- Resposta:
 ```json
[
  {
    "id": 1,
    "nome": "Zeldris",
    "especie": "Cachorro",
    "idade": 2,
    "proprietario": "Breno"
  },
  {
    "id": 2,
    "nome": "Whiskers",
    "especie": "Gato",
    "idade": 3,
    "proprietario": "Ragnar"
  }
]
 ```
