﻿using DigitalBank.Domain.Enumerations;
using DigitalBank.Domain.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBank.Domain.Entities
{
    public class Conta : IConta
    {
        [Key]
        public long? id { get; set; }
                
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Range(1, Int64.MaxValue, ErrorMessage = "O campo {0} é inválido.")]
        [Display(Name = "número")]
        public long numero { get; set; }
        
        public decimal saldo { get; private set; }
        
        [Display(Name = "situação")]
        public SituacaoConta situacao { get; private set; }
        
        [DataType(DataType.DateTime)]
        [Display(Name = "data de cadastro")]
        
        public DateTime dataDeCadastro { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "data de finalização")]
        public DateTime? dataDeFinalizacao { get; set; }

        public Cliente cliente { get; set; }

        public Conta()
        {
            saldo = 0;
            situacao = SituacaoConta.Ativa;
            dataDeCadastro = DateTime.Now;
            dataDeFinalizacao = null;
        }

        public Conta(Cliente cliente) : this()
        {
            this.cliente = cliente;
            Random random = new Random();
            numero = random.Next();
        }

        public Conta(Cliente cliente, long numero) : this()
        {
            this.cliente = cliente;
            this.numero = numero;
        }

        public virtual bool Sacar(decimal valor)
        {
            if (valor > saldo)
                return false;
            saldo -= valor;
            return true;
        }

        public bool Depositar(decimal valor)
        {
            saldo += valor;
            return true;
        }

        public string VerSaldo()
        {
            return saldo.ToString("C2");
        }
        public string Finalizar()
        {
            if (dataDeFinalizacao != null)
                return "Essa conta já está finalizada / fechada.";
            else if (saldo != 0)
                return "Para fechar a conta o saldo precisa ser igual a R$ 0,00.";
            else
            {
                situacao = SituacaoConta.Finalizada;
                dataDeFinalizacao = DateTime.Now;
                return "ok";
            }
        }
    }
}

