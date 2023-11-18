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
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }




        private void button1_Click(object sender, EventArgs e)
        {
            new FrmMenu().ShowDialog();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            new frmCliente().ShowDialog();
        }

        private void btnEmpleado_Click_1(object sender, EventArgs e)
        {
            new FrmEmpleado().ShowDialog();
        }

        private void cb_Click(object sender, EventArgs e)
        {
            new FrmMenu().ShowDialog();
        }

        private void btnClientes_Click_1(object sender, EventArgs e)
        {
            new frmCliente().ShowDialog();
        }

        private void btnEmpleado_Click(object sender, EventArgs e)
        {
            new FrmEmpleado().ShowDialog();
        }

        private void btnFactura_Click(object sender, EventArgs e)
        {

        }
    }
}

