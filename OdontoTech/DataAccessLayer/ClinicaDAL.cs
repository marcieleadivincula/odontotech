﻿using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using Domain;

namespace DataAccessLayer
{
    public class ClinicaDAL
    {

        SqlConnection conn = new SqlConnection(DBConfig.CONNECTION_STRING);
        SqlCommand cmd = new SqlCommand();
        /// <summary>
        /// Insere a Clinica no BD. Caso houver erro a função informa.
        /// </summary>
        /// <param name="clinica"></param>
        public string Insert(Clinica clinica)
        {
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO clinica (nomeClinica, dtInauguracao, idEndereco, idEstoque) values (@nomeClinica, @dtInauguracao, @idEndereco, @idEstoque)";
            cmd.Parameters.AddWithValue("@nomeClinica", clinica.Nome);
            cmd.Parameters.AddWithValue("@dtInauguracao", clinica.DataInauguracao);
            cmd.Parameters.AddWithValue("@idEndereco", clinica.Endereco.Id);
            cmd.Parameters.AddWithValue("@idEstoque", clinica.Estoque.Id);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                return "Clínica cadastrada com sucesso!";
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Duplicate"))
                {
                    return "Clínica já cadastrada.";
                }
                else
                {
                    Console.WriteLine(ex);
                    return "Erro no Banco de dados. Contate o administrador.";
                }
            }
            finally
            {
                conn.Dispose();
            }
        }
        /// <summary>
        /// Tenta deletar, caso der certo retorna (Clinica deletads com êxito!) se não (Erro no Banco de dados. Contate o administrador.)
        /// </summary>
        /// <param name="clinica"></param>
        /// <returns></returns>
        public string Delete(Clinica clinica)
        {
            if (clinica.Id == 0)
            {
                return "Clínica informada inválida!";
            }

            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM clinica WHERE idClinica = @idClinica";
            cmd.Parameters.AddWithValue("@idClinica", clinica.Id);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                return "Clínica deletada com êxito!";
            }
            catch (Exception)
            {
                return "Erro no Banco de dados.Contate o administrador.";
            }
            finally
            {
                conn.Dispose();
            }
        }
        /// <summary>
        /// Tenta atualizar, caso der certo retorna (Clinica atualizada com êxito!) se não (Erro no Banco de dados. Contate o administrador.)
        /// </summary>
        /// <param name="clinica"></param>
        /// <returns></returns>
        public string Update(Clinica clinica)
        {
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE clinica SET nomeClinica = @nomeClinica, dtInauguracao = @dtInauguracao, idEndereco = @idEndereco, idEstoque = @idEstoque WHERE idClinica = @idClinica";
            cmd.Parameters.AddWithValue("@nomeClinica", clinica.Nome);
            cmd.Parameters.AddWithValue("@dtInauguracao", clinica.DataInauguracao);
            cmd.Parameters.AddWithValue("@idEndereco", clinica.Endereco.Id);
            cmd.Parameters.AddWithValue("@idEstoque", clinica.Estoque.Id);
            cmd.Parameters.AddWithValue("@idClinica", clinica.Id);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                return "Clínica atualizada com êxito!";
            }
            catch (Exception)
            {
                return "Erro no Banco de dados.Contate o administrador.";
            }
            finally
            {
                conn.Dispose();
            }
        }
        /// <summary>
        /// Retorna Lista com todas as clinicas
        /// </summary>
        /// <returns></returns>
        public List<Clinica> GetAll()
        {
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM clinica";
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<Clinica> clinicas = new List<Clinica>();

                while (reader.Read())
                {
                    Clinica temp = new Clinica();
                    temp.Endereco = new Endereco();
                    temp.Estoque = new Estoque();

                    temp.Id = Convert.ToInt32(reader["idClinica"]);
                    temp.Nome = Convert.ToString(reader["nomeClinica"]);
                    temp.DataInauguracao = Convert.ToDateTime(reader["dtInauguracao"]);
                    temp.Endereco.Id = Convert.ToInt32(reader["idEndereco"]);
                    temp.Estoque.Id = Convert.ToInt32(reader["idEstoque"]);

                    clinicas.Add(temp);
                }

                return clinicas;
            }
            catch (Exception)
            {
                throw new Exception("Erro no Banco de dados.Contate o administrador.");
            }
            finally
            {
                conn.Dispose();
            }
        }
        public Clinica GetById(int idClinica)
        {
            cmd.Connection = conn;
            cmd.CommandText = $"SELECT * FROM clinica WHERE idClinica = {idClinica}";

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Clinica clinica = new Clinica();

                while (reader.Read())
                {
                    clinica.Endereco = new Endereco();
                    clinica.Estoque = new Estoque();

                    clinica.Id = Convert.ToInt32(reader["idClinica"]);
                    clinica.Nome = Convert.ToString(reader["nomeClinica"]);
                    clinica.DataInauguracao = Convert.ToDateTime(reader["dtInauguracao"]);
                    clinica.Endereco.Id = Convert.ToInt32(reader["idEndereco"]);
                    clinica.Estoque.Id = Convert.ToInt32(reader["idEstoque"]);
                }

                return clinica;
            }
            catch (Exception)
            {
                throw new Exception("Erro no Banco de dados.Contate o administrador.");
            }
            finally
            {
                conn.Dispose();
            }
        }
        public Clinica GetLastRegister()
        {
            SqlConnection conn = new SqlConnection(DBConfig.CONNECTION_STRING);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "SELECT * FROM clinica ORDER BY idClinica DESC limit 1";

            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                Clinica clinica = new Clinica();

                while (reader.Read())
                {
                    clinica.Endereco = new Endereco();
                    clinica.Estoque = new Estoque();

                    clinica.Id = Convert.ToInt32(reader["idClinica"]);
                    clinica.Nome = Convert.ToString(reader["nomeClinica"]);
                    clinica.DataInauguracao = Convert.ToDateTime(reader["dtInauguracao"]);
                    clinica.Endereco.Id = Convert.ToInt32(reader["idEndereco"]);
                    clinica.Estoque.Id = Convert.ToInt32(reader["idEstoque"]);
                }

                return clinica;
            }
            catch (Exception)
            {
                throw new Exception("Erro no Banco de dados.Contate o administrador.");
            }
            finally
            {
                conn.Dispose();
            }
        }

        public List<Clinica> GetByEndereco(Endereco endereco)
        {
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM clinica WHERE idEndereco = @idEndereco";
            cmd.Parameters.AddWithValue("@idEndereco", endereco.Id);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<Clinica> clinicas = new List<Clinica>();

                while (reader.Read())
                {
                    Clinica temp = new Clinica();

                    temp.Endereco = new Endereco();
                    temp.Estoque = new Estoque();

                    temp.Id = Convert.ToInt32(reader["idClinica"]);
                    temp.Nome = Convert.ToString(reader["nomeClinica"]);
                    temp.DataInauguracao = Convert.ToDateTime(reader["dtInauguracao"]);
                    temp.Endereco.Id = Convert.ToInt32(reader["idEndereco"]);
                    temp.Estoque.Id = Convert.ToInt32(reader["idEstoque"]);

                    clinicas.Add(temp);
                }

                return clinicas;
            }
            catch (Exception)
            {
                throw new Exception("Erro no Banco de dados.Contate o administrador.");
            }
            finally
            {
                conn.Dispose();
            }
        }
    }
}
