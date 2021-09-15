using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP4_WRITE
{
    class Producto
    {
        private long id;
        private DateTime fechaDeAlta;
        private string codigo;
        private string denominacion;
        private double precio;
        private char publicado;

        public long Id { get => id; set => id = value; }
        public DateTime FechaDeAlta { get => fechaDeAlta; set => fechaDeAlta = value; }
        public string Codigo { get => codigo; set => codigo = value; }
        public string Denominacion { get => denominacion; set => denominacion = value; }
        public double Precio { get => precio; set => precio = value; }
        public char Publicado { get => publicado; set => publicado = value; }
    }
}
