namespace App.Web.Models
{
    public class Payment
    {
        public int Price { get; set; }
        public string Description { get; set; }
        public CategoryEnum Category { get; set; }
        public int MonthId { get; set; }
    }
}
