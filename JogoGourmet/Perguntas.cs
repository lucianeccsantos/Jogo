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
        bool inicio = true;


        public int indiceDaLista { get; private set; }

        public Perguntas()
        {
            InitializeComponent();
            inicio = SetarVariavelInicio();
            IniciarPerguntas();
        }

        public bool SetarVariavelInicio()
        {
            return (Sessao.LstAdjetivos.Count == 0 &&
                      Sessao.LstAlimentos.Count == 0 &&
                      Sessao.LtTipoPratos.Count == 0 &&
                      string.IsNullOrEmpty(Sessao.RespostaAnterior)
                      && string.IsNullOrEmpty(Sessao.RespostaAtual));
        }

        public void IniciarPerguntas()
        {
            lblPergunta.Text = pratoMassa;
            nivel = 1;
            inicio = true;
        }

        private void btnSim_Click(object sender, EventArgs e)
        {
            if (inicio && nivel == 1)
            {
                BuscaRespostas(true);
                return;
            }
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
                Sessao.RespostaAnterior = Sessao.RespostaAtual;
                nivel = 4;
                BuscaRespostas(true);
                Finaliza();
                return;
            }
            if (nivel == 4)
            {
                Desisto();
            }
            Finaliza();

        }

        private void btnNao_Click(object sender, EventArgs e)
        {
            if (inicio && (nivel == 1 || nivel == 2))
            {
                BuscaRespostas(false);
                inicio = false;
                return;
            }
            if (nivel == 2)
            {
                Sessao.RespostaAnterior = Sessao.RespostaAtual;
                BuscaRespostas(true);
                nivel = 4;
                return;
                //Finaliza();
            }
            if (nivel == 3)
            {
                Sessao.RespostaAtual = "bolo de chocolate";
                Sessao.LstAlimentos.Add(new Alimento() { Nome = Sessao.RespostaAtual });
                BuscaRespostas(false);
                inicio = false;
                return;
            }
            if (nivel == 4)
            {              
                BuscaRespostas(false);
                return;
            }
            if (nivel == 4 || nivel == 0)
            {
                Sessao.RespostaAnterior = Sessao.RespostaAtual;
                BuscaRespostas(false);
                return;
            }
            else
            {
                nivel++;
                BuscaRespostas(false);
            }
        }


        private void BuscaRespostas(bool valorBotao)
        {

            switch (nivel)
            {
                case -1:
                    Finaliza();
                    break;
                case 0:
                    Desisto();
                    break;
                case 1:
                case 2:
                    PerguntaTipoAlimento(valorBotao);
                    break;
                case 3:
                    PerguntaAdjetivo(valorBotao);
                    break;
                case 4:
                    BuscarTipoPratoPorAdjetivo();
                    break;
                default:
                    JogoGourmet jogo = new JogoGourmet();
                    jogo.Show();
                    break;
            }

        }
        

        private void BuscarTipoPratoPorAdjetivo()
        {
            string tipoPrato = TipoPrato.BuscarTipoPratoPorAdjetivo(Sessao.RespostaAnterior);
            
            if (string.IsNullOrEmpty(tipoPrato))
            {
                string adj = TipoPrato.BuscarAdjetivoPorTipoPrato(Sessao.RespostaAnterior);
                lblPergunta.Text = string.Format(oPratoE, adj);
                Sessao.RespostaAnterior = Sessao.RespostaAtual;
                nivel = 0;
            }
            else
            {
                lblPergunta.Text = string.Format(oPratoE, tipoPrato);
                Sessao.RespostaAnterior = Sessao.RespostaAtual;
                nivel = 4;
            }

        }

        private void PerguntaTipoAlimento(bool afirmacao)
        {
            Classes.TipoPrato tipoPrato = new Classes.TipoPrato();

            if (!afirmacao && TipoPrato.ValidaSeTemAdjetivosPrato())
            {
                string adjetivo = TipoPrato.BuscarAdjetivoPratoAleatorio(nivel);

                lblPergunta.Text = string.Format(oPratoE, adjetivo);
                Sessao.RespostaAtual = adjetivo;
                nivel = 2;
            }
            else if (inicio)
            {
                tipoPrato.Nome = afirmacao ? "lasanha" : "bolo de chocolate";

                lblPergunta.Text = string.Format(oPratoE, tipoPrato.Nome);
                nivel = (afirmacao) ? 2 : 3;
            }
            else
            {
                if (Sessao.LstAdjetivos.Count() == 0)
                {
                    Sessao.RespostaAtual = "bolo de chocolate";
                    this.Desisto();
                    return;
                }
                if (nivel == 2)
                {
                    if (Sessao.LstAdjetivos.Count() > 1)
                    {
                        int hash = Sessao.LstAdjetivos.FindIndex(d => d.Nome.Equals(Sessao.RespostaAnterior)).GetHashCode();
                        int index = Sessao.LstAdjetivos.Count() > hash ? hash-- : hash++;
                        string pergunta = Sessao.LstAdjetivos.ElementAtOrDefault(index).Nome;
                        lblPergunta.Text = string.Format(oPratoE, pergunta);
                    }
                    else
                    {
                        Desisto();
                    }

                    nivel--;
                }
                else
                {
                    lblPergunta.Text = string.Format(oPratoE, Sessao.LtTipoPratos.First().Nome);
                    nivel++;

                }
                Sessao.RespostaAnterior = Sessao.RespostaAtual;
                
            }

        }

        private void PerguntaAdjetivo(bool afirmacao)
        {
            indiceDaLista = Sessao.LtTipoPratos.FindIndex(d => d.lstAdjetivo.Count() > 0);
            if (indiceDaLista > 0)
            {
                TipoPrato tipo = Sessao.LtTipoPratos.Find(d => d.Nome.Equals(Sessao.RespostaAtual));
               
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
            Desisto desistir = new Desisto();
            desistir.Show();
        }
        private void Finaliza()
        {
            lblPergunta.Text = acertei;
            btnNao.Visible = false;
            btnSim.Visible = false;
            btnOK.Visible = true;
            nivel = 0;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            JogoGourmet jogo = new JogoGourmet();
            jogo.Show();
        }

    }
}
