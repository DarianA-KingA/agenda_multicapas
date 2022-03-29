using Microsoft.Analytics.Interfaces;
using Microsoft.Analytics.Interfaces.Streaming;
using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CapaAccesoDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class N_agenda
    {
        D_agenda datos = new D_agenda();

        public E_agenda ListarDatos(String buscar)
        {
           return datos.ListarDatos(buscar);
        }
        public void create(E_agenda agenda)
        {
            datos.InsertarDatos(agenda);
        }
        public void update(E_agenda agenda)
        {
            datos.CambiarDatos(agenda);
        }
        public void delete(E_agenda agenda)
        {
            datos.EliminarDatos(agenda);
        }
    }
}