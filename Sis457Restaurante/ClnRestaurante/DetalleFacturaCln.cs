using CadRestaurante;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnRestaurante
{
    public class DetalleFacturaCln
    {
        public static int insertar(Factura factura)
        {
            using (var context = new LabRestauranteMPEntities())
            {
                context.Factura.Add(factura);
                context.SaveChanges();
                return factura.id;
            }
        }

        public static int actualizar(Factura Factura)
        {
            using (var context = new LabRestauranteMPEntities())
            {
                var existente = context.Factura.Find(Factura.id);
                existente.idCliente = Factura.idCliente;
                existente.idEmpleado = Factura.idEmpleado;
                existente.idComida = Factura.idComida;
                existente.idBebida = Factura.idBebida;
                existente.usuarioRegistro =Factura.usuarioRegistro;
                return context.SaveChanges();
            }
        }

        public static int eliminar(int id, string usuarioRegistro)
        {
            using (var context = new LabRestauranteMPEntities())
            {
                var existente = context.Factura.Find(id);
                existente.estado = -1;
                existente.usuarioRegistro = usuarioRegistro;
                return context.SaveChanges();
            }
        }

        public static Factura get(int id)
        {
            using (var context = new LabRestauranteMPEntities())
            {
                return context.Factura.Find(id);
            }
        }

        public static List<Factura> listar()
        {
            using (var context = new LabRestauranteMPEntities())
            {
                return context.Factura.Where(x => x.estado != -1).ToList();
            }
        }

        public static List<paFacturarListar_Result> listarPa(string parametro)
        {
            using (var context = new LabRestauranteMPEntities())
            {
                return context.paFacturarListar(parametro).ToList();
            }
        }
    }
}
