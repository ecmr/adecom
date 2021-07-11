using AdECon.Model;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace AdECon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();



        }

        private void cnsultarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void salvarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Morador morador = new Morador();
            morador.NomeDestinatario = txtNomeMoraador.Text;
            morador.Bloco = txtBloco.Text;
            morador.Apartamento = txtApartamento.Text;
            morador.NumeroCelular = txtCelular.Text;
            morador.email = txtEmail.Text;
            morador.CodigoBarraEtiqueta = txtCodBarras.Text;
            morador.CodigoBarraEtiquetaLocal = txtEtiquetaLocal.Text;
            //morador.LocalPrateleira = int.Parse(txtPrateleira.Text);

            EnvioMensagem.EnvioSms(morador);
            //EnvioMensagem.EnvioSmsTeste(morador);
            //EnvioMensagem.EnvioZap(morador);
            lblMsgMorador.Text = "Mensagem enviada com sucesso!";
            lblMsgMorador.Visible = true;


            //EncomendaBus bus = new EncomendaBus();
            //if (bus.Adicionar(morador))
            //{
            //    //TODO: ENVAIR NOTIFICACAO MORADOR
            //}
        }

        private void carregarExcelToolStripMenuItem_Click(object sender, EventArgs e)
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
                    string registro = string.Concat("Bloco: ", linha[0].ToString().PadLeft(2, '0'), " – Apto: ", linha[1].ToString().PadLeft(3, '0'), " – Numero /Nome: ", linha[2].ToString());
                    
                    listView1.Items.Add(registro);
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
    }
}
