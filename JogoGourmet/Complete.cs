using JogoGourmet.Classes;
using JogoGourmet.Classes.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace JogoGourmet
{
    public partial class Complete : Form
    {

        string adjetivoPrato = "{0} é _____ mas o {1} não.";

        #region Construtor
        public Complete()
        {
            InitializeComponent();
            PerguntaAdjetivoPrato();
        }
        #endregion

        #region Metodos
        private void PerguntaAdjetivoPrato()
        {
            string tipoP = TipoPrato.BuscarTipoPratoPorAdjetivo(Sessao.RespostaAnterior);
            lblPergunta.Text = string.Format(adjetivoPrato, Sessao.RespostaAtual, !string.IsNullOrEmpty(tipoP) ? tipoP : Sessao.RespostaAnterior);
            Sessao.RespostaAnteriorRecebeAtual();
        }

        private void GravarResposta()
        {
            Sessao.RespostaAtual = txtResposta.Text;

            List<Adjetivo> lstA = new List<Adjetivo>();

            if (!string.IsNullOrEmpty(Sessao.RespostaAnterior))
            {
                Adjetivo adj = new Adjetivo()
                {
                    Nome = Sessao.RespostaAtual
                };
                Sessao.LtTipoPratos.First(d => d.Nome.Equals(Sessao.RespostaAnterior)).lstAdjetivo.Add(adj);
                Sessao.LstAdjetivos.Add(adj);
            }
            else
            {
                lstA.Add(new Adjetivo()
                {
                    Nome = Sessao.RespostaAnterior
                });

                Sessao.LtTipoPratos.Add(new TipoPrato
                {
                    Nome = Sessao.RespostaAtual,
                    lstAdjetivo = lstA
                });
                Sessao.LstAdjetivos = lstA;

            }

            VoltaAoInicio();
        }

        private void VoltaAoInicio()
        {
            Sessao.RespostaAnterior = string.Empty;
            Sessao.RespostaAtual = string.Empty;
            JogoGourmet jogo = new JogoGourmet();
            jogo.Show();
            this.Hide();
        }
        #endregion

        #region Eventos

        private void btnGravar_Click(object sender, EventArgs e)
        {
            GravarResposta();
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
