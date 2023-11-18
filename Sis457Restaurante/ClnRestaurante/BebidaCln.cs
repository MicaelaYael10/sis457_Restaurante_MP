using CadRestaurante;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace ClnRestaurante
{
    public class BebidaCln
    {
        public static int insertar(Bebida bebida)

        {
            using (var context = new LabRestauranteMPEntities())
            {
                context.Bebida.Add(bebida);
                context.SaveChanges();
                return bebida.id;
            }
        }

        public static int actualizar(Bebida bebida)
        {
            using (var context = new LabRestauranteMPEntities()) {
                var existente = context.Bebida.Find(bebida.id);
                existente.nombre = bebida.nombre;
                existente.precio = bebida.precio;
               
                existente.usuarioRegistro = bebida.usuarioRegistro;
                return context.SaveChanges();
            }
               
        }

        public static int eliminar(int id, string usuarioRegistro)
        {
            using (var context = new LabRestauranteMPEntities())
            {
                var existente = context.Bebida.Find(id);
                existente.estado = -1;
                existente.usuarioRegistro = usuarioRegistro;
                return context.SaveChanges();
            }
        }

        public static Bebida get(int id)
        {
            using (var context = new LabRestauranteMPEntities())
            {
                return context.Bebida.Find(id);
            }
        }

        public static List<Bebida> listar()
        {
            using (var context = new LabRestauranteMPEntities())
            {
                return context.Bebida.Where(x => x.estado != -1).ToList();
            }
        }

        public static List<paBebidasListar_Result> listarPa(string parametro)
        {
            using (var context = new LabRestauranteMPEntities())
            {
                return context.paBebidasListar(parametro).ToList();
            }
        }
    }


}
