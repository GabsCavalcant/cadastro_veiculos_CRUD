using MySqlConnector;

namespace CadastroVeiculos;

// classe que convesa com o banco de dados
// utilize parâmetros (@Placa, @Marca etc] em vez de concatenar texto em si, para evitar SQL injection.
public class VeiculoRepository
{
    private readonly string _conexao;

    public VeiculoRepository(string conexao)
    {
        _conexao = conexao;
    }

    // CREATE
    public void Inserir(Veiculo v)
    {
        string sql = @"INSERT INTO Veiculo (Placa, Marca, Modelo, Cor, Ano, Porte, TipoCarga, Chassis)
                       VALUES (@Placa, @Marca, @Modelo, @Cor, @Ano, @Porte, @TipoCarga, @Chassis)";

        using var conn = new MySqlConnection(_conexao);
        using var cmd = new MySqlCommand(sql, conn);
        PreencherParametros(cmd, v);
        conn.Open();
        cmd.ExecuteNonQuery();
    }

    // UPDATE - retorna false se o Id não existir
    public bool Alterar(Veiculo v)
    {
        string sql = @"UPDATE Veiculo
                       SET Placa=@Placa, Marca=@Marca, Modelo=@Modelo, Cor=@Cor,
                           Ano=@Ano, Porte=@Porte, TipoCarga=@TipoCarga, Chassis=@Chassis
                       WHERE Id=@Id";

        using var conn = new MySqlConnection(_conexao);
        using var cmd = new MySqlCommand(sql, conn);
        PreencherParametros(cmd, v);
        cmd.Parameters.AddWithValue("@Id", v.Id);
        conn.Open();
        return cmd.ExecuteNonQuery() > 0; // quantidade de linhas afetadas
    }

    // DELETE - retorna false se o Id não existir
    public bool Excluir(int id)
    {
        using var conn = new MySqlConnection(_conexao);
        using var cmd = new MySqlCommand("DELETE FROM Veiculo WHERE Id=@Id", conn);
        cmd.Parameters.AddWithValue("@Id", id);
        conn.Open();
        return cmd.ExecuteNonQuery() > 0;
    }

    // READ - lista todos
    public List<Veiculo> ListarTodos()
    {
        using var conn = new MySqlConnection(_conexao);
        using var cmd = new MySqlCommand("SELECT * FROM Veiculo ORDER BY Id", conn);
        return Ler(cmd, conn);
    }

    // READ - pesquisa por parte da placa ou do modelo
    public List<Veiculo> Pesquisar(string texto)
    {
        string sql = "SELECT * FROM Veiculo WHERE Placa LIKE @Texto OR Modelo LIKE @Texto ORDER BY Id";

        using var conn = new MySqlConnection(_conexao);
        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Texto", "%" + texto + "%");
        return Ler(cmd, conn);
    }

    // executa o comando SQL e devolve a lista de veículos resultante
    private List<Veiculo> Ler(MySqlCommand cmd, MySqlConnection conn)
    {
        var lista = new List<Veiculo>();
        conn.Open();
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            lista.Add(new Veiculo
            {
                Id = (int)reader["Id"],
                Placa = (string)reader["Placa"],
                Marca = (string)reader["Marca"],
                Modelo = (string)reader["Modelo"],
                Cor = (string)reader["Cor"],
                Ano = (int)reader["Ano"],
                Porte = (string)reader["Porte"],
                TipoCarga = (string)reader["TipoCarga"],
                Chassis = (string)reader["Chassis"]
            });
        }
        return lista;
    }

    // funcao para preencher os parâmetros do comando SQL com os valores do objeto Veiculo
    private void PreencherParametros(MySqlCommand cmd, Veiculo v)
    {
        cmd.Parameters.AddWithValue("@Placa", v.Placa);
        cmd.Parameters.AddWithValue("@Marca", v.Marca);
        cmd.Parameters.AddWithValue("@Modelo", v.Modelo);
        cmd.Parameters.AddWithValue("@Cor", v.Cor);
        cmd.Parameters.AddWithValue("@Ano", v.Ano);
        cmd.Parameters.AddWithValue("@Porte", v.Porte);
        cmd.Parameters.AddWithValue("@TipoCarga", v.TipoCarga);
        cmd.Parameters.AddWithValue("@Chassis", v.Chassis);
    }
}
