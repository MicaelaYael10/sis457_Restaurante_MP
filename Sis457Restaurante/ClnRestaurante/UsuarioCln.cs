using CadRestaurante;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace ClnRestaurante
{
    public class UsuarioCln
    {
        public static int insertar(Usuarios usuarios)
        {
            using (var context = new LabRestauranteMPEntities())
            {
                context.Usuarios.Add(usuarios);
                context.SaveChanges();
                return usuarios.id;
            }
        }

        public static int actualizar(Usuarios usuarios)
        {
            using (var context = new LabRestauranteMPEntities())
            {
                var existente = context.Usuarios.Find(usuarios.id);
                existente.usuario = usuarios.usuario.Trim();
                existente.clave = usuarios.clave;
                existente.usuarioRegistro = usuarios.usuarioRegistro;
                return context.SaveChanges();
            }
        }
        public static int eliminar(int id, string usuario)
        {
            using (var context = new LabRestauranteMPEntities())
            {
                var existente = context.Usuarios.Find(id);
                existente.estado = -1;
                existente.usuarioRegistro = usuario;
                return context.SaveChanges();
            }
        }

        public static Usuarios get(int id)
        {
            using (var context = new LabRestauranteMPEntities())
            {
                return context.Usuarios.Find(id);
            }
        }

        public static List<Usuarios> listar()
        {
            using(var context = new LabRestauranteMPEntities())
            {
                return context.Usuarios.Where(x => x.estado !=-1).ToList();
            }
        }
        public static List<paUsuariosListar_Result> listarPa(string parametro)
        {
            using (var contexto = new LabRestauranteMPEntities())
            {
                return contexto.paUsuariosListar(parametro).ToList();
            }
        }
        public static Usuarios validar (string usuario, string clave)
        {
            using (var context = new LabRestauranteMPEntities())
            {
                return context.Usuarios
                    .Where(x => x.usuario == usuario && x.clave == clave)
                    .FirstOrDefault();
            }
        }
    }
}
