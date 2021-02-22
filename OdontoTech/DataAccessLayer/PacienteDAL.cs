using System;
using System.Collections.Generic;
using Domain;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class Response
    {
        public string Message { get; set; }
        public bool Sucess { get; set; }
        public Exception Exception { get; set; }
    }

    public class SingleResponse<T> : Response
    {
        public T Item { get; set; }
    }

    public class QueryResponse<T> : Response
    {
        public List<T> Items { get; set; }
    }

    public class PacienteDAL
    {
        SqlConnection conn = new SqlConnection(DBConfig.CONNECTION_STRING);
        SqlCommand cmd = new SqlCommand();
        public Response Insert(Paciente paciente)
        {
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO paciente(nome,sobrenome,rg,cpf,dtNascimento,obs,idEndereco) values(@nome, @sobrenome, @rg, @cpf, @dtNascimento, @obs, @idEndereco)";
            cmd.Parameters.AddWithValue("@nome", paciente.Nome);
            cmd.Parameters.AddWithValue("@sobrenome", paciente.Sobrenome);
            cmd.Parameters.AddWithValue("@rg", paciente.Rg);
            cmd.Parameters.AddWithValue("@cpf", paciente.Cpf);
            cmd.Parameters.AddWithValue("@dtNascimento", paciente.DataNascimento);
            cmd.Parameters.AddWithValue("@obs", paciente.Observacao);
            cmd.Parameters.AddWithValue("@idEndereco", paciente.Endereco.Id);

            Response r = new Response();

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();

                r.Sucess = true;
                r.Message = "Paciente cadastrado com sucesso.";
                return r;
            }
            catch (Exception ex)
            {
                r.Sucess = false;
                r.Exception = ex;

                if (ex.Message.Contains("Duplicate"))
                {
                    r.Message = "Paciente já cadastrado.";
                    return r;
                }
                else
                {
                    r.Message = "Erro no Banco de dados. Contate o administrador.";
                    return r;
                }
            }
            finally
            {
                conn.Dispose();
            }

        }
        public Response Delete(Paciente paciente)
        {
            Response r = new Response();

            if (paciente.Id == 0)
            {
                r.Message = "Paciente informado inválido!";
                return r;
            }

            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM paciente WHERE idPaciente = @idPaciente";
            cmd.Parameters.AddWithValue("@idPaciente", paciente.Id);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();

                r.Sucess = true;
                r.Message = "Paciente deletado com êxito!";
                return r;
            }
            catch (Exception ex)
            {
                r.Sucess = false;
                r.Exception = ex;
                r.Message = "Erro no Banco de dados.Contate o administrador.";
                return r;
            }
            finally
            {
                conn.Dispose();
            }
        }
        public Response Update(Paciente paciente)
        {
            Response r = new Response();

            cmd.Connection = conn;
            cmd.CommandText = "UPDATE paciente SET nome = @nome,  sobrenome = @sobrenome,  rg = @rg,  cpf = @cpf,  dtNascimento = @dtNascimento,  obs = @obs, idEndereco = @idEndereco WHERE idPaciente = @idPaciente";
            cmd.Parameters.AddWithValue("@nome", paciente.Nome);
            cmd.Parameters.AddWithValue("@sobrenome", paciente.Sobrenome);
            cmd.Parameters.AddWithValue("@rg", paciente.Rg);
            cmd.Parameters.AddWithValue("@cpf", paciente.Cpf);
            cmd.Parameters.AddWithValue("@dtNascimento", paciente.DataNascimento);
            cmd.Parameters.AddWithValue("@obs", paciente.Observacao);
            cmd.Parameters.AddWithValue("@idEndereco", paciente.Endereco.Id);
            cmd.Parameters.AddWithValue("@idPaciente", paciente.Id);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                r.Sucess = true;
                r.Message = "Paciente atualizado com êxito!";
                return r;
            }
            catch (Exception ex)
            {
                r.Sucess = false;
                r.Exception = ex;
                r.Message = "Erro no Banco de dados.Contate o administrador.";
                return r;
            }
            finally
            {
                conn.Dispose();
            }
        }
        public QueryResponse<Paciente> GetAll()
        {
            Response r = new Response();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM paciente";

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<Paciente> pacientes = new List<Paciente>();

                QueryResponse<Paciente> qpacientes = new QueryResponse<Paciente>();

                while (reader.Read())
                {
                    Paciente temp = new Paciente();

                    temp.Endereco = new Endereco();
                    temp.Endereco.Logradouro = new Logradouro();
                    temp.Endereco.Logradouro.Bairro = new Bairro();
                    temp.Endereco.Logradouro.Bairro.Cidade = new Cidade();
                    temp.Endereco.Logradouro.Bairro.Cidade.Estado = new Estado();
                    temp.Endereco.Logradouro.Bairro.Cidade.Estado.Pais = new Pais();

                    temp.Id = Convert.ToInt32(reader["idPaciente"]);
                    temp.Nome = Convert.ToString(reader["nomePaciente"]);
                    temp.Sobrenome = Convert.ToString(reader["sobrenome"]);
                    temp.Rg = Convert.ToString(reader["rg"]);
                    temp.Cpf = Convert.ToString(reader["cpf"]);
                    temp.DataNascimento = Convert.ToDateTime(reader["dtNascimento"]);
                    temp.Observacao = Convert.ToString(reader["obs"]);
                    temp.Endereco.Id = Convert.ToInt32(reader["idEndereco"]);

                    pacientes.Add(temp);
                }

                qpacientes.Items = pacientes;
                return qpacientes;
            }
            catch (Exception ex)
            {
                r.Sucess = false;
                r.Exception = ex;
                r.Message = "Erro no Banco de dados. Contate o administrador.";
                return r;
            }
            finally
            {
                conn.Dispose();
            }
        }
        /// <summary>
        /// Retorna Paciente.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Paciente GetById(int idPaciente)
        {
            cmd.Connection = conn;
            cmd.CommandText = $"SELECT * FROM paciente WHERE idPaciente = {idPaciente}";


            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Paciente paciente = new Paciente();

                while (reader.Read())
                {
                    paciente.Endereco = new Endereco();

                    paciente.Id = Convert.ToInt32(reader["idPaciente"]);
                    paciente.Nome = Convert.ToString(reader["nome"]);
                    paciente.Sobrenome = Convert.ToString(reader["sobrenome"]);
                    paciente.Rg = Convert.ToString(reader["rg"]);
                    paciente.Cpf = Convert.ToString(reader["cpf"]);
                    paciente.DataNascimento = Convert.ToDateTime(reader["dtNascimento"]);
                    paciente.Observacao = Convert.ToString(reader["obs"]);
                    paciente.Endereco.Id = Convert.ToInt32(reader["idEndereco"]);
                }

                return paciente;
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
        public Paciente GetLastRegister()
        {
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM paciente ORDER BY idPaciente DESC limit 1";

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Paciente paciente = new Paciente();

                while (reader.Read())
                {
                    paciente.Endereco = new Endereco();


                    paciente.Id = Convert.ToInt32(reader["idPaciente"]);
                    paciente.Nome = Convert.ToString(reader["nome"]);
                    paciente.Sobrenome = Convert.ToString(reader["sobrenome"]);
                    paciente.Rg = Convert.ToString(reader["rg"]);
                    paciente.Cpf = Convert.ToString(reader["cpf"]);
                    paciente.DataNascimento = Convert.ToDateTime(reader["dtNascimento"]);
                    paciente.Observacao = Convert.ToString(reader["obs"]);
                    paciente.Endereco.Id = Convert.ToInt32(reader["idEndereco"]);
                }

                return paciente;
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
