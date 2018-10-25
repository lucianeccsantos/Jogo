using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoGourmet.Classes.Util
{
    public static class Sessao
    {

        public static List<TipoPrato> LtTipoPratos { get; set; } = new List<TipoPrato>();

        public static List<Adjetivo> LstAdjetivos { get; set; } = new List<Adjetivo>();

        public static string RespostaAnterior
        {
            get; set;
        }
        public static string RespostaAtual
        {
            get; set;
        }

        public static void RespostaAnteriorRecebeAtual()
        {
            Sessao.RespostaAnterior = Sessao.RespostaAtual;
        }
    }
}
