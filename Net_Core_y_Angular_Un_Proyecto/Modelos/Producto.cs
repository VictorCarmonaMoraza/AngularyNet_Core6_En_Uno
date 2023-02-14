using System;
using System.Collections.Generic;

namespace Net_Core_y_Angular_Un_Proyecto.Modelos
{
    public partial class Producto
    {
        public Producto()
        {
            LineasPedidos = new HashSet<LineasPedido>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal Precio { get; set; }
        public string Descripcion { get; set; } = null!;

        public virtual ICollection<LineasPedido> LineasPedidos { get; set; }
    }
}
