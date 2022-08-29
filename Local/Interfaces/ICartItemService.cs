using Local.DTOS.CartItems;

namespace Local.Interfaces
{
    public interface ICartItemService
    { 
        Task<object> GetCartItemByAccountId(string id);
        Task<object> ItemPlus(int id);
        Task<object> ItemRemove(int id);
        Task<object> CreateCartItem(CartItemCreate data);
        Task<object> DeleteCartItem(int id);
    }
}
