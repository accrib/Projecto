using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace _01_DAL
{

    public class Produto
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "ID Linha de Produto")]
        public int ID { get; set; }

        [Display(Name = "Nome do Produto")]
        public string Nome { get; set; }

        [Display(Name = "Descrição do Produto")]
        public string Descricao_Produto { get; set; }


        // Um produto está associado a apenas um Empregado
        [ForeignKey("Empregado")]
        [Display(Name = "ID Empregado")]
        public int ID_Empregado { get; set; }
        public virtual Empregado Empregado { get; set; }

        [Display(Name = "Faturas")]
        // Um produto pode estar em várias faturas
        public virtual List<Fatura> Faturas { get; set; }

        [Display(Name = "Linhas de Faturas")]
        // Um produto pode estar em várias linhas de faturas
        public virtual List<Linha_Fatura> Lista_Linhas_Faturas { get; set; }
    }
}
