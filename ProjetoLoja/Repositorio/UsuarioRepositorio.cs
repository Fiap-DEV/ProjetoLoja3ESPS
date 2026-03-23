using MySql.Data.MySqlClient;
using ProjetoLoja.Interfaces;
using ProjetoLoja.Models;

namespace ProjetoLoja.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        //Variavel que guada o endereço para entrar no banco de dados
        private readonly string _connectionString;

        public UsuarioRepositorio(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("Conexao");
        }

        public LoginViewModel? Validar(string email, string senha)
        {
            using var con = new MySqlConnection(_connectionString);
            con.Open();
            var cmd = new MySqlCommand("SELECT * FROM Usuarios WHERE Email =@email AND Senha=@senha",con);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@senha", senha);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new LoginViewModel
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Nome = reader["Nome"].ToString()!,
                    Email = reader["Email"].ToString()!,
                    Nivel = reader["Nivel"].ToString()!,
                };

            }
            return null;
        }
    }
}
