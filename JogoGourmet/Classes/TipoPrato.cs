﻿using System;
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

        public TipoPrato(){ }

        public static bool ValidarTipoPrato(TipoPrato tipoPrato)
        {
            return Sessao.LtTipoPratos.IndexOf(tipoPrato) > 0;
        }

        public static bool ValidarTipoPratoAdjetivo(TipoPrato tipoPrato, Adjetivo adjetivo)
        {
            return Sessao.LtTipoPratos.Where(d=> d.Nome != tipoPrato.Nome && 
                                                             d.lstAdjetivo.IndexOf(adjetivo) > 0)
                                        .ToList()
                                        .Count() > 0;

        }
    }
}
