using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class FrmAgenda : Form
    {   
        private char operation;
        private int id;
        N_agenda negocio = new N_agenda();
        E_agenda entidad = new E_agenda();
        public FrmAgenda()
        {
            InitializeComponent();
            limpiar();
        }
        private void limpiar()
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtDireccion.Text = "";
            txtFecha.Text = "";
            txtCelular.Text = "";

            txtNombre.Enabled = false;
            txtApellido.Enabled = false;
            txtDireccion.Enabled = false;
            txtCelular.Enabled = false;

            BT.HideSync(btnSave);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CargarDatos()
        {
            if (txtBuscar.Text == string.Empty)
            {
                limpiar();
            }
            else
            {
                entidad = negocio.ListarDatos(txtBuscar.Text);
                id = entidad.Id;
                txtNombre.Text = entidad.Nombre;
                txtApellido.Text = entidad.Apellido;
                txtDireccion.Text = entidad.Direccion;
                bunifuDatepicker1.Value = entidad.Fecha_nacimiento;
                txtCelular.Text = entidad.Celular;
            }
        }
        private void btnUpdateCancel_Click(object sender, EventArgs e)
        {
        }
        private void crud()
        {
            if (operation.Equals('C'))
            {
                if (txtNombre.Text == string.Empty || txtApellido.Text == string.Empty || txtDireccion.Text == string.Empty || txtCelular.Text == string.Empty)
                {
                    MessageBox.Show("No se aceptan campos vacios{create}");
                }
                else
                {
                    try
                    {
                        entidad.Nombre = txtNombre.Text.ToUpper();
                        entidad.Apellido = txtApellido.Text.ToUpper();
                        entidad.Direccion = txtDireccion.Text.ToUpper();
                        entidad.Fecha_nacimiento = bunifuDatepicker1.Value;
                        entidad.Celular = txtCelular.Text;
                        negocio.create(entidad);

                        MessageBox.Show("Regisstro creado con exito!!");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("No se pudo crear el registro" + e.Message);
                    }
                    finally
                    {
                        txtBuscar.Text = txtNombre.Text;
                        limpiar();
                        CargarDatos();
                    }
                }
                
            }
            else if (operation.Equals('U'))
            {
                if (txtNombre.Text == string.Empty || txtApellido.Text == string.Empty || txtDireccion.Text == string.Empty || txtCelular.Text == string.Empty)
                {
                    MessageBox.Show("No se aceptan campos vacios {update}");
                }
                else
                {
                    try
                    {
                        entidad.Id = id;
                        entidad.Nombre = txtNombre.Text.ToUpper();
                        entidad.Apellido = txtApellido.Text.ToUpper();
                        entidad.Direccion = txtDireccion.Text.ToUpper();
                        entidad.Fecha_nacimiento = bunifuDatepicker1.Value;
                        entidad.Celular = txtCelular.Text;
                        negocio.update(entidad);

                        MessageBox.Show("Registro actualizado con exito!!");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("No se pudo alterar el registro" + e.Message);
                    }
                    finally
                    {
                        txtBuscar.Text = txtNombre.Text;
                        limpiar();
                        CargarDatos();
                    }
                }
            }
            else if (operation.Equals('D'))
            {
                try
                {
                    entidad.Id = id;
                    negocio.delete(entidad);
                    MessageBox.Show("Registro elimina con exito");
                }
                catch (Exception e)
                {
                    MessageBox.Show("No se pudo eliminar el registro" + e.Message);
                }
                finally
                {
                    txtBuscar.Text = txtNombre.Text;
                    limpiar();
                    CargarDatos();
                }
            }
        }

        private void txtBuscar_OnValueChanged(object sender, EventArgs e)
        {
            CargarDatos();
            
        }
        //boton de nuevo registro
        private void btnCreateSave_Click_1(object sender, EventArgs e)
        {
            operation = 'C';
            txtNombre.Enabled = true;
            txtApellido.Enabled = true;
            txtDireccion.Enabled = true;
            txtCelular.Enabled = true;

            BT.ShowSync(btnSave);
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (btnCreate.Visible == false || btnUpdate.Visible == false || btnDelete.Visible == false)
            {
                BT.ShowSync(btnCreate);

                BT.ShowSync(btnUpdate);

                BT.ShowSync(btnDelete);

            }
            else
            {
                BT.HideSync(btnCreate);

                BT.HideSync(btnUpdate);

                BT.HideSync(btnDelete);

            }

            if (btnSave.Visible == true)
            {
                BT.HideSync(btnSave);
            }


        }
        //boton de eliminar un registro
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtBuscar.Text == string.Empty)
            {
                MessageBox.Show("Escribe en el buscador el contacto que deseas eliminar");
            }
            else
            {
                operation = 'D';
                BT.ShowSync(btnSave);
            }
        }

        private void btnUpdateCancel_Click_1(object sender, EventArgs e)
        {
            if (txtBuscar.Text == string.Empty)
            {
                MessageBox.Show("Escribe en el buscador el contacto que deseas alterar");
            }
            else
            {
                operation = 'U';
                txtNombre.Enabled = true;
                txtApellido.Enabled = true;
                txtDireccion.Enabled = true;
                txtCelular.Enabled = true;

                BT.ShowSync(btnSave);
            }
        }

        private void bunifuDatepicker1_onValueChanged(object sender, EventArgs e)
        {
            txtFecha.Text = Convert.ToString(bunifuDatepicker1.Value);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            crud();
        }


    }
}
