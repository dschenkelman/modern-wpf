namespace ModernWPF.EntityFramework
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Stock
    {
        [ForeignKey("Company"), Key, Column(Order = 0)]
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }

        [Key, Column(Order = 1)]
        public DateTime Date { get; set; }

        public double Price { get; set; }
    }
}
