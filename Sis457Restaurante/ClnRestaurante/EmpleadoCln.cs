using CadRestaurante;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnRestaurante
{
    public class EmpleadoCln
    {
        public static int insertar(Empleado empleado)
        {
            using (var context = new LabRestauranteMPEntities())
            {
                context.Empleado.Add(empleado);
                context.SaveChanges();
                return empleado.id;
            }
        }

        public static int actualizar(Empleado empleado)
        {
            using (var context = new LabRestauranteMPEntities())
            {
                var existente = context.Empleado.Find(empleado.id);
                existente.nombre = empleado.nombre;
                existente.primerApellido = empleado.primerApellido;
                existente.segundoApellido = empleado.segundoApellido;
                existente.telefono = empleado.telefono;
                existente.direccion = empleado.direccion;
                existente.cargo = empleado.cargo;
                existente.usuarioRegistro = empleado.usuarioRegistro;
                return context.SaveChanges();
            }
        }

        public static int eliminar(int id, string usuarioRegistro)
        {
            using (var context = new LabRestauranteMPEntities())
            {
                var existente = context.Empleado.Find(id);
                existente.estado = -1;
                existente.usuarioRegistro = usuarioRegistro;
                return context.SaveChanges();
            }
        }

        public static Empleado get(int id)
        {
            using (var context = new LabRestauranteMPEntities())
            {
                return context.Empleado.Find(id);
            }
        }

        public static List<Empleado> listar()
        {
            using (var context = new LabRestauranteMPEntities())
            {
                return context.Empleado.Where(x => x.estado != -1).ToList();
            }
        }

        public static List<paEmpleadoListar_Result> listarPa(string parametro)
        {
            using (var context = new LabRestauranteMPEntities())
            {
                return context.paEmpleadoListar(parametro).ToList();
            }
        }
    }
}
