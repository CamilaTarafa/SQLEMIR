using System.IO.Pipes;
using System.Reflection.Metadata;
using System.Data;
using MySql.Data.MySqlClient;

namespace EjemploSQL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {

        }


        public class Persona
        {
            public string Nombre;
            public string Apellido;
            public string ID;
            public string Sector;
            public string Contacto;
            public Persona(string nombre, string apellido, string id, string sector, string contacto)
            {
                Nombre = nombre;
                Apellido = apellido;
                ID = id;
                Sector = sector;
                Contacto = contacto;

            }
        }
     

        private void MostrarDatosEnGrilla()
        {
            try
            {
                string connectionString = "Server=localhost;Database=datos;Uid=root;Pwd=";
                
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT Nombre, Apellido, ID, Sector, Contacto from datos";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            // Asignar el DataTable al DataGridView
                            dgv_personal.DataSource = dataTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            try
            {
                string nombre = txtNombre.Text;
                string apellido = txtApellido.Text;
                string id = txtID.Text;
                string sector = txtSector.Text;
                string contacto = txtContacto.Text;


                Persona nuevaPersona = new Persona(nombre, apellido, id, sector, contacto);

                txtNombre.Clear();
                txtApellido.Clear();
                txtID.Clear();
                txtSector.Clear();
                txtContacto.Clear();

                string connectionString = "Server=localhost;Database=datos;Uid=root;Pwd=";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO datos (Nombre, Apellido, ID, Sector,Contacto) VALUES (@Nombre, @Apellido, @ID, @Sector, @Contacto)";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Nombre", nuevaPersona.Nombre);
                        command.Parameters.AddWithValue("@Apellido", nuevaPersona.Apellido);
                        command.Parameters.AddWithValue("@ID", nuevaPersona.ID);
                        command.Parameters.AddWithValue("@Sector", nuevaPersona.Sector);
                        command.Parameters.AddWithValue("@Contacto", nuevaPersona.Contacto);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("DATOS GUARDADOS CON EXITO.");
                            dgv_personal.Rows.Add(nuevaPersona.Nombre, nuevaPersona.Apellido, nuevaPersona.ID, nuevaPersona.Sector, nuevaPersona.Contacto);
                        }
                        else
                        {
                            MessageBox.Show("ALGO SALIO MAL.");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR:" + ex.Message);
            }

            MostrarDatosEnGrilla();
        }
    }



            }






        





    


    
