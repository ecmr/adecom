using AdECon.Bus;
using AdECon.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;

namespace AdECon
{
    public partial class Form1 : Form
    {
        List<Morador> moradoresBlocos = new List<Morador>();
        public Form1()
        {
            InitializeComponent();
            this.cboApto.GotFocus += CboApto_GotFocus;
            ckbSms.Checked = true;
            timer1.Enabled = false;

            cboBloco.Items.Add("01");
            cboBloco.Items.Add("02");
            cboBloco.Items.Add("03");
            cboBloco.Items.Add("04");
            cboBloco.Items.Add("05");
            cboBloco.Items.Add("06");
            cboBloco.Items.Add("07");
            cboBloco.Items.Add("08");
            cboBloco.Items.Add("09");
            cboBloco.Items.Add("10");
            cboBloco.Items.Add("11");
            cboBloco.Items.Add("12");
            cboBloco.Items.Add("13");


            cboApto.Items.Add("011");
            cboApto.Items.Add("012");
            cboApto.Items.Add("013");
            cboApto.Items.Add("014");
            cboApto.Items.Add("021");
            cboApto.Items.Add("022");
            cboApto.Items.Add("023");
            cboApto.Items.Add("024");
            cboApto.Items.Add("031");
            cboApto.Items.Add("032");
            cboApto.Items.Add("033");
            cboApto.Items.Add("034");
            cboApto.Items.Add("041");
            cboApto.Items.Add("042");
            cboApto.Items.Add("043");
            cboApto.Items.Add("044");
            cboApto.Items.Add("051");
            cboApto.Items.Add("052");
            cboApto.Items.Add("053");
            cboApto.Items.Add("054");
            cboApto.Items.Add("061");
            cboApto.Items.Add("062");
            cboApto.Items.Add("063");
            cboApto.Items.Add("064");
            cboApto.Items.Add("071");
            cboApto.Items.Add("072");
            cboApto.Items.Add("073");
            cboApto.Items.Add("074");
            cboApto.Items.Add("081");
            cboApto.Items.Add("082");
            cboApto.Items.Add("083");
            cboApto.Items.Add("084");
            cboApto.Items.Add("091");
            cboApto.Items.Add("092");
            cboApto.Items.Add("093");
            cboApto.Items.Add("094");
            cboApto.Items.Add("101");
            cboApto.Items.Add("102");
            cboApto.Items.Add("103");
            cboApto.Items.Add("104");

            CarregarBlocos();
        }
        private void cnsultarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void salvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Salvar();
        }
        private void Salvar()
        {
            Morador morador = new Morador();
            morador.NomeDestinatario = txtNomeMoraador.Text;
            morador.Bloco = cboBloco.Text;
            morador.Apartamento = cboApto.Text;
            morador.NumeroCelular = txtCelular.Text;
            morador.email = txtEmail.Text;
            morador.CodigoBarraEtiqueta = txtCodBarras.Text;
            morador.CodigoBarraEtiquetaLocal = txtEtiquetaLocal.Text;
            int iPrateleira = string.IsNullOrEmpty(txtPrateleira.Text) ? 0 : int.Parse(txtPrateleira.Text);
            morador.LocalPrateleira = iPrateleira;
            morador.DataEnvioMensagem = string.Concat(DateTime.Now.Day.ToString(), "/", DateTime.Now.Month.ToString(), "/", DateTime.Now.Year.ToString(), " ", DateTime.Now.Hour.ToString(), ":", DateTime.Now.Minute.ToString());
            morador.Enviadosms = ckbSms.Checked == true ? "S" : "N";
            morador.EnviadoZap = ckbZap.Checked == true ? "S" : "N";
            morador.EnviadoTelegram = "N";
            morador.EnviadoEmail = ckbMail.Checked == true ? "S" : "N";

            if (cknTodos.Checked)
            {
                EnvioMensagem.EnvioSmsDev(morador);
                EnvioMensagem.EnvioEmail();
                EnvioMensagem.EnvioZap(morador);
                return;
            }
            else if (ckbSms.Checked)
                EnvioMensagem.EnvioSmsDev(morador);
            else if (ckbMail.Checked)
                EnvioMensagem.EnvioEmail();
            else if (ckbZap.Checked)
                EnvioMensagem.EnvioZap(morador);
            
            
            // EnvioMensagem.EnvioSms(morador);
            // EnvioMensagem.EnvioSmsTeste(morador);

            lblMsgMorador.Text = "Mensagem enviada com sucesso!";
            lblMsgMorador.Visible = true;


            EncomendaBus bus = new EncomendaBus();
            if (bus.Adicionar(morador))
            {
                //TODO: ENVAIR NOTIFICACAO MORADOR
            }
        }
        private void CarregarBlocos()
        {
            CarregaBloco01();
            CarregaBloco02();
            CarregaBloco03();
            CarregaBloco04();
            CarregaBloco05();
            CarregaBloco06();
            CarregaBloco07();
            CarregaBloco08();
            CarregaBloco09();
            CarregaBloco10();
            CarregaBloco11();
            CarregaBloco12();
            CarregaBloco13();

        }
        private void carregarExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CarregarBlocos();
        }
        private void CarregaBloco01()
        {
            OleDbConnection conexao = new(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=c:\Users\Edinei\Documents\cadastro  moradores 09 11 20 ATUALIZADO.xlsx;Extended Properties ='Excel 12.0 Xml; HDR = YES';");

            OleDbDataAdapter adapter = new("select * from[Bl 1$]", conexao);
            DataSet ds = new();

            try
            {
                conexao.Open();

                adapter.Fill(ds);
                foreach (DataRow linha in ds.Tables[0].Rows)
                {
                    Morador morador = new();
                    morador.Bloco = linha[0].ToString().PadLeft(2, '0');
                    morador.Apartamento = linha[1].ToString().PadLeft(3, '0');
                    morador.NomeDestinatario = linha[2].ToString();
                    morador.NumeroCelular = linha[3].ToString();
                    morador.email = "";

                    string registro = string.Concat("Apto: ", linha[1].ToString().PadLeft(3, '0'));

                    //listView1.Items.Add(registro);
                    moradoresBlocos.Add(morador);
                }

                //var queryNames =
                //  from nome in moradoresBlocos
                //  group nome by nome.Apartamento into newGroup
                //  orderby newGroup.Key
                //  select newGroup;

                //foreach (var apto in queryNames)
                //{
                //  //Console.WriteLine($"\t{student.LastName}, {student.FirstName}");
                //    listView1.Items.Add(apto.Key);
                //}


            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao acessar os dados: " + ex.Message);
            }
            finally
            {
                conexao.Close();
                listView1.Refresh();
            }
        }
        private void CarregaBloco02()
        {
            OleDbConnection conexao = new(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=c:\Users\Edinei\Documents\cadastro  moradores 09 11 20 ATUALIZADO.xlsx;Extended Properties ='Excel 12.0 Xml; HDR = YES';");

            OleDbDataAdapter adapter = new("select * from[Bl 2$]", conexao);
            DataSet ds = new();

            try
            {
                conexao.Open();

                adapter.Fill(ds);
                foreach (DataRow linha in ds.Tables[0].Rows)
                {
                    Morador morador = new();
                    morador.Bloco = linha[0].ToString().PadLeft(2, '0');
                    morador.Apartamento = linha[1].ToString().PadLeft(3, '0');
                    morador.NomeDestinatario = linha[2].ToString();
                    morador.NumeroCelular = linha[3].ToString();
                    morador.email = "";

                    string registro = string.Concat("Apto: ", linha[1].ToString().PadLeft(3, '0'));

                    //listView1.Items.Add(registro);
                    moradoresBlocos.Add(morador);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao acessar os dados: " + ex.Message);
            }
            finally
            {
                conexao.Close();
                listView1.Refresh();
            }
        }
        private void CarregaBloco03()
        {
            OleDbConnection conexao = new(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=c:\Users\Edinei\Documents\cadastro  moradores 09 11 20 ATUALIZADO.xlsx;Extended Properties ='Excel 12.0 Xml; HDR = YES';");

            OleDbDataAdapter adapter = new("select * from[Bl 3$]", conexao);
            DataSet ds = new();

            try
            {
                conexao.Open();

                adapter.Fill(ds);
                foreach (DataRow linha in ds.Tables[0].Rows)
                {
                    Morador morador = new();
                    morador.Bloco = linha[0].ToString().PadLeft(2, '0');
                    morador.Apartamento = linha[1].ToString().PadLeft(3, '0');
                    morador.NomeDestinatario = linha[2].ToString();
                    morador.NumeroCelular = linha[3].ToString();
                    morador.email = "";

                    string registro = string.Concat("Apto: ", linha[1].ToString().PadLeft(3, '0'));

                    //listView1.Items.Add(registro);
                    moradoresBlocos.Add(morador);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao acessar os dados: " + ex.Message);
            }
            finally
            {
                conexao.Close();
                listView1.Refresh();
            }
        }
        private void CarregaBloco04()
        {
            OleDbConnection conexao = new(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=c:\Users\Edinei\Documents\cadastro  moradores 09 11 20 ATUALIZADO.xlsx;Extended Properties ='Excel 12.0 Xml; HDR = YES';");

            OleDbDataAdapter adapter = new("select * from[Bl 4$]", conexao);
            DataSet ds = new();

            try
            {
                conexao.Open();

                adapter.Fill(ds);
                foreach (DataRow linha in ds.Tables[0].Rows)
                {
                    Morador morador = new();
                    morador.Bloco = linha[0].ToString().PadLeft(2, '0');
                    morador.Apartamento = linha[1].ToString().PadLeft(3, '0');
                    morador.NomeDestinatario = linha[2].ToString();
                    morador.NumeroCelular = linha[3].ToString();
                    morador.email = "";

                    string registro = string.Concat("Apto: ", linha[1].ToString().PadLeft(3, '0'));

                    //listView1.Items.Add(registro);
                    moradoresBlocos.Add(morador);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao acessar os dados: " + ex.Message);
            }
            finally
            {
                conexao.Close();
                listView1.Refresh();
            }
        }
        private void CarregaBloco05()
        {
            OleDbConnection conexao = new(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=c:\Users\Edinei\Documents\cadastro  moradores 09 11 20 ATUALIZADO.xlsx;Extended Properties ='Excel 12.0 Xml; HDR = YES';");

            OleDbDataAdapter adapter = new("select * from[Bl 5$]", conexao);
            DataSet ds = new();

            try
            {
                conexao.Open();

                adapter.Fill(ds);
                foreach (DataRow linha in ds.Tables[0].Rows)
                {
                    Morador morador = new();
                    morador.Bloco = linha[0].ToString().PadLeft(2, '0');
                    morador.Apartamento = linha[1].ToString().PadLeft(3, '0');
                    morador.NomeDestinatario = linha[2].ToString();
                    morador.NumeroCelular = linha[3].ToString();
                    morador.email = "";

                    string registro = string.Concat("Apto: ", linha[1].ToString().PadLeft(3, '0'));

                    //listView1.Items.Add(registro);
                    moradoresBlocos.Add(morador);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao acessar os dados: " + ex.Message);
            }
            finally
            {
                conexao.Close();
                listView1.Refresh();
            }
        }
        private void CarregaBloco06()
        {
            OleDbConnection conexao = new(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=c:\Users\Edinei\Documents\cadastro  moradores 09 11 20 ATUALIZADO.xlsx;Extended Properties ='Excel 12.0 Xml; HDR = YES';");

            OleDbDataAdapter adapter = new("select * from[Bl 6$]", conexao);
            DataSet ds = new();

            try
            {
                conexao.Open();

                adapter.Fill(ds);
                foreach (DataRow linha in ds.Tables[0].Rows)
                {
                    Morador morador = new();
                    morador.Bloco = linha[0].ToString().PadLeft(2, '0');
                    morador.Apartamento = linha[1].ToString().PadLeft(3, '0');
                    morador.NomeDestinatario = linha[2].ToString();
                    morador.NumeroCelular = linha[3].ToString();
                    morador.email = "";

                    string registro = string.Concat("Apto: ", linha[1].ToString().PadLeft(3, '0'));

                    //listView1.Items.Add(registro);
                    moradoresBlocos.Add(morador);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao acessar os dados: " + ex.Message);
            }
            finally
            {
                conexao.Close();
                listView1.Refresh();
            }
        }
        private void CarregaBloco07()
        {
            OleDbConnection conexao = new(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=c:\Users\Edinei\Documents\cadastro  moradores 09 11 20 ATUALIZADO.xlsx;Extended Properties ='Excel 12.0 Xml; HDR = YES';");

            OleDbDataAdapter adapter = new("select * from[Bl 7$]", conexao);
            DataSet ds = new();

            try
            {
                conexao.Open();

                adapter.Fill(ds);
                foreach (DataRow linha in ds.Tables[0].Rows)
                {
                    Morador morador = new();
                    morador.Bloco = linha[0].ToString().PadLeft(2, '0');
                    morador.Apartamento = linha[1].ToString().PadLeft(3, '0');
                    morador.NomeDestinatario = linha[2].ToString();
                    morador.NumeroCelular = linha[3].ToString();
                    morador.email = "";

                    string registro = string.Concat("Apto: ", linha[1].ToString().PadLeft(3, '0'));

                    //listView1.Items.Add(registro);
                    moradoresBlocos.Add(morador);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao acessar os dados: " + ex.Message);
            }
            finally
            {
                conexao.Close();
                listView1.Refresh();
            }
        }
        private void CarregaBloco08()
        {
            OleDbConnection conexao = new(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=c:\Users\Edinei\Documents\cadastro  moradores 09 11 20 ATUALIZADO.xlsx;Extended Properties ='Excel 12.0 Xml; HDR = YES';");

            OleDbDataAdapter adapter = new("select * from[Bl 8$]", conexao);
            DataSet ds = new();

            try
            {
                conexao.Open();

                adapter.Fill(ds);
                foreach (DataRow linha in ds.Tables[0].Rows)
                {
                    Morador morador = new();
                    morador.Bloco = linha[0].ToString().PadLeft(2, '0');
                    morador.Apartamento = linha[1].ToString().PadLeft(3, '0');
                    morador.NomeDestinatario = linha[2].ToString();
                    morador.NumeroCelular = linha[3].ToString();
                    morador.email = "";

                    string registro = string.Concat("Apto: ", linha[1].ToString().PadLeft(3, '0'));

                    //listView1.Items.Add(registro);
                    moradoresBlocos.Add(morador);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao acessar os dados: " + ex.Message);
            }
            finally
            {
                conexao.Close();
                listView1.Refresh();
            }
        }
        private void CarregaBloco09()
        {
            OleDbConnection conexao = new(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=c:\Users\Edinei\Documents\cadastro  moradores 09 11 20 ATUALIZADO.xlsx;Extended Properties ='Excel 12.0 Xml; HDR = YES';");

            OleDbDataAdapter adapter = new("select * from[Bl 9$]", conexao);
            DataSet ds = new();

            try
            {
                conexao.Open();

                adapter.Fill(ds);
                foreach (DataRow linha in ds.Tables[0].Rows)
                {
                    Morador morador = new();
                    morador.Bloco = linha[0].ToString().PadLeft(2, '0');
                    morador.Apartamento = linha[1].ToString().PadLeft(3, '0');
                    morador.NomeDestinatario = linha[2].ToString();
                    morador.NumeroCelular = linha[3].ToString();
                    morador.email = "";

                    string registro = string.Concat("Apto: ", linha[1].ToString().PadLeft(3, '0'));

                    //listView1.Items.Add(registro);
                    moradoresBlocos.Add(morador);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao acessar os dados: " + ex.Message);
            }
            finally
            {
                conexao.Close();
                listView1.Refresh();
            }
        }
        private void CarregaBloco10()
        {
            OleDbConnection conexao = new(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=c:\Users\Edinei\Documents\cadastro  moradores 09 11 20 ATUALIZADO.xlsx;Extended Properties ='Excel 12.0 Xml; HDR = YES';");

            OleDbDataAdapter adapter = new("select * from[Bl 10$]", conexao);
            DataSet ds = new();

            try
            {
                conexao.Open();

                adapter.Fill(ds);
                foreach (DataRow linha in ds.Tables[0].Rows)
                {
                    Morador morador = new();
                    morador.Bloco = linha[0].ToString().PadLeft(2, '0');
                    morador.Apartamento = linha[1].ToString().PadLeft(3, '0');
                    morador.NomeDestinatario = linha[2].ToString();
                    morador.NumeroCelular = linha[3].ToString();
                    morador.email = "";

                    string registro = string.Concat("Apto: ", linha[1].ToString().PadLeft(3, '0'));

                    //listView1.Items.Add(registro);
                    moradoresBlocos.Add(morador);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao acessar os dados: " + ex.Message);
            }
            finally
            {
                conexao.Close();
                listView1.Refresh();
            }
        }
        private void CarregaBloco11()
        {
            OleDbConnection conexao = new(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=c:\Users\Edinei\Documents\cadastro  moradores 09 11 20 ATUALIZADO.xlsx;Extended Properties ='Excel 12.0 Xml; HDR = YES';");

            OleDbDataAdapter adapter = new("select * from[Bl 11$]", conexao);
            DataSet ds = new();

            try
            {
                conexao.Open();

                adapter.Fill(ds);
                foreach (DataRow linha in ds.Tables[0].Rows)
                {
                    Morador morador = new();
                    morador.Bloco = linha[0].ToString().PadLeft(2, '0');
                    morador.Apartamento = linha[1].ToString().PadLeft(3, '0');
                    morador.NomeDestinatario = linha[2].ToString();
                    morador.NumeroCelular = linha[3].ToString();
                    morador.email = "";

                    string registro = string.Concat("Apto: ", linha[1].ToString().PadLeft(3, '0'));

                    //listView1.Items.Add(registro);
                    moradoresBlocos.Add(morador);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao acessar os dados: " + ex.Message);
            }
            finally
            {
                conexao.Close();
                listView1.Refresh();
            }
        }
        private void CarregaBloco12()
        {
            OleDbConnection conexao = new(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=c:\Users\Edinei\Documents\cadastro  moradores 09 11 20 ATUALIZADO.xlsx;Extended Properties ='Excel 12.0 Xml; HDR = YES';");

            OleDbDataAdapter adapter = new("select * from[Bl 12$]", conexao);
            DataSet ds = new();

            try
            {
                conexao.Open();

                adapter.Fill(ds);
                foreach (DataRow linha in ds.Tables[0].Rows)
                {
                    Morador morador = new();
                    morador.Bloco = linha[0].ToString().PadLeft(2, '0');
                    morador.Apartamento = linha[1].ToString().PadLeft(3, '0');
                    morador.NomeDestinatario = linha[2].ToString();
                    morador.NumeroCelular = linha[3].ToString();
                    morador.email = "";

                    string registro = string.Concat("Apto: ", linha[1].ToString().PadLeft(3, '0'));

                    //listView1.Items.Add(registro);
                    moradoresBlocos.Add(morador);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao acessar os dados: " + ex.Message);
            }
            finally
            {
                conexao.Close();
                listView1.Refresh();
            }
        }
        private void CarregaBloco13()
        {
            OleDbConnection conexao = new(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=c:\Users\Edinei\Documents\cadastro  moradores 09 11 20 ATUALIZADO.xlsx;Extended Properties ='Excel 12.0 Xml; HDR = YES';");

            OleDbDataAdapter adapter = new("select * from[Bl 13$]", conexao);
            DataSet ds = new();

            try
            {
                conexao.Open();

                adapter.Fill(ds);
                foreach (DataRow linha in ds.Tables[0].Rows)
                {
                    Morador morador = new();
                    morador.Bloco = linha[0].ToString().PadLeft(2, '0');
                    morador.Apartamento = linha[1].ToString().PadLeft(3, '0');
                    morador.NomeDestinatario = linha[2].ToString();
                    morador.NumeroCelular = linha[3].ToString();
                    morador.email = "";

                    string registro = string.Concat("Apto: ", linha[1].ToString().PadLeft(3, '0'));

                    //listView1.Items.Add(registro);
                    moradoresBlocos.Add(morador);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao acessar os dados: " + ex.Message);
            }
            finally
            {
                conexao.Close();
                listView1.Refresh();
            }
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string apto = listView1.SelectedItems[0].Text;

            List<Morador> queryMoradores = moradoresBlocos.Where(ap => ap.Apartamento.Equals(apto)).ToList();

            foreach (var item in queryMoradores)
            {
                listViewMoradoresApto.Items.Add(item.NomeDestinatario);
            }
        }
        private void cboApto_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Morador morador = (Morador)moradoresBlocos.Where(ap => ap.Bloco.Equals(cboBloco.SelectedItem.ToString()) && ap.Apartamento.Equals(cboApto.SelectedItem.ToString()));
            
            listViewMoradoresApto.Items.Clear();

            for (int i = 0; i < moradoresBlocos.Count; i++)
            {
                int bloco = int.Parse(moradoresBlocos[i].Bloco);
                int cBloco = int.Parse(cboBloco.Text);    
                int apto = int.Parse(moradoresBlocos[i].Apartamento);
                int cApto = int.Parse(cboApto.Text);

                if (bloco.Equals(cBloco) && apto.Equals(cApto))
                {
                    listViewMoradoresApto.Items.Add(moradoresBlocos[i].NomeDestinatario);
                }
            }
        }
        private void cboBlocoKeyPress(object sender, KeyPressEventArgs e)
        {
            if (string.IsNullOrEmpty(e.KeyChar.ToString()))
                return;

            int sBloco = int.Parse(e.KeyChar.ToString());

            if ((sBloco >= 1) && (sBloco <= 13))
            {
                cboApto.Focus();
            }
            cboBloco.Select(cboBloco.Text.Length, 0);
        }
        private void CboApto_GotFocus(Object sender, EventArgs e)
        {
            if ((!string.IsNullOrEmpty(cboBloco.Text) && (cboBloco.Text.Length > 2)))
            {
                cboBloco.Text = cboBloco.Text.Remove(0, 1);
            }
        }
        private void CarregaNomes(object sender, KeyPressEventArgs e)
        {
            if (string.IsNullOrEmpty(e.KeyChar.ToString()) || (string.IsNullOrEmpty(cboApto.Text)))
                return;

            int iApto = int.Parse(cboApto.Text);

            if ((iApto > 10) && (iApto <= 104))
            {
                listViewMoradoresApto.Items.Clear();

                int sBloco = int.Parse(cboBloco.Text);

//                Morador morador = (Morador)moradoresBlocos.Where(ap => ap.Bloco.Equals("01") && ap.Apartamento.Equals("104"));

                for (int i = 0; i < moradoresBlocos.Count; i++)
                {
                    int bloco = int.Parse(moradoresBlocos[i].Bloco);
                    int apto = int.Parse(moradoresBlocos[i].Apartamento);
                    
                    if (bloco.Equals(sBloco) && apto.Equals(iApto))
                    {
                        listViewMoradoresApto.Items.Add(moradoresBlocos[i].NomeDestinatario);
                    }
                    if (bloco.Equals(sBloco) && (apto > iApto))
                        return;
                }
            }
        }
        private void listViewMoradoresApto_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (listViewMoradoresApto.SelectedItems.Count < 1)
                return;

            string nomeMorador = listViewMoradoresApto.SelectedItems[0].Text;
            string bloco = cboBloco.Text;
            string apto = cboApto.Text;

            List<Morador> queryMoradores = moradoresBlocos.Where(ap => ap.Apartamento.Equals(apto) && ap.Bloco.Equals(bloco) && ap.NomeDestinatario.Equals(nomeMorador)).ToList();

            foreach (var item in queryMoradores)
            {
                txtNomeMoraador.Text = item.NomeDestinatario;
                txtCelular.Text = item.NumeroCelular;
                txtEmail.Text = item.email;
            }
        }
        private void btnEnviar_Click(object sender, EventArgs e)
        {
            Salvar();
            timer1.Enabled = true;
        }
        private void LimparMensagem(object sender, EventArgs e)
        {
            lblMsgMorador.Text = "";
            timer1.Enabled = false;
        }
    
        private Morador ConsultarBase()
        {
            Morador morador;




            return morador;
        }
    }
}
