using AdECon.Bus;
using AdECon.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            EnvioMensagem.EnvioSms();
            EnvioMensagem.EnvioZap();
            lblMsgMorador.Text = "Mensagem enviada com sucesso!";
            

            //Morador morador = new Morador();
            //morador.NomeDestinatario = txtNomeMoraador.Text;
            //morador.Bloco = txtBloco.Text;
            //morador.Apartamento = txtApartamento.Text;
            //morador.NumeroCelular = txtCelular.Text;
            //morador.email = txtEmail.Text;
            //morador.CodigoBarraEtiqueta = txtCodBarras.Text;
            //morador.CodigoBarraEtiquetaLocal = txtEtiquetaLocal.Text;
            //morador.LocalPrateleira = int.Parse(txtPrateleira.Text);

            //EncomendaBus bus = new EncomendaBus();
            //if (bus.Adicionar(morador))
            //{
            //    //TODO: ENVAIR NOTIFICACAO MORADOR
            //}
        }
    }
}
