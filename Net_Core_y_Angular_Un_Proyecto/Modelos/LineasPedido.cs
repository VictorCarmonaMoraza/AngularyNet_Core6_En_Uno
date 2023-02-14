using System;
using System.Collections.Generic;

namespace Net_Core_y_Angular_Un_Proyecto.Modelos
{
    public partial class LineasPedido
    {
        public int Id { get; set; }
        public int IdPedido { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal ImporteUnitario { get; set; }

        public virtual Pedido IdPedidoNavigation { get; set; } = null!;
        public virtual Producto IdProductoNavigation { get; set; } = null!;
    }
}
