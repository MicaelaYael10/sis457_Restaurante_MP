using CadRestaurante;
using ClnRestaurante;
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
    public partial class FrmUsuario : Form
    {
        bool esNuevo = false;
        public FrmUsuario()
        {
            InitializeComponent();
        }

        private void listar()
        {
            var usuarios = UsuarioCln.listarPa(txtParametro.Text.Trim());
            dgvLista.DataSource = usuarios;
            dgvLista.Columns["id"].Visible = false;
            dgvLista.Columns["usuario"].HeaderText = "Usuario";
            dgvLista.Columns["clave"].HeaderText = "Clave";
            dgvLista.Columns["idEmpleado"].HeaderText = "idEmpleado";
            dgvLista.Columns["usuarioRegistro"].HeaderText = "Usuario";
            dgvLista.Columns["fechaRegistro"].HeaderText = "Fecha del Registro";
            btnEditar.Enabled = usuarios.Count > 0;
            btnEliminar.Enabled = usuarios.Count > 0;
            if (usuarios.Count > 0) dgvLista.Rows[0].Cells["usuario"].Selected = true;
        }
        private void cargarEmpleado()
        {
            cbxNombre.DataSource = EmpleadoCln.listar();
            cbxNombre.DisplayMember = "Nombre";
            cbxNombre.ValueMember = "id";
        }
        private void FrmUsuario_Load(object sender, EventArgs e)
        {
            Size = new Size(747, 426);
            listar();
            cargarEmpleado();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            esNuevo = false;
            Size = new Size(747, 571);
            txtUsuario.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            esNuevo = false;
            Size = new Size(747, 571);
            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            var usuarios = UsuarioCln.get(id);
            txtUsuario.Text = usuarios.usuario;
            txtClave.Text = usuarios.clave;

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            listar();
        }

        private void txtParametro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) listar();
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {

            var usuarios = new Usuarios();
            usuarios.usuario = txtUsuario.Text.Trim();
            usuarios.clave = Util.Encrypt(txtClave.Text.Trim());
            usuarios.usuarioRegistro = "LabRestaurante";



            if (esNuevo)
            {
                usuarios.fechaRegistro = DateTime.Now;
                usuarios.estado = 1;
                usuarios.idEmpleado = Convert.ToInt32(cbxNombre.SelectedValue);
                UsuarioCln.insertar(usuarios);
            }
            else
            {
                var index = dgvLista.CurrentCell.RowIndex;
                usuarios.id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
                UsuarioCln.actualizar(usuarios);
            }
            listar();
            btnCancelar.PerformClick();
            MessageBox.Show("Usuario guardado correctamente", "::: Restaurante - Mensaje :::",
                MessageBoxButtons.OK, MessageBoxIcon.Information);


        }
        private void limpiar()
        {
            txtUsuario.Text = string.Empty;
            txtClave.Text = string.Empty;

        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            string usuarios = dgvLista.Rows[index].Cells["usuarios"].Value.ToString();
            DialogResult dialog = MessageBox.Show($"¿Está seguro que desea dar de baja el usuario {usuarios}?",
                "::: Minerva - Mensaje :::", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialog == DialogResult.OK)
            {
                UsuarioCln.eliminar(id, "SIS457");
                listar();
                MessageBox.Show("Producto dado de baja correctamente", "::: Restaurante - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Size = new Size(747, 426);
            limpiar();
        }

        private void btnPrincipal_Click(object sender, EventArgs e)
        {
            new frmPrincipal().ShowDialog();
        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {

        }
    }
}
