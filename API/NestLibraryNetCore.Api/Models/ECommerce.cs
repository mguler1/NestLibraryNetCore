using Nest;

namespace NestLibraryNetCore.Api.Models
{
    public class ECommerce
    {
        [PropertyName("_id")]
        public string? Id { get; set; }
        [PropertyName("customer_first_name")]
        public string? CustomerFirstName { get; set; }
        [PropertyName("customer_last_name")]
        public string? CustomerLastName { get; set; }
        [PropertyName("customer_full_name")]
        public string? CustomerFullName { get; set; }
        [PropertyName("category")]
        public string[]? Category { get; set; }
        [PropertyName("order_id")]
        public int OrderId { get; set; }
        [PropertyName("taxtful_total_price")]
        public double TaxFulTotalPrice { get; set; }

        [PropertyName("order_date")]
        public DateTime OrderDate { get; set; }
        [PropertyName("products")]
        public Products[]? Products { get; set; }
    }

    public class Products
    {
        [PropertyName("product_id")]
        public long ProductId { get; set; }
        [PropertyName("product_name")]
        public string? ProductName { get; set; }
    }
}
