using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoGourmet.Classes.Util
{
    public static class Sessao
    {
        
        public static List<TipoPrato> lstTipoPratos {
            get; set;
           // get { return Sessao.lstTipoPratos; }
           // set { Sessao.lstTipoPratos = value; }
        }

        public static List<Adjetivo> lstAdjetivos {
            get; set;
            // get { return Sessao.lstAdjetivos; }
            // set { Sessao.lstAdjetivos = value; }

        }

        public static List<Alimento> lstAlimentos{
            get; set;
            // get { return Sessao.lstAlimentos; }
            // set { Sessao.lstAlimentos = value; }
        }

    }
}
