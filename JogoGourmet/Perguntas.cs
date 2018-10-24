using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JogoGourmet.Classes;
using JogoGourmet.Classes.Util;

namespace JogoGourmet
{
    public partial class Perguntas : Form
    {
        string pratoMassa = "O prato que você pensou é massa?";
        string oPratoE = "O prato que você pensou é {0}?";
        string qualPratoPensou = "Qual prato você pensou?";
        string adjetivoPrato = "{0} é _____ mas o {1} não.";
        string acertei = "Acertei denovo!";

        int nivel = 0;
        string respostaAtual = string.Empty;
        Boolean inicio = true;

        public Perguntas()
        {
            InitializeComponent();
            IniciarPerguntas();
        }

        public void IniciarPerguntas()
        {
            lblPergunta.Text = pratoMassa;
        }

        private void btnSim_Click(object sender, EventArgs e)
        {           
            BuscaRespostas(true);
            nivel = inicio ? nivel++ : nivel;
        }

        private void btnNao_Click(object sender, EventArgs e)
        {
            BuscaRespostas(false);
        }

        private void BuscaRespostas(bool valorBotao) {
           
            switch (nivel)
            {
                case -1:
                    Finaliza();
                    break;
                case 0:
                    PerguntaTipoAlimento(valorBotao);
                    break;
                case 2:
                    Finaliza();
                    break;
                case 1:
                    if (!valorBotao)
                        PerguntaAdjetivo(valorBotao);
                    break;
                default:
                    JogoGourmet jogo = new JogoGourmet();
                    jogo.Show();
                    break;
            } 
                        
           
        }

        private void Finaliza()
        {
            lblPergunta.Text = acertei;
            btnNao.Visible = false;
            btnSim.Visible = false;
            btnOK.Visible = true;
            nivel = 0;
        }



        private void PerguntaTipoAlimento(bool afirmacao) {
            Classes.TipoPrato tipoPrato = new Classes.TipoPrato();

            if (inicio)
            {
                tipoPrato.Nome = afirmacao ? "lasanha" : "bolo de chocolate";
                inicio = false;

                lblPergunta.Text = string.Format(oPratoE, tipoPrato.Nome);
                Sessao.lstTipoPratos.Add(tipoPrato);
            }
            else
            {
                lblPergunta.Text = string.Format(oPratoE, Sessao.lstTipoPratos.First().Nome);
                respostaAtual = Sessao.lstTipoPratos.First().Nome;


            }
        }

        private void PerguntaAdjetivo(bool afirmacao )
        {
            if (Sessao.lstTipoPratos.FindIndex(d => d.lstAdjetivo.Count() > 0) > 0)
            {

                if (!afirmacao) // clicou em não
                {
                    TipoPrato tipo = Sessao.lstTipoPratos.Find(d => d.Nome.Equals(respostaAtual));
                   // TipoPrato.ValidarTipoPratoAdjetivo(tipo,)
            }
            }
            else
            {
                Desisto desistir = new Desisto();
                desistir.Show();
                
            }

            
            
              
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            JogoGourmet jogo = new JogoGourmet();
            jogo.Show();
        }
       
    }
}
