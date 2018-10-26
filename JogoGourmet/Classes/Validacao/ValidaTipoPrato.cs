using JogoGourmet.Classes.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoGourmet.Classes.Validacao
{
    public static class ValidaTipoPrato
    {
      
        public static bool ValidarTipoPrato(TipoPrato tipoPrato)
        {
            return Sessao.LtTipoPratos.IndexOf(tipoPrato) > 0;
        }

        public static bool ValidaSeTemAdjetivosPrato()
        {
            return Sessao.LtTipoPratos.Where(d => d.lstAdjetivo.Count() > 0)
                                     .ToList()
                                     .Count() > 0;
        }

    }
}
