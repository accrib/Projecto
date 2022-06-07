using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace _01_DAL
{
    public class Fatura
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "ID Fatura")]
        public int ID { get; set; }

        //[Display(Name = "Data da Fatura")]
        public DateTime Data_Fatura { get; set; }


        //Uma fatura está associada a apenas um Empregado
        [ForeignKey("Empregado")]
        [Display(Name = "ID Empregado")]
        public int ID_Empregado { get; set; }
        public virtual Empregado Empregado { get; set; }

        [Display(Name = "Lista de Linhas de Faturas")]
        // Uma fatura tem várias linhas de fatura
        public virtual List<Linha_Fatura> Lista_Linhas_Faturas { get; set; }

        [Display(Name = "Preco Final")]
        public double Preco_Final { get; set; }
    }
}
