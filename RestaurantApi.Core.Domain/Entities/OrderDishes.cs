namespace RestaurantApi.Core.Domain.Entities
{
    public class OrderDishes
    {
        public int OrderId { get; set; }
        public int DishId { get; set; }

        public Order? Order { get; set; }
        public Dish? Dish { get; set; }
    }
}
