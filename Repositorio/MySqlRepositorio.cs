using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

namespace Repositorio
{
    public abstract class MySqlRepositorio
    {
        protected readonly string ConnectionString = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;

        protected MySqlConnection AbrirConexao()
        {
            var conexao = new MySqlConnection(ConnectionString);
            conexao.Open();
            return conexao;
        }

        protected void FecharConexao(MySqlCommand comando)
        {
            if (comando.Connection != null && comando.Connection.State == ConnectionState.Open)
            {
                comando.Connection.Close();
            }
        }

        protected MySqlCommand IniciarComando(MySqlConnection conexao, bool usarTransacao)
        {
            var comando = conexao.CreateCommand();
            if (usarTransacao)
            {
                comando.Transaction = conexao.BeginTransaction();
            }
            return comando;
        }
    }
}