using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CRUD_ASP.net.Models
{
    [Table("receipt_detail")]
    public class ReceiptDetail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "PRODUCTO")]
        [Column("product_name")]
        public string? Product_name { get; set; } // Se permite null
        [Required]
        [Display(Name = "MONTO")]
        [Precision(18, 2)]  // Define la precisión
        [Column("amount")]
        public decimal Amount { get; set; }
        [Required]
        [Display(Name = "RECEIPT ID")]
        [Column("receipt_id")]
        public int? ReceiptId { get; set; }

        [JsonIgnore] // Esto evita el problema de referencia circular
        public Receipt? Receipt { get; set; }
    }
}
