namespace Local.DTOS.CartItems
{
    public class CartItemCreate
    {  
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public int Amount { get; set; }  
    }
}
