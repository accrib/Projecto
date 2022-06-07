using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace _01_DAL
{
    public class Linha_Fatura
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        public int Quantidade_Produto { get; set; }
        public double Preco_Produto { get; set; }

        [ForeignKey("Produto")]
        public int ID_Produto { get; set; }
        public virtual Produto Produto { get; set; }
    }
}
