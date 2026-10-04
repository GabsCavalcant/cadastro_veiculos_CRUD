# Cadastro de Veículos (CRUD)

Aplicação de console em **C# (.NET 8)** para cadastrar, alterar, excluir, listar e pesquisar veículos (por placa ou modelo).

## Tecnologias utilizadas

- Linguagem: C# (.NET 8)
- Banco de dados: **MySQL**
- Acesso ao banco: MySqlConnector (pacote NuGet)

> **Observação:** o enunciado indica C# e SQL Server como tecnologias preferenciais. Neste projeto foi utilizado **MySQL**. Por isso o script SQL está em sintaxe MySQL e o campo `Id` usa `AUTO_INCREMENT` (equivalente ao `IDENTITY` do SQL Server). Os pré-requisitos e os passos para executar estão nas seções abaixo.

## Estrutura do projeto

```
cadastro-veiculos/
├── sql/criar_banco.sql        -> script de criação do banco e da tabela (MySQL)
├── csharp/
│   ├── Veiculo.cs             -> classe com os dados do veículo
│   ├── VeiculoRepository.cs   -> acesso ao banco (comandos SQL)
│   ├── Program.cs             -> menu, leitura dos dados e validações
│   └── CadastroVeiculos.csproj
└── prints/                    -> capturas de tela da aplicação
```

## Regras de negócio

- Todos os campos são obrigatórios.
- A placa é gravada em letras maiúsculas.
- Placa e chassis não podem ser duplicados (garantido pelo `UNIQUE` no banco; o erro é mostrado de forma amigável).
- O chassis deve ter exatamente 17 caracteres.
- O ano deve ser um número entre 1900 e o ano atual + 1.
- Campos não podem ultrapassar o tamanho definido no banco.

## Pré-requisitos

- MySQL instalado e em execução (e o MySQL Workbench ou outro cliente para rodar o script)
- .NET SDK 8.0

## Como executar

### 1. Criar o banco

Abra o MySQL Workbench, conecte no seu servidor, abra o arquivo `sql/criar_banco.sql` e execute o script inteiro.
Ou, pelo terminal:

```bash
mysql -u root -p < sql/criar_banco.sql
```

### 2. Configurar a conexão

No início do arquivo `csharp/Program.cs`, ajuste usuário e senha na variável `conexao`:

```csharp
string conexao = "Server=localhost;Port=3306;Database=CadastroVeiculos;User=root;Password=SuaSenha;";
```

### 3. Executar

```bash
cd csharp
dotnet run
```

## Prints da aplicação

As capturas de tela da aplicação em funcionamento estão na pasta `prints/`.
