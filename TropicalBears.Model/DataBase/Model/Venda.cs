﻿using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TropicalBears.Model.DataBase.Model
{
    public class Venda
    {
        public virtual int Id { get; set; }
        public virtual Carrinho Carrinho { get; set; }
        public virtual Double ValorFrete { get; set; }
        public virtual Double ValorTotal { get; set; }
        public virtual FormaPagamento FormaPagamento { get; set; }
        public virtual Endereco Endereco { get; set; }
        public virtual int Status { get; set; }

    }
    public class VendaMap : ClassMapping<Venda>
    {
        public VendaMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.GuidComb));

            OneToOne(x => x.Carrinho, m => {
                m.Constrained(true);
                m.PropertyReference(typeof(Carrinho).GetPropertyOrFieldMatchingName("carrinho_id"));
            });

            ManyToOne(x => x.Endereco, m => {
                m.Cascade(Cascade.None);
                m.Column("endereco_id");
                m.Class(typeof(Endereco));
            });

            ManyToOne(x => x.FormaPagamento, m => {
                m.Cascade(Cascade.None);
                m.Column("formapagamento_id");
                m.Class(typeof(FormaPagamento));
            });
        }
    }
}
