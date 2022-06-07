using System;
using System.Collections.Generic;
using System.Text;

namespace _01_DAL.ViewModel
{
    public class Fatura_Empregado
    {
        public int Empregado_ID { get; set; }
        public int Fatura_ID { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
    }
}
