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
    public partial class JogoGourmet : Form
    {
        string pratoQueGosta = "Pense em um prato que gosta";
       

        public JogoGourmet()
        {
            InitializeComponent();
            lblPergunta.Text = pratoQueGosta;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Perguntas perguntas = new Perguntas();
            perguntas.Show();
        }
    }
}
