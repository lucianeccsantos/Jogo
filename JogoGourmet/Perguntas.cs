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
       
        string acertei = "Acertei denovo!";

        int nivel = 0;
        
        Boolean inicio = true;

        public int indiceDaLista { get; private set; }

        public Perguntas()
        {
            InitializeComponent();
            IniciarPerguntas();
        }

        public void IniciarPerguntas()
        {
            lblPergunta.Text = pratoMassa;
            nivel = 1;
            inicio = true;
        }

        private void btnSim_Click(object sender, EventArgs e)
        {            
            if (inicio && nivel == 2)
            {
                Sessao.RespostaAtual = "lasanha";
                Sessao.LstAlimentos.Add(new Alimento() { Nome = Sessao.RespostaAtual });
                inicio = false;
                    nivel = -1;
                BuscaRespostas(true);
                return;
            }
            if (nivel == 1)
            {
               
                Sessao.RespostaAtual = "bolo de chocolate";
                Sessao.LtTipoPratos.Add(new TipoPrato() { Nome = Sessao.RespostaAtual });
                nivel = -1;
                BuscaRespostas(true);
                return;
            }
            if (nivel == 2)
            {
                nivel++;
                BuscaRespostas(true);
                Finaliza();
            }
            nivel++;
            BuscaRespostas(true);
            
        }

        private void btnNao_Click(object sender, EventArgs e)
        {
            if (inicio && nivel == 1)
            {
                BuscaRespostas(false);
                /*Sessao.RespostaAtual = "bolo de chocolate";
                Sessao.LstAlimentos.Add(new Alimento() { Nome = Sessao.RespostaAtual });*/
                inicio = false;
            }
            else
            {
                nivel++;
                BuscaRespostas(false);
            }
        }
            private void BuscaRespostas(bool valorBotao) {
           
            switch (nivel)
            {
                case -1:
                    Finaliza();
                    break;
                case 1:
                    PerguntaTipoAlimento(valorBotao);
                    break;                
                case 2:
                        PerguntaTipoAlimento(valorBotao);
                    break;
                case 3:
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
               

                lblPergunta.Text = string.Format(oPratoE, tipoPrato.Nome);
                nivel = 1;
            }
            else
            {
                if (Sessao.LtTipoPratos.Count() == 0)
                {
                    Sessao.RespostaAtual = "bolo de chocolate";
                    this.Desisto();
                    return;
                }
                if (nivel == 2)
                {
                    lblPergunta.Text = string.Format(oPratoE, Sessao.LstAdjetivos.First().Nome);
                    nivel--;
                }
                else
                {
                    lblPergunta.Text = string.Format(oPratoE, Sessao.LtTipoPratos.First().Nome);
                    nivel++;
                    
                }
                Sessao.RespostaAtual = Sessao.LtTipoPratos.First().Nome;

                //nivel = 2;

            }
           
        }

        private void PerguntaAdjetivo(bool afirmacao)
        {
            indiceDaLista = Sessao.LtTipoPratos.FindIndex(d => d.lstAdjetivo.Count() > 0);
            if (indiceDaLista > 0)
            {

                    TipoPrato tipo = Sessao.LtTipoPratos.Find(d => d.Nome.Equals(Sessao.RespostaAtual));
                   // TipoPrato.ValidarTipoPratoAdjetivo(tipo,)
            }
            else
            {
                if (afirmacao)
                    this.Finaliza();
                else
                    this.Desisto();
                
            }

            
            
              
        }

        private void Desisto()
        {
            Sessao.RespostaAnterior = Sessao.RespostaAtual;
            Sessao.RespostaAtual = string.Empty;
            Desisto desistir = new Desisto();
            desistir.Show();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            JogoGourmet jogo = new JogoGourmet();
            jogo.Show();
        }
       
    }
}
