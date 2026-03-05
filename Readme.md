# Sistema de Cadastro de Advogados

## DESCRIÇÃO DO PROJETO
Este sistema é um CRUD de advogados, desenvolvido em ASP.NET MVC 5. 

## ARQUITETURA E PADROES

- **Camada de Domínio:** Entidades marcadas como [Serializable] e organizadas em namespaces específicos.

- **Camada de Repositório:** Implementação do Repository Pattern com separação entre Interface e Implementação.

- **Camada Web:** MVC 5 utilizando ViewModels para tráfego de dados e Views organizadas por objeto.

## TECNOLOGIAS UTILIZADAS

- .NET Framework 4.8 / C#.

- MySQL (Docker).

- jQuery

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

3 - Execute o projeto. O sistema irá abrir na listagem de advogados, a rota padrão configurada no "RouteConfig.cs".

4 - Para remover Containers e os dados, no terminal, execute:
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