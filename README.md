# mini-ecommerce

### Projeto utilizando dotnet core 5 dbfirst.
#### github actions está configurado para rodar testes a cada push

pasta "ecom" contem o projeto

pasta "ecom.Tests" contem alguns testes

Para rodar utilize o comando **´dotnet run´** na pasta ecom e abra a url **/swagger**

Para rodar os testes utilize o comando **´dotnet test´** na pasta ecom.Test

- Criar um produto
	- A entidade de Produto deve conter pelo menos: Nome, Estoque e Valor
	- O valor do produto não pode ser negativo
- Atualizar um produto
- Deletar um produto
- Listar os produtos
	- Visualizar um produto específico
	- Ordenar os produtos por diferentes campos
	- Buscar produto pelo nome

Não Funcionais
- Subir o código em um repositório público no github
- README no projeto com breve explicação dos projetos incluídos em sua solution
- Utilizar .NET Core 3.1 ou .NET 5
- Utilizar o Entity Framework
	- Pode usar code-first ou db-first, mas o uso deve estar detalhado no README
	- Popular a base de dados com 5 produtos ao iniciar a aplicação

Bônus
- Utilizar padrões de projeto
- Utilizar github actions para os testes
- Incluir testes unitários
- Incluir testes de integração
