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
    public partial class frmCliente : Form
    {
        bool esNuevo = false;
        public frmCliente()
        {
            InitializeComponent();
        }

        private void gbxDatos_Enter(object sender, EventArgs e)
        {

        }

        private void frmCliente_Load(object sender, EventArgs e)
        {
            Size = new Size(780, 518);
            listar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            listar();
        }
        private void listar()
        {
            var clientes = ClienteCln.listarPa(txtParametro.Text.Trim());
            dgvLista.DataSource = clientes;
            dgvLista.Columns["id"].Visible = false;
            dgvLista.Columns["nombre"].HeaderText = "Nombre";
            dgvLista.Columns["primerApellido"].HeaderText = "Primer Apellido";
            dgvLista.Columns["SegundoApellido"].HeaderText = "Segundo Apellido";
            dgvLista.Columns["cedulaIdentidad"].HeaderText = "Cedula de Identidad";
            //dgvLista.Columns["usuarioRegistro"].HeaderText = "Usuario";
            btnEditar.Enabled = clientes.Count > 0;
            btnEliminar.Enabled = clientes.Count > 0;
            if (clientes.Count > 0) dgvLista.Rows[0].Cells["Nombre"].Selected = true; ;

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            Size = new Size(780, 699);
            txtNombre.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            esNuevo = false;
            Size = new Size(780, 699);
            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            var cliente = ClienteCln.get(id);
            txtNombre.Text = cliente.nombre;
            txtPrimerApellido.Text = cliente.primerApellido;
            txtSegundoApellido.Text = cliente.segundoApellido;
            txtCedulaIdentidad.Text = cliente.cedulaIdentidad;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Size = new Size(780, 518);
            limpiar();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtParametro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) listar();
        }
        private bool validar()
        {
            bool esValido = true;
            erpNombre.SetError(txtNombre, "");
            erpPrimerApellido.SetError(txtPrimerApellido, "");
            erpSegundoApellido.SetError(txtSegundoApellido, "");
            erpCedulaIdentidad.SetError(txtCedulaIdentidad, "");

            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                esValido = false;
                erpNombre.SetError(txtNombre, "El campo Nombre es obligatorio");
            }
            if (string.IsNullOrEmpty(txtPrimerApellido.Text))
            {
                esValido = false;
                erpPrimerApellido.SetError(txtPrimerApellido, "El campo Primer Apellido es obligatorio");
            }
            if (string.IsNullOrEmpty(txtSegundoApellido.Text))
            {
                esValido = false;
                erpSegundoApellido.SetError(txtSegundoApellido, "El campo Segundo Apellido es obligatorio");
            }
            if (string.IsNullOrEmpty(txtCedulaIdentidad.Text))
            {
                esValido = false;
                erpCedulaIdentidad.SetError(txtCedulaIdentidad, "El campo Cedula Identidad es obligatorio");
            }

            return esValido;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                var cliente = new Cliente();
                cliente.nombre = txtNombre.Text.Trim();
                cliente.primerApellido = txtPrimerApellido.Text.Trim();
                cliente.segundoApellido = txtSegundoApellido.Text;
                cliente.cedulaIdentidad = txtCedulaIdentidad.Text.Trim();
                cliente.usuarioRegistro = "LabRestaurante";

                if (esNuevo)
                {
                    cliente.fechaRegistro = DateTime.Now;
                    cliente.estado = 1;
                    ClienteCln.insertar(cliente);
                }
                else
                {
                    int index = dgvLista.CurrentCell.RowIndex;
                    cliente.id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
                    ClienteCln.actualizar(cliente);
                }
                listar();
                btnCancelar.PerformClick();
                MessageBox.Show("El cliente ha sido guardado correctamente", "::: Restaurante - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private void limpiar()
        {
            txtNombre.Text = string.Empty;
            txtPrimerApellido.Text = string.Empty;
            txtSegundoApellido.Text = string.Empty;
            txtCedulaIdentidad.Text = string.Empty;

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            string nombre = dgvLista.Rows[index].Cells["nombre"].Value.ToString();
            DialogResult dialog = MessageBox.Show($"¿Está seguro que desea dar de baja cliente {nombre}?",
                "::: Restaurante - Mensaje :::", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialog == DialogResult.OK)
            {
                ClienteCln.eliminar(id, "Parcial2");
                listar();
                MessageBox.Show("El cliente ha sido dado de baja correctamente", "::: Restaurante - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtSegundoApellido_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new frmPrincipal().ShowDialog();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtParametro_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
