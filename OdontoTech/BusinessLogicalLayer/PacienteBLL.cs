using DataAccessLayer;
using Domain;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace BusinessLogicalLayer
{
    public class PacienteBLL
    {
        PacienteDAL dal = new PacienteDAL();
        Response r = new Response();

        //Incluir um registro
        public Response Insert(Paciente paciente)
        {
            StringBuilder erros = new StringBuilder();
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            if (string.IsNullOrWhiteSpace(paciente.Nome))
            {
                erros.AppendLine("O nome deve ser informado.");
            }

            if (!string.IsNullOrWhiteSpace(paciente.Nome))
            {

                if (paciente.Nome.Length > 60)
                {
                    erros.AppendLine("O nome não pode conter mais que 60 caracteres.");
                }
            }


            if (string.IsNullOrWhiteSpace(paciente.Sobrenome))
            {
                erros.AppendLine("O sobrenome deve ser informado.");
            }

            if (!string.IsNullOrWhiteSpace(paciente.Sobrenome))
            {
                if (paciente.Sobrenome.Length > 60)
                {
                    erros.AppendLine("O sobrenome não pode conter mais que 60 caracteres.");
                }
            } 

            if (string.IsNullOrWhiteSpace(paciente.Rg))
            {
                erros.AppendLine("O rg deve ser informado.");
            }
            if (!string.IsNullOrWhiteSpace(paciente.Rg))
            {
                if (paciente.Rg.Length > 20)
                {
                    erros.AppendLine("O rg não pode conter mais que 20 caracteres.");
                }
            }
            if (string.IsNullOrWhiteSpace(paciente.Cpf))
            {
                erros.AppendLine("O cpf deve ser informado.");

            }
            if (!string.IsNullOrWhiteSpace(paciente.Cpf))
            {
                if (paciente.Cpf.Length > 14)
                {
                    erros.AppendLine("O cpf não pode conter mais que 14 caracteres.");
                }

            }
        
            if (paciente.DataNascimento == null)
            {
                erros.AppendLine("A data de nacimento deve ser informada.");
            }     

            if (string.IsNullOrWhiteSpace(paciente.Observacao))
            {
                erros.AppendLine("Uma observação deve ser informada.");
            }
            if (!string.IsNullOrWhiteSpace(paciente.Observacao))
            {
                if (paciente.Observacao.Length > 250)
                {
                    erros.AppendLine("A observação não pode conter mais que 250 caracteres.");
                }

            }

            if (erros.Length != 0)
            {
                r.Message = erros.ToString();
                return r;
            }

            r = dal.Insert(paciente);
            return r;
        }

        //Obter todos os registros
        public QueryResponse<Paciente> GetAll()
        {
            return dal.GetAll();
        }

        //Atualizar um registro existente
        public Response Update(Paciente paciente)
        {
            StringBuilder erros = new StringBuilder();

            if (string.IsNullOrWhiteSpace(paciente.Nome))
            {
                erros.AppendLine("O nome deve ser informado.");
            }

            if (!string.IsNullOrWhiteSpace(paciente.Nome))
            {

                if (paciente.Nome.Length > 60)
                {
                    erros.AppendLine("O nome não pode conter mais que 60 caracteres.");
                }
            }


            if (string.IsNullOrWhiteSpace(paciente.Sobrenome))
            {
                erros.AppendLine("O sobrenome deve ser informado.");
            }

            if (!string.IsNullOrWhiteSpace(paciente.Sobrenome))
            {
                if (paciente.Sobrenome.Length > 60)
                {
                    erros.AppendLine("O sobrenome não pode conter mais que 60 caracteres.");
                }
            }

            if (string.IsNullOrWhiteSpace(paciente.Rg))
            {
                erros.AppendLine("O rg deve ser informado.");
            }
            if (!string.IsNullOrWhiteSpace(paciente.Rg))
            {
                if (paciente.Rg.Length > 20)
                {
                    erros.AppendLine("O rg não pode conter mais que 20 caracteres.");
                }
            }
            if (string.IsNullOrWhiteSpace(paciente.Cpf))
            {
                erros.AppendLine("O cpf deve ser informado.");

            }
            if (!string.IsNullOrWhiteSpace(paciente.Cpf))
            {
                if (paciente.Cpf.Length > 14)
                {
                    erros.AppendLine("O cpf não pode conter mais que 14 caracteres.");
                }

            }

            if (paciente.DataNascimento == null)
            {
                erros.AppendLine("A data de nacimento deve ser informada.");
            }

            if (string.IsNullOrWhiteSpace(paciente.Observacao))
            {
                erros.AppendLine("Uma observação deve ser informada.");
            }
            if (!string.IsNullOrWhiteSpace(paciente.Observacao))
            {
                if (paciente.Observacao.Length > 250)
                {
                    erros.AppendLine("A observação não pode conter mais que 250 caracteres.");
                }

            }

            //if (paciente.Id == 0)
            //{
            //    return "O ID do paciente deve ser informado.";
            //}

            if (erros.Length != 0)
            {
                r.Message = erros.ToString();
                return r;
            }

            r = dal.Update(paciente);
            return r;
        }

        //Excluir um registro
        public Response Delete(Paciente paciente)
        {
            StringBuilder erros = new StringBuilder();

            if (paciente.Id == 0)
            {
                erros.AppendLine("O ID deve ser informado.");
            }

            if (erros.Length != 0)
            {
                r.Message = erros.ToString();
                return r;
            }

            r = dal.Delete(paciente);
            return r;
        }

        //Obter um registro
        public Paciente GetById(Paciente paciente)
        {
            StringBuilder erros = new StringBuilder();

            if (paciente.Id == 0 || paciente.Id < 0)
            {
                erros.AppendLine("O ID do paciente deve ser informado.");
            }

            return dal.GetById(paciente.Id);
        }

        //Obter último registro
        public Paciente GetLastRegister()
        {
            return dal.GetLastRegister();
        }
    }
}
