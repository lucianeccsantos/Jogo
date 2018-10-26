using JogoGourmet.Classes;
using JogoGourmet.Classes.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JogoGourmet
{
    public partial class Desisto : Form
    {
        string qualPratoPensou = "Qual prato você pensou?";

        #region Construtor
        public Desisto()
        {
            InitializeComponent();
            lblPergunta.Text = qualPratoPensou;
        }
        #endregion

        #region Eventos
        private void btnGravar_Click(object sender, EventArgs e)
        {
            Sessao.RespostaAnteriorRecebeAtual();
            Sessao.RespostaAtual = txtResposta.Text;
            TipoPrato tipoPrato = new TipoPrato() { Nome = txtResposta.Text } ;

            Sessao.LtTipoPratos.Add(tipoPrato);
            txtResposta.Clear();

            Complete complete = new Complete();
            complete.Show();
            this.Hide();
          
        }
        
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void txtResposta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnGravar_Click(sender, e);
        }
        #endregion
    }
}
