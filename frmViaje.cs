using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


//CURSO – 1w3  LEGAJO 11 3059 – APELLIDO bertella NOMBRE Gabriel

namespace TUP_PI_Recu_Viajes
{
    public partial class frmViaje : Form
    {
        private SqlConnection cnn = null;
        private SqlCommand cmd = null;
        List<Viaje> lV = new List<Viaje>();

        public frmViaje()
        {
            InitializeComponent();
            cnn = new SqlConnection(@"Data Source=FNILSSON\GABRIEL;Initial Catalog=AgenciaViaje;Integrated Security=True");

        }


        private void frmViaje_Load(object sender, EventArgs e)
        {
            cargaCombo();
            cargarLista(lstViajes, "Viajes");

        }

        private void cargarLista(ListBox lista, string nombreTabla)
        {
            int c = 0;
            lV.Clear();
            cnn.Open();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM  " + nombreTabla;
            SqlDataReader lector = cmd.ExecuteReader();
            while (lector.Read() == true)
            {
                Viaje p = new Viaje();
                if (!lector.IsDBNull(0))
                    p.pCodigo = lector.GetInt32(0);
                if (!lector.IsDBNull(1))
                    p.pDestino = lector.GetString(1);
                if (!lector.IsDBNull(2))
                    p.pTransporte = lector.GetInt32(2);
                if (!lector.IsDBNull(3))
                    p.pTipo = lector.GetInt32(3);
                if (!lector.IsDBNull(4))
                    p.pFecha = lector.GetDateTime(4);
              
                
                lV.Add(p);
                c++;
            }
            lector.Close();
            cnn.Close();

            lista.Items.Clear();
            //for (int i = 0; i < c; i++)
            //{
            //    lista.Items.Add(aPersona[i].ToString());

            //}
            for (int i = 0; i < lV.Count; i++)
            {
                lista.Items.Add(lV[i].ToString());
            }
            lista.SelectedIndex = 0;

        }




        private void cargaCombo()
        {
            try
            {
                cnn.Open(); 
                cmd = new SqlCommand("Select * From Transportes", cnn);  
                DataTable table = new DataTable();  
                table.Load(cmd.ExecuteReader());  
                cboTransporte.DataSource = table;  
                cboTransporte.DisplayMember = "nombreTransporte";  
                cboTransporte.ValueMember = "idTransporte"; // SE DA VALOR A LA SELECCION DEL COMBOX
                cboTransporte.DropDownStyle = ComboBoxStyle.DropDownList; //PROPIEDAD DE SOLO LECTURA

                cnn.Close(); //CERRAMPOS CONEXION


            }
            catch (Exception ex )
            {

                throw;
            }
            
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            btnGrabar.Enabled = true;

        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {   //REVISA QUE ALLAS LLENADO TODOS LOS CAMPOS
                if ( txtCodigo.Text == "" || txtDestino.Text == "")
                {
                    MessageBox.Show("Olvidó ingresar un campo!");
                    return; //CANCELA LA ACCION SI OLVIDASTE ALGUN CAMPO
                }
                if (cboTransporte.SelectedIndex < 0) //COMPRUEBA QUE HAYAS SELECCIONADO ALGO EN EL COMBOBOX
                {
                    MessageBox.Show("Olvidó seleccionar un transporte!");
                    return;
                }

                try //COMPRUEBA QUE HAYAS ESCRITO EL TIPO DE VALOR CORRECTO (YA SEA NUMERICO O ALFA)
                {
                    int codigo = Convert.ToInt32(txtCodigo.Text); //COMPRUEBA QUE EL CODIGO SEA UN NUMERO
                 

                }
                catch (Exception)
                {
                    MessageBox.Show("Código debe ser numéricos!");
                    return;
                }

                cnn.Open(); //ABRO CONEXION PARA CARGAR DATOS EN BD SQL
                cmd = new SqlCommand("INSERT INTO Viajes VALUES(@codigo,@destino, @transporte,@tipo,@fecha)", cnn);
                cmd.Parameters.AddWithValue("@codigo", txtCodigo.Text); //COMANDO AGREGA DATOS 
                cmd.Parameters.AddWithValue("@destino", txtDestino.Text);
                cmd.Parameters.AddWithValue("@transporte", cboTransporte.SelectedValue); //CARGA EN SQL VALOR SELECCIONADO EN COMBOBOX
 
                if (rbtNacional.Checked)
                {
                    cmd.Parameters.AddWithValue("@tipo",1);

                }else cmd.Parameters.AddWithValue("@tipo", 2);

                cmd.Parameters.AddWithValue("@fecha", dtpFecha.Value);


                cmd.ExecuteNonQuery();  

                //limpiarCampos();
               
                
                cnn.Close();
                MessageBox.Show("Viaje guardado!"); 

            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo guardar el Viaje");  
                MessageBox.Show(ex.Message);
            }
        }

        //private void limpiarCampos()
        //{
        ////
        //}

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
