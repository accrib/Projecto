using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace _01_DAL
{
    public class Empregado
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        public string Nome { get; set; }
        public int Numero_Empregado { get; set; }

        // Um empregado pode ter associado a si uma lista de produtos
        [Display(Name = "Lista de Produtos")]
        public virtual List<Produto> Lista_Produtos { get; set; }

        // Um empregado pode ter associado a si uma lista de faturas
        [Display(Name = "Lista de Faturas")]
        public virtual List<Fatura> Lista_Faturas { get; set; }

        [Display(Name = "E-Mail")]
        public string EMail { get; set; }
    }
}
