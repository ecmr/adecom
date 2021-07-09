using AdECon.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Text;

namespace AdECon.Dal
{
    public class DalHelper
    {
        private static SQLiteConnection sqliteConnection;

        public DalHelper()
        { }

        private static SQLiteConnection DbConnection()
        {
            string caminho = Directory.GetCurrentDirectory();
            string CaminhoRaiz = string.Concat(caminho.Substring(0, (caminho.Length - 20)), "\\dados\\Cadastro.sqlite");

            sqliteConnection = new SQLiteConnection(string.Concat("Data Source=", CaminhoRaiz, "; Version=3;"));  //"Data Source=c:\\dados\\Cadastro.sqlite; Version=3;");
            sqliteConnection.Open();
            return sqliteConnection;
        }

        public static void CriarBancoSQLite()
        {
            try
            {
                SQLiteConnection.CreateFile(@"c:\dados\Cadastro.sqlite");
            }
            catch
            {
                throw;
            }
        }

        public static void CriarTabelaSQlite()
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS Sedex(IdMorador int, Bloco Varchar(2), Apartamento Varchar(3), NomeDestinatario Varchar(100), Email Varchar(100), NumeroCelular Varchar(11), CodigoBarraEtiqueta Varchar(50), CodigoBarraEtiquetaLocal Varchar(50), LocalPrateleira Varchar(3))";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<Morador> GetClientes()
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Sedex";
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                    return PreencheMoradorDt(dt);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Morador GetCliente(int id)
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();

            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Sedex Where IdMorador=" + id;
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);

                    var morador = PreencheMoradorDt(dt); 
                    return morador[0];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool Add(Morador morador)
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    var query = @"INSERT INTO Sedex(IdMorador, Bloco , Apartamento , NomeDestinatario , Email , NumeroCelular , CodigoBarraEtiqueta , CodigoBarraEtiquetaLocal , LocalPrateleira)
                                    values (@IdMorador, @Bloco , @Apartamento , @NomeDestinatario , @Email , @NumeroCelular , @CodigoBarraEtiqueta , @CodigoBarraEtiquetaLocal , @LocalPrateleira)";

                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@IdMorador", morador.IdMorador);
                    cmd.Parameters.AddWithValue("@Bloco", morador.Bloco);
                    cmd.Parameters.AddWithValue("@Apartamento", morador.Apartamento);
                    cmd.Parameters.AddWithValue("@NomeDestinatario", morador.NomeDestinatario);
                    cmd.Parameters.AddWithValue("@Email", morador.email);
                    cmd.Parameters.AddWithValue("@NumeroCelular", morador.NumeroCelular);
                    cmd.Parameters.AddWithValue("@CodigoBarraEtiqueta", morador.CodigoBarraEtiqueta);
                    cmd.Parameters.AddWithValue("@CodigoBarraEtiquetaLocal", morador.CodigoBarraEtiquetaLocal);
                    cmd.Parameters.AddWithValue("@LocalPrateleira", morador.LocalPrateleira);

                    cmd.ExecuteNonQuery();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool Update(Morador morador)
        {
            //try
            //{
            //    using (var cmd = new SQLiteCommand(DbConnection()))
            //    {
            //        if (cliente.Id != null)
            //        {
            //            cmd.CommandText = "UPDATE Clientes SET Nome=@Nome, Email=@Email WHERE Id=@Id";
            //            cmd.Parameters.AddWithValue("@Id", cliente.Id);
            //            cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
            //            cmd.Parameters.AddWithValue("@Email", cliente.Email);
            //            cmd.ExecuteNonQuery();
            //        }
            //    };
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}
            return true;
        }

        public static bool Delete(int Id)
        {
            try
            {
                using (var cmd = new SQLiteCommand(DbConnection()))
                {
                    cmd.CommandText = "DELETE FROM Sedex Where IdMorador=@Id";
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static List<Morador> PreencheMoradorDt(DataTable data)
        {
            List<Morador> moradores = new List<Morador>();

            foreach (DataRow row in data.Rows)
            {
                Morador morador = new Morador();

                morador.IdMorador = int.Parse(data.Columns["IdMorador"].ToString());
                morador.NomeDestinatario = data.Columns["NomeDestinatario"].ToString();
                morador.Bloco = data.Columns["Bloco"].ToString();
                morador.Apartamento = data.Columns["Apartamento"].ToString();
                morador.email = data.Columns["email"].ToString();
                morador.NumeroCelular = data.Columns["NumeroCelular"].ToString();
                morador.CodigoBarraEtiqueta = data.Columns["CodigoBarraEtiqueta"].ToString();
                morador.CodigoBarraEtiquetaLocal = data.Columns["CodigoBarraEtiquetaLocal"].ToString();
                morador.LocalPrateleira = int.Parse(data.Columns["LocalPrateleira"].ToString());

                moradores.Add(morador);
            }
            return moradores;
        }
    }
}
