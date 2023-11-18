using CadRestaurante;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnRestaurante
{
    public class ComidaCln
    {
        public static int insertar(Comida comida)
        {
            using (var context = new LabRestauranteMPEntities())
            {
                context.Comida.Add(comida);
                context.SaveChanges();
                return comida.id;   
            }
        }

        public static int actualizar(Comida comida)
        {
            using (var context = new LabRestauranteMPEntities())
            {
                var existente = context.Comida.Find(comida.id);
                existente.nombre = comida.nombre;
                existente.precio = comida.precio;
                existente.usuarioRegistro = comida.usuarioRegistro;
                return context.SaveChanges();
            }
        }

        public static int eliminar(int id, string usuarioRegistro)
        {
            using (var context = new LabRestauranteMPEntities())
            {
                var existente = context.Comida.Find(id);
                existente.estado = -1;
                existente.usuarioRegistro = usuarioRegistro;
                return context.SaveChanges();
            }
        }

        public static Comida get(int id)
        {
            using (var context = new LabRestauranteMPEntities())
            {
                return context.Comida.Find(id);
            }
        }

        public static List<Comida> listar()
        {
            using (var context = new LabRestauranteMPEntities())
            {
                return context.Comida.Where(x => x.estado != -1).ToList();
            }
        }

        public static List<paComidaListar_Result> listarPa(string parametro)
        {
            using (var context = new LabRestauranteMPEntities())
            {
                return context.paComidaListar(parametro).ToList();
            }
        }
    }
}
