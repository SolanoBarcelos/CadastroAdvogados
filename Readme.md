# Sistema de Cadastro de Advogados

## DESCRIÇÃO DO PROJETO
Este sistema é um CRUD de advogados, desenvolvido em ASP.NET MVC 5. 

## ARQUITETURA E PADROES

- **Camada de Domínio:** Entidades marcadas como [Serializable] e organizadas em namespaces específicos.

- **Camada de Repositório:** Implementação do Repository Pattern com separação entre Interface e Implementação.

- **Camada Web:** MVC 5 utilizando ViewModels para tráfego de dados e Views organizadas por objeto.

- **Governança de Código (Shift-Left):** Automação de regras estruturais garantindo o cumprimento de padrões arquiteturais diretamente na esteira de CI (Integração Contínua). Ler "Sumary" de "ARCHITECTURE UNIT TEST" do arquivo "PadraoNomenclaturaTests.cs" no projeto "CadastroAdvogados.Tests" para mais detalhes.

## TECNOLOGIAS UTILIZADAS

- .NET Framework 4.8 / C#.

- MySQL (Docker).

- jQuery
 
- Testes de Arquitetura: MSTest e Fluent Assertions

- Integração Contínua (CI): GitHub Actions

## COMO EXECUTAR (Localhost)

1 - No terminal, dentro da pasta raiz do projeto, execute:
```bash
docker-compose up -d
```
ou
```bash
docker compose up -d
```
(Os comandos acima criarão o banco "db_advogados" e farão o insert dos dados iniciais do "init.sql" automaticamente).

2 - Abra a solução (.slnx).

3- Clique com o botão direito no projeto "Web" e selecione "Definir como Projeto de Inicialização".

4 - Execute o projeto. O sistema irá abrir na listagem de advogados, a rota padrão configurada no "RouteConfig.cs".

5 - Para remover Containers e os dados, no terminal, execute:
```bash
docker-compose down -v
```
ou
```bash
docker compose down -v
```

## FUNCIONALIDADES

- Listagem: Lista de advogados cadastrados.

- Busca: Filtro por Nome ou ID.

- Cadastro/Edição: Formulário.

- Exclusão: Remoção de registros. 