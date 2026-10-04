namespace CadastroVeiculos;

// classe que representa um veículo (uma linha da tabela Veiculo)
public class Veiculo
{
    public int Id { get; set; }
    public string Placa { get; set; } = "";
    public string Marca { get; set; } = "";
    public string Modelo { get; set; } = "";
    public string Cor { get; set; } = "";
    public int Ano { get; set; }
    public string Porte { get; set; } = "";
    public string TipoCarga { get; set; } = "";
    public string Chassis { get; set; } = "";
}
