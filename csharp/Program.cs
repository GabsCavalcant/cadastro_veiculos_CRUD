using CadastroVeiculos;
using MySqlConnector;

// caso vc utilizar na maquina mude aqui, o usuario e senha!1!!
string conexao = "Server=localhost;Port=3306;Database=CadastroVeiculos;User=root;Password=;";

var repo = new VeiculoRepository(conexao);
string opcao;


//Menu principal via console, para mostrar a funcionalidade do CRUD
do
{
    Console.WriteLine("\n===== CADASTRO DE VEÍCULOS =====");
    Console.WriteLine("1 - Cadastrar");
    Console.WriteLine("2 - Alterar");
    Console.WriteLine("3 - Excluir");
    Console.WriteLine("4 - Listar todos");
    Console.WriteLine("5 - Pesquisar por placa ou modelo");
    Console.WriteLine("0 - Sair");
    Console.Write("Opção: ");
    opcao = Console.ReadLine() ?? "";


    //testar quais opcões foram escolhidas, e chamar a função correspondente
    try
    {
        switch (opcao)
        {
            case "1": Cadastrar(); break;
            case "2": Alterar(); break;
            case "3": Excluir(); break;
            case "4": Mostrar(repo.ListarTodos()); break;
            case "5":
                Console.Write("Digite a placa ou o modelo: ");
                Mostrar(repo.Pesquisar(Console.ReadLine() ?? ""));
                break;
            case "0": Console.WriteLine("Encerrando..."); break;
            default: Console.WriteLine("Opção inválida."); break;
        }
    }
    catch (MySqlException ex)
    {
        // caso de o numero do erro seja 1062, significa que houve uma violação de chave única (codigo de erro do MySQL)
        if (ex.Number == 1062)
            Console.WriteLine("Erro: já existe um veículo com essa placa ou esse chassis.");
        else
            Console.WriteLine("Erro ao acessar o banco de dados. Verifique a conexão.");
    }
} while (opcao != "0");


//  funções do menu

void Cadastrar()
{
    Veiculo? v = LerVeiculo();
    if (v == null) return; // dados inválidos: a mensagem já foi exibida

    repo.Inserir(v);
    Console.WriteLine("Veículo cadastrado com sucesso!");
}
//funcao parar alterar um veiculo, onde pede seu id
void Alterar()
{
    Console.Write("Id do veículo que deseja alterar: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("Id inválido.");
        return;
    }

    Veiculo? v = LerVeiculo();
    if (v == null) return;
    //verifica se o id existe, caso nao exista, mostra a mensagem de erro
    v.Id = id;
    if (repo.Alterar(v))
        Console.WriteLine("Veículo alterado com sucesso!");
    else
        Console.WriteLine("Veículo não encontrado.");
}


//funncao para excluir um veiculo, tbm dependendo do seu id e caso nao exista, mostra a mensagem de erro
void Excluir()
{
    Console.Write("Id do veículo que deseja excluir: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("Id inválido.");
        return;
    }

    if (repo.Excluir(id))
        Console.WriteLine("Veículo excluído com sucesso!");
    else
        Console.WriteLine("Veículo não encontrado.");
}

// funcao para a leitura dos dados do veiculo, com validações de campos obrigatórios, tamanho máximo e ano válido
Veiculo? LerVeiculo()
{
    var v = new Veiculo();

    Console.Write("Placa: ");
    v.Placa = (Console.ReadLine() ?? "").Trim().ToUpper(); // placa sempre em maiúsculas padrao br
    Console.Write("Marca: ");
    v.Marca = (Console.ReadLine() ?? "").Trim();
    Console.Write("Modelo: ");
    v.Modelo = (Console.ReadLine() ?? "").Trim();
    Console.Write("Cor: ");
    v.Cor = (Console.ReadLine() ?? "").Trim();
    Console.Write("Ano: ");
    string ano = (Console.ReadLine() ?? "").Trim();
    Console.Write("Porte: ");
    v.Porte = (Console.ReadLine() ?? "").Trim();
    Console.Write("Tipo de carga: ");
    v.TipoCarga = (Console.ReadLine() ?? "").Trim();
    Console.Write("Chassis: ");
    v.Chassis = (Console.ReadLine() ?? "").Trim().ToUpper();

    // validações campo obrigatórios, tamanho máximo e ano válido
    if (v.Placa == "" || v.Marca == "" || v.Modelo == "" || v.Cor == "" ||
        ano == "" || v.Porte == "" || v.TipoCarga == "" || v.Chassis == "")
    {
        Console.WriteLine("Erro: todos os campos são obrigatórios.");
        return null;
    }
    if (!int.TryParse(ano, out int anoNumero) || anoNumero < 1900 || anoNumero > DateTime.Now.Year + 1)
    {
        Console.WriteLine("Erro: ano inválido.");
        return null;
    }
    if (v.Placa.Length > 10 || v.Marca.Length > 50 || v.Modelo.Length > 50 ||
        v.Cor.Length > 30 || v.Porte.Length > 20 || v.TipoCarga.Length > 30)
    {
        Console.WriteLine("Erro: algum campo ultrapassou o tamanho máximo permitido.");
        return null;
    }
    if (v.Chassis.Length != 17)
    {
        Console.WriteLine("Erro: o chassis deve ter exatamente 17 caracteres.");
        return null;
    }

    v.Ano = anoNumero;
    return v;
}

//funcao para mostrara a lista de veiculos, pegando do banco de dados, caso n haja nenhuma retorna
void Mostrar(List<Veiculo> lista)
{
    if (lista.Count == 0)
    {
        Console.WriteLine("Nenhum veículo encontrado.");
        return;
    }

    foreach (var v in lista)
    {
        Console.WriteLine($"[{v.Id}] {v.Placa} | {v.Marca} {v.Modelo} | {v.Cor} | {v.Ano} | " +
                          $"Porte: {v.Porte} | Carga: {v.TipoCarga} | Chassis: {v.Chassis}");
    }
}