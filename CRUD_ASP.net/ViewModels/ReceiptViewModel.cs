using CRUD_ASP.net.Models;

namespace CRUD_ASP.net.ViewModels
{
    public class ReceiptViewModel
    {
        public Receipt Receipt { get; set; }
        public List<ReceiptDetail> ReceiptDetails { get; set; }
    }
}
