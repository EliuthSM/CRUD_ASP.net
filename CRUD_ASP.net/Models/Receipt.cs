using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD_ASP.net.Models
{
    [Table("receipt")]
    public class Receipt
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "TIPO COMPROBANTE")]
        [Column("type_receipt")]
        public string? TypeReceipt { get; set; } // Se permite null
        [Required]
        [Display(Name = "COMPROBANTE")]
        [Column("receipt_number")]
        public string? ReceiptNumber { get; set; }
        [Required]
        [Display(Name = "OBSERVACIONES")]
        [Column("observations")]
        public string? Observations { get; set; }
        [Required]
        [Display(Name = "FECHA DE EMISIÓN")]
        [Column("date_emision")]
        public DateTime? DateEmision { get; set; }
        [Required]
        [Display(Name = "MONTO TOTAL")]
        [Column("amount_total")]
        public decimal AmountTotal { get; set; }

        public ICollection<ReceiptDetail> ReceiptDetails{ get; set; }
    }

}