using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ProjetoCidades.Models;

namespace ProjetoCidades.Repositorio
{
    public class CidadeRep
    {
        string connectionString = @"Data source=.\SqlExpress;Initial catalog=ProjetoCidades; User Id=sa; Password=senai@123";

        public List<Cidade> Listar()
        {
            List<Cidade> lista = new List<Cidade>();
            //Cria uma conexao com o banco de dados usando a String com o data source
            SqlConnection connection = new SqlConnection(connectionString);
            //Cria um comando sql para consulta passado como parametro a querry e a conexao
            SqlCommand sqlQuerry = new SqlCommand("SELECT * FROM cidades", connection);
            //Abre conexao com o banco de dados
            connection.Open();
            //Armazena no Reader as informações recuperadas da querry informada
            SqlDataReader sqlReader = sqlQuerry.ExecuteReader();
            //Enquanto ouver dados no Reader ele ficara no laço while
            while (sqlReader.Read())
            {
               lista.Add(new Cidade(Convert.ToInt32(sqlReader["Id"]), sqlReader["Nome"].ToString(), sqlReader["Estado"].ToString(), Convert.ToInt32(sqlReader["Habitantes"])));
              
            }
            connection.Close();
            return lista;
        }

        public List<Cidade> ListarCidades(int id)
        {
            List<Cidade> lista = new List<Cidade>();
            //Cria uma conexao com o banco de dados usando a String com o data source
            SqlConnection connection = new SqlConnection(connectionString);
            //Cria um comando sql para consulta passado como parametro a querry e a conexao
            SqlCommand sqlQuerry = new SqlCommand("SELECT * FROM cidades where id=@id", connection);
            //Abre conexao com o banco de dados
            sqlQuerry.Parameters.AddWithValue("@id",id);
            connection.Open();
            //Armazena no Reader as informações recuperadas da querry informada
            SqlDataReader sqlReader = sqlQuerry.ExecuteReader();
            //Enquanto ouver dados no Reader ele ficara no laço while
            while (sqlReader.Read())
            {
               lista.Add(new Cidade(Convert.ToInt32(sqlReader["Id"]), sqlReader["Nome"].ToString(), sqlReader["Estado"].ToString(), Convert.ToInt32(sqlReader["Habitantes"])));
              
            }
            connection.Close();
            return lista;
        }

        public void Cadastar(Cidade cidade)
        {
            try
            {
                //Cria uma conexao com o Banco de dados com a string informado o data source
                SqlConnection connection = new SqlConnection(connectionString);
                //Define a string que ira ser execuatda no sql e passa a conexao tambem como parametro
                SqlCommand sqlQuerry = new SqlCommand("INSERT INTO cidades(Nome, Estado, Habitantes) VALUES('" + cidade.Nome + "','" + cidade.Estado + "'," + cidade.Habitantes + ")", connection);
                connection.Open();
                sqlQuerry.ExecuteNonQuery();
                connection.Close();
            }
            catch (SqlException e)
            {
                throw new Exception("Erro ao cadastar! Mensagem de erro: " + e.Message);
            }
        }

        public void Delete(int Id)
        {
            try 
            { 
                SqlConnection connection=new SqlConnection(connectionString);
                SqlCommand sqlQuerry =new SqlCommand("DELETE FROM cidades WHERE id="+Id,connection);
                connection.Open();
                sqlQuerry.ExecuteNonQuery();
                connection.Close();
            } 
            catch (SqlException e) 
            { 
                throw new Exception("Erro ao Deletar! Descrição do erro: "+e.Message);
            }
        }

        public Cidade BuscarCidade(int Id){
            try
            {
                SqlConnection connection=new SqlConnection(connectionString);
                SqlCommand sqlQuerry=new SqlCommand("SELECT * FROM cidade WHERE id="+Id,connection);
                SqlDataReader sqlDataReader=sqlQuerry.ExecuteReader();
                Cidade c=new Cidade();
                if(sqlDataReader.Read()){
                    c=new Cidade(Convert.ToInt32(sqlDataReader["id"]), sqlDataReader["nome"].ToString(), sqlDataReader["Estado"].ToString(), Convert.ToInt32(sqlDataReader["habitantes"]));
                }
                connection.Close();
                return c;
            }
            catch(SqlException e)
            {
                throw new Exception("Erro ao buscar a cidade! Descrição do Erro: "+e.Message);
            }
        }

        public string AlterarCidade(Cidade cidade){
             SqlConnection connection = new SqlConnection(connectionString);
            string msg;
            try
            {
                
                SqlCommand sqlQuerry=new SqlCommand();
                sqlQuerry.Connection = connection;
                sqlQuerry.CommandText = "UPDATE Cidades SET nome=@n, estado=@e, habitantes=@h WHERE id=@id";
                sqlQuerry.Parameters.AddWithValue("@n", cidade.Nome);
                sqlQuerry.Parameters.AddWithValue("@e", cidade.Estado);
                sqlQuerry.Parameters.AddWithValue("@h", cidade.Habitantes);
                sqlQuerry.Parameters.AddWithValue("@id", cidade.Id);
                connection.Open();
                int r = sqlQuerry.ExecuteNonQuery();

                if(r > 0) 
                    msg = "Atualização Efetuada";
                else
                    msg = "Não foi possível atualizar";
                sqlQuerry.Parameters.Clear();

            }
            catch(SqlException e)
            {
                throw new Exception("Erro ao atualizar a cidade! Descrição do erro: " + e.Message);
            } finally{
                connection.Close();
            }
            return msg;
        }
    }
}