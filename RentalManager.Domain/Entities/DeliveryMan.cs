using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalManager.Domain.Entities
{
    public class DeliveryMan
    {
        public DeliveryMan(string identificador, string nome, string cnpj, DateTime dataNascimento,
            string numeroCNH, string tipoCNH, string imagemCNH)
        {
            Identificador = identificador;
            Nome = nome;
            CNPJ = cnpj;
            DataNascimento = dataNascimento;
            NumeroCNH = numeroCNH;
            TipoCNH = tipoCNH;
            ImagemCNH = imagemCNH;
        }

        public string Identificador { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public DateTime DataNascimento { get; set; }
        public string NumeroCNH { get; set; }
        public string TipoCNH { get; set; }
        public string ImagemCNH { get; set; }
    }
}
