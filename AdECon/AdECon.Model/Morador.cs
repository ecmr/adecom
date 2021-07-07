using System;

namespace AdECon.Model
{
    public class Morador
    {
        public int IdMorador {get; set;}
        public string Bloco { get; set; }
        public string Apartamento { get; set; }
        public string NomeDestinatario { get; set; }
        public string email { get; set; }
        public string NumeroCelular { get; set; }
        public string CodigoBarraEtiqueta { get; set; }
        public string CodigoBarraEtiquetaLocal { get; set; }
        public int LocalPrateleira { get; set; }
    }
}
