namespace JSONServices.Models
{
    public class Order
    {
        public int Id { get; set; }
        public List<int> ProductIds { get; set; }
        public DateTime Date { get; set; } // Обратите внимание, что у вас было неправильно написано "date" вместо "Date"
    }

    public class OrderRequest
    {
        public List<int> ProductIds { get; set; }
    }

}
