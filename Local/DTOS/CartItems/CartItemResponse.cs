using Local.Models;
using Local.DTOS.Products;
using Local.Settings;

namespace Local.DTOS.CartItems
{
    public class CartItemResponse
    {
        static public object CartItemProductCateOneImg(CartItem data) => new
        {
            data.Id,
            data.ProductId,
            data.Amount,
            data.CreateDate,
            SumAmountPrice = data.Product.ProductPrice * data.Amount,
            ProductName = data.Product.ProductName,
            ProductCategoryName = data.Product.Category.CategoryName,
            ProductPrice = data.Product.ProductPrice,
            ProductImage = data.Product.ProductImg.Count > 0 
                ? Constants.PathServer + data.Product.ProductImg.First().ProductImgName
                : null,
        };


    }
}