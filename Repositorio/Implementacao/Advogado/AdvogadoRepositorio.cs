using Dominio;
using Repositorio.Interface;
using System;
using System.Collections.Generic;

namespace Repositorio.Implementacao
{
    public class AdvogadoRepositorio : MySqlRepositorio, IAdvogadoRepositorio
    {
        public void IncluirAdvogado(Advogado pObjAdvogado)
        {
            using (var conexao = AbrirConexao())
            {
                using (var comando = IniciarComando(conexao, usarTransacao: true))
                {
                    try
                    {
                        comando.CommandText = @"INSERT INTO Advogados 
                    (Nome, Senioridade, Logradouro, Bairro, Estado, Cep, Numero, Complemento) 
                    VALUES (@pNome, @pSenioridade, @pLogradouro, @pBairro, @pEstado, @pCep, @pNumero, @pComplemento)";

                        comando.Parameters.AddWithValue("@pNome", pObjAdvogado.Nome);
                        comando.Parameters.AddWithValue("@pSenioridade", (int)pObjAdvogado.Senioridade);
                        comando.Parameters.AddWithValue("@pLogradouro", pObjAdvogado.Endereco.Logradouro);
                        comando.Parameters.AddWithValue("@pBairro", pObjAdvogado.Endereco.Bairro);
                        comando.Parameters.AddWithValue("@pEstado", (int)pObjAdvogado.Endereco.Estado);
                        comando.Parameters.AddWithValue("@pCep", pObjAdvogado.Endereco.Cep);
                        comando.Parameters.AddWithValue("@pNumero", pObjAdvogado.Endereco.Numero);
                        comando.Parameters.AddWithValue("@pComplemento", pObjAdvogado.Endereco.Complemento ?? (object)DBNull.Value);

                        comando.ExecuteNonQuery();

                        comando.Transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        comando.Transaction.Rollback();
                        throw ex;
                    }
                    finally
                    {
                        FecharConexao(comando);
                    }
                }
            }
        }
        public void AtualizarAdvogado(Advogado pObjAdvogado)
        {
            using (var conexao = AbrirConexao())
            {
                using (var comando = IniciarComando(conexao, usarTransacao: true))
                {
                    try
                    {
                        comando.CommandText = @"UPDATE Advogados SET 
                                        Nome = @pNome, 
                                        Senioridade = @pSenioridade, 
                                        Logradouro = @pLogradouro, 
                                        Bairro = @pBairro, 
                                        Estado = @pEstado, 
                                        Cep = @pCep, 
                                        Numero = @pNumero, 
                                        Complemento = @pComplemento 
                                        WHERE Id = @pId";

                        comando.Parameters.AddWithValue("@pId", pObjAdvogado.Id);
                        comando.Parameters.AddWithValue("@pNome", pObjAdvogado.Nome);
                        comando.Parameters.AddWithValue("@pSenioridade", (int)pObjAdvogado.Senioridade);
                        comando.Parameters.AddWithValue("@pLogradouro", pObjAdvogado.Endereco.Logradouro);
                        comando.Parameters.AddWithValue("@pBairro", pObjAdvogado.Endereco.Bairro);
                        comando.Parameters.AddWithValue("@pEstado", (int)pObjAdvogado.Endereco.Estado);
                        comando.Parameters.AddWithValue("@pCep", pObjAdvogado.Endereco.Cep);
                        comando.Parameters.AddWithValue("@pNumero", pObjAdvogado.Endereco.Numero);
                        comando.Parameters.AddWithValue("@pComplemento", pObjAdvogado.Endereco.Complemento ?? (object)DBNull.Value);

                        comando.ExecuteNonQuery();
                        comando.Transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        comando.Transaction.Rollback();
                        throw ex;
                    }
                    finally
                    {
                        FecharConexao(comando);
                    }
                }
            }
        }
        public void ExcluirAdvogado(int pIntId)
        {
            using (var conexao = AbrirConexao())
            {
                using (var comando = IniciarComando(conexao, usarTransacao: true))
                {
                    try
                    {
                        comando.CommandText = "DELETE FROM Advogados WHERE Id = @pIntId";
                        comando.Parameters.AddWithValue("@pIntId", pIntId);
                        comando.ExecuteNonQuery();
                        comando.Transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        comando.Transaction.Rollback();
                        throw e;
                    }
                }
            }
        }
        public Advogado ObterAdvogado(int pIntId)
        {
            Advogado advogado = null;

            using (var conexao = AbrirConexao())
            {
                using (var comando = IniciarComando(conexao, usarTransacao: false))
                {
                    comando.CommandText = "SELECT * FROM Advogados WHERE Id = @pIntId";
                    comando.Parameters.AddWithValue("@pIntId", pIntId);

                    using (var leitor = comando.ExecuteReader())
                    {
                        if (leitor.Read())
                        {
                            advogado = new Advogado
                            {
                                Id = Convert.ToInt32(leitor["Id"]),
                                Nome = leitor["Nome"].ToString(),
                                Senioridade = (Senioridade)Convert.ToInt32(leitor["Senioridade"]),
                                Endereco = new Endereco
                                {
                                    Logradouro = leitor["Logradouro"].ToString(),
                                    Bairro = leitor["Bairro"].ToString(),
                                    Estado = (Estado)Convert.ToInt32(leitor["Estado"]),
                                    Cep = leitor["Cep"].ToString(),
                                    Numero = leitor["Numero"].ToString(),
                                    Complemento = leitor["Complemento"] != DBNull.Value ? leitor["Complemento"].ToString() : null
                                }
                            };
                        }
                    }
                }
            }
            return advogado;
        }

        public IEnumerable<Advogado> ListarAdvogados(string pStrBusca = null)
        {
            var listaAdvogados = new List<Advogado>();

            using (var conexao = AbrirConexao())
            {
                using (var comando = IniciarComando(conexao, usarTransacao: false))
                {
                    try
                    {
                        string sql = "SELECT * FROM Advogados";

                        if (!string.IsNullOrEmpty(pStrBusca))
                        {
                            sql += " WHERE Id = @busca OR Nome LIKE @buscaLike";

                            comando.Parameters.AddWithValue("@busca", pStrBusca);
                            comando.Parameters.AddWithValue("@buscaLike", "%" + pStrBusca + "%");
                        }

                        sql += " ORDER BY Nome";
                        comando.CommandText = sql;

                        using (var leitor = comando.ExecuteReader())
                        {
                            while (leitor.Read())
                            {
                                var advogado = new Advogado
                                {
                                    Id = Convert.ToInt32(leitor["Id"]),
                                    Nome = leitor["Nome"].ToString(),
                                    Senioridade = (Senioridade)Convert.ToInt32(leitor["Senioridade"]),
                                    Endereco = new Endereco
                                    {
                                        Logradouro = leitor["Logradouro"].ToString(),
                                        Bairro = leitor["Bairro"].ToString(),
                                        Estado = (Estado)Convert.ToInt32(leitor["Estado"]),
                                        Cep = leitor["Cep"].ToString(),
                                        Numero = leitor["Numero"].ToString(),
                                        Complemento = leitor["Complemento"].ToString()
                                    }
                                };
                                listaAdvogados.Add(advogado);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        FecharConexao(comando);
                    }
                }
            }
            return listaAdvogados;
        }

    }
}