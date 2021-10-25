namespace DataContext.Models
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FoodServedTypeId { get; set; }
        public FoodServedType FoodServedType { get; set; }
        public double Price { get; set; }
        public bool IsAFoodItem { get; set; }

    }
}
