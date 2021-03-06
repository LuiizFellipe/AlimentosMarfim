﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AlimentosMarfim.Models
{
    public class Pedido
    {

        [Key]
        public int Id { get; set; }

        [Display(Name = "Data da Compra")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "(0:dd-MM-yyyy)", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Campo de datas é obrigatório")]
        public DateTime DataCompra { get; set; }


        // chave estrangeira da model cliente
        [ForeignKey("ClienteId")]
        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }
        // referenciando a model cliente
        [Required(ErrorMessage = "Nome do Cliente é obrigatório")]
        public Cliente Cliente { get; set; }

        // chave estrangeira da model Funcionário
        [ForeignKey("FunicionarioId")]
        [Display(Name = "Funcionário")]
        public int FuncionarioId { get; set; }
        //referenciando a model funcionario
        [Required(ErrorMessage = "Nome do Funcionário é obrigatório")]
        public Funcionario Funcionario { get; set; }


        // chave estrangeira da model Produto
        [ForeignKey("ProdutoId")]
        [Display(Name = "Produtos")]
        public int ProdutoId { get; set; }
        //referenciando a model produto
        public Produto Produtos { get; set; }

    }
}
