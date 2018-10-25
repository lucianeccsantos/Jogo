using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JogoGourmet.Classes.Util;

namespace JogoGourmet.Classes
{
    public class TipoPrato
    {
        public string Nome { get; set; }
        public List<Adjetivo> lstAdjetivo { get; set; } = new List<Adjetivo>();
        public Alimento Alimento { get; set; }

        public TipoPrato() { }

        public static bool ValidarTipoPrato(TipoPrato tipoPrato)
        {
            return Sessao.LtTipoPratos.IndexOf(tipoPrato) > 0;
        }

        public static bool ValidarTipoPratoAdjetivo(TipoPrato tipoPrato, Adjetivo adjetivo)
        {
            return Sessao.LtTipoPratos.Where(d => d.Nome != tipoPrato.Nome &&
                                                             d.lstAdjetivo.IndexOf(adjetivo) > 0)
                                        .ToList()
                                        .Count() > 0;

        }

        public static bool ValidaSeTemAdjetivosPrato()
        {
            return Sessao.LtTipoPratos.Where(d => d.lstAdjetivo.Count() > 0)
                                     .ToList()
                                     .Count() > 0;
        }
        public static string BuscarAdjetivoPratoAleatorio(int nivel)
        {
            switch (nivel)
            {
                case 2:
                    return Sessao.LtTipoPratos.Where(d => d.lstAdjetivo != null)
                                   .ElementAtOrDefault(new System.Random().Next() % Sessao.LtTipoPratos.Count())
                                   .lstAdjetivo.LastOrDefault().Nome;
                    
                default:
                    return Sessao.LtTipoPratos.Where(d => d.lstAdjetivo != null)
                                              .ElementAtOrDefault(new System.Random().Next() % Sessao.LtTipoPratos.Count()).Nome;
                    
            }

        }

        public static  string BuscarTipoPratoPorAdjetivo(string adjetivo)
        {
            return Sessao.LtTipoPratos.Where(d => d.lstAdjetivo.Exists(e => e.Nome.Equals(adjetivo))).FirstOrDefault().Nome;
        }

    }

}
