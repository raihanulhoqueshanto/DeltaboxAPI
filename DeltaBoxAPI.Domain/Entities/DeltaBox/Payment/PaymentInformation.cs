using DeltaboxAPI.Domain.Entities.DeltaBox.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Domain.Entities.DeltaBox.Payment
{
    [Table("payment_information")]
    public class PaymentInformation : CommonEntity
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("customer_id")]
        public string CustomerId { get; set; }

        [Column("invoice_no")]
        public string InvoiceNo { get; set; }

        [Column("invoice_id")]
        public string InvoiceId { get; set; }

        [Column("payment_method")]
        public string PaymentMethod { get; set; }

        [Column("sender_number")]
        public string SenderNumber { get; set; }

        [Column("transaction_id")]
        public string TransactionId { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("amount")]
        public string Amount { get; set; }

        [Column("fee")]
        public string Fee { get; set; }

        [Column("charged_amount")]
        public string ChargedAmount { get; set; }
    }
}
