﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBLIOTECA.CAMADAS.DAL
{
    public class Livros
    {
        private string strCon = Conexao.getConexao();

        public List<MODEL.Livros> Select()
        {
            List<MODEL.Livros> lstLivros = new List<MODEL.Livros>();
            SqlConnection conexao = new SqlConnection(strCon);
            string sql = "Select * from Livros";
            SqlCommand cmd = new SqlCommand(sql, conexao);
            try
            {
                conexao.Open();
                SqlDataReader dados = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dados.Read())
                {
                    MODEL.Livros livro = new MODEL.Livros();
                    livro.id = Convert.ToInt32(dados[0].ToString()); //dados["id"]
                    livro.titulo = dados["titulo"].ToString();
                    livro.editora = dados["editora"].ToString();
                    livro.autor = dados["autor"].ToString();
                    livro.valor = Convert.ToSingle(dados["valor"].ToString());
                    livro.situacao = Convert.ToInt32(dados["situacao"].ToString());
                    lstLivros.Add(livro);
                }
            }
            catch
            {
                Console.WriteLine("Deu erro na execução do comando select de livros");
            }
            finally
            {
                conexao.Close();
            }
            return lstLivros;
        }

        public void Insert(MODEL.Livros livro)
        {
            SqlConnection conexao = new SqlConnection(strCon);
            string sql = "Insert into Livros values (@titulo, @editora, @autor, @valor, @situacao);";
            SqlCommand cmd = new SqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@titulo", livro.titulo);
            cmd.Parameters.AddWithValue("@editora", livro.editora);
            cmd.Parameters.AddWithValue("@autor", livro.autor);
            cmd.Parameters.AddWithValue("@valor", livro.valor);
            cmd.Parameters.AddWithValue("@situacao", livro.situacao);

            try
            {
                conexao.Open();
                cmd.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("Deu erro inserção de livros...");
            }
            finally
            {
                conexao.Close();
            }
        }

        public void Update(MODEL.Livros livro)
        {
            SqlConnection conexao = new SqlConnection(strCon);
            string sql = "UPDATE Livros SET titulo=@titulo, editora=@editora, autor=@autor, valor=@valor, situacao=@situacao ";
            sql += " WHERE id=@id;";
            SqlCommand cmd = new SqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@id", livro.id);
            cmd.Parameters.AddWithValue("@titulo", livro.titulo);
            cmd.Parameters.AddWithValue("@editora", livro.editora);
            cmd.Parameters.AddWithValue("@autor", livro.autor);
            cmd.Parameters.AddWithValue("@valor", livro.valor);
            cmd.Parameters.AddWithValue("@situacao", livro.situacao);

            try
            {
                conexao.Open();
                cmd.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("Deu erro atualização-update de livros...");
            }
            finally
            {
                conexao.Close();
            }
        }

        public void Delete(int idLivro)
        {
            SqlConnection conexao = new SqlConnection(strCon);
            string sql = "DELETE FROM Livros  WHERE id=@id;";
            SqlCommand cmd = new SqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@id", idLivro);

            try
            {
                conexao.Open();
                cmd.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("Deu erro remoção  de Livros...");
            }
            finally
            {
                conexao.Close();
            }
        }

    }
}
