using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CpRestaurante
{
    public partial class FrmComida : Form
    {
        private int indice;
        public FrmComida()
        {
            InitializeComponent();
            indice = 0;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnCambio_Click(object sender, EventArgs e)
        {
            indice++;

            if (indice > 8)
                indice = 0;

            lblComida.ImageIndex=indice;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
