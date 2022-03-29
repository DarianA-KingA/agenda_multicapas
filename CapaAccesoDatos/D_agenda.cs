using Microsoft.Analytics.Interfaces;
using Microsoft.Analytics.Interfaces.Streaming;
using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using CapaEntidad;

namespace CapaAccesoDatos
{
    public class D_agenda
    //irás no volverás 
    //irás no, volverás 
    {
        //conexion a la base de datos
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conectar"].ConnectionString);
        //metodopara mostrar los datos  nombre, apellido, direccion, fecha de nacimiento y celular a traves del nombre
        public E_agenda ListarDatos(String buscar)
        {
            try
            {
                con.Open();
                SqlDataReader LeerFilar;
                SqlCommand cmd = new SqlCommand("sp_select", con);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@nombre", buscar);
                LeerFilar = cmd.ExecuteReader();
                List<E_agenda> listar = new List<E_agenda>();
                while (LeerFilar.Read())
                {
                    listar.Add(new E_agenda
                    {
                        Id = LeerFilar.GetInt32(0),
                        Nombre = LeerFilar.GetString(1),
                        Apellido = LeerFilar.GetString(2),
                        Direccion = LeerFilar.GetString(3),
                        Fecha_nacimiento = LeerFilar.GetDateTime(4),
                        Celular = LeerFilar.GetString(5),
                    });
                }
                con.Close();
                LeerFilar.Close();
                return listar[listar.Count - 1];
            }
            catch (Exception e)
            {
                E_agenda entidad = new E_agenda
                {
                    Id = 0,
                    Nombre = "...",
                    Apellido ="...",
                    Direccion = "...",
                    Fecha_nacimiento = DateTime.Now,
                    Celular = "..."
                };
                return entidad;

            }
            
        }
        //metoda para nuevo registro o insertar datos ,recibe nombre, apellido, direccion, fecha de nacimiento y celular
        public void InsertarDatos(E_agenda agenda)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_insertar",con);
            cmd.CommandType = CommandType.StoredProcedure;
            

            cmd.Parameters.AddWithValue("@nombre", agenda.Nombre);
            cmd.Parameters.AddWithValue("@apellido", agenda.Apellido);
            cmd.Parameters.AddWithValue("@direccion", agenda.Direccion);
            cmd.Parameters.AddWithValue("@fecha", agenda.Fecha_nacimiento);
            cmd.Parameters.AddWithValue("@celular", agenda.Celular);

            cmd.ExecuteNonQuery();
            con.Close();
        }
        //metodo para alterar un registro ya existente, a traves del id, y recibe nombre, apellido, direccion, fecha de nacimiento y celular
        public void CambiarDatos(E_agenda agenda)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_update",con);
            cmd.CommandType = CommandType.StoredProcedure;
            

            cmd.Parameters.AddWithValue("@id",agenda.Id);
            cmd.Parameters.AddWithValue("@nombre",agenda.Nombre);
            cmd.Parameters.AddWithValue("@apellido",agenda.Apellido);
            cmd.Parameters.AddWithValue("@direccion",agenda.Direccion);
            cmd.Parameters.AddWithValue("@fecha",agenda.Fecha_nacimiento);
            cmd.Parameters.AddWithValue("@celular",agenda.Celular);

            cmd.ExecuteNonQuery();
            con.Close();
        }
        //metodo para eliminar un registro, a traves del id
        public void EliminarDatos(E_agenda agenda)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_delete",con);
            cmd.CommandType = CommandType.StoredProcedure;  
            

            cmd.Parameters.AddWithValue("@id",agenda.Id);
            cmd.ExecuteNonQuery();

            con.Close();
        }
    }
}