using Local.DTOS.CartItems;
using Local.Settings;
using Local.Interfaces;
using Local.Models;
using Local.Models.Data;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Local.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly LocaldbContext context;
        public CartItemService(LocaldbContext context)
        {
            this.context = context;
        }

        public async Task<object> CreateCartItem(CartItemCreate data)
        {
            var result = await context.CartItems.FirstOrDefaultAsync(a => 
                a.UserId.Equals(data.UserId) && a.ProductId.Equals(data.ProductId));
            var product = await context.Products.FirstOrDefaultAsync(a => a.ProductId.Equals(data.ProductId));
            if (product is null) return Constants.Return400("ไม่พบสินค้า");
            if (result is not null)
            { 
                if (data.Amount + result.Amount > product!.ProductStock) return Constants.Return400("สินค้าไม่เพียงพอ");
                result.Amount += data.Amount;
                context.CartItems.Update(result);
            }
            else
            { 
                if (data.Amount > product!.ProductStock) return Constants.Return400("สินค้าไม่เพียงพอ");
                var newItem = data.Adapt<CartItem>();
                newItem.CreateDate = DateTime.Now;
                await context.CartItems.AddAsync(newItem);
            } 
            await context.SaveChangesAsync(); 
            return Constants.Return200("บันทึกข้อมูลสำเร็จ"); 
        }

        public async Task<object> DeleteCartItem(int id)
        {
            var result = await context.CartItems.Include(a => a.Product).FirstOrDefaultAsync(a => a.Id.Equals(id));
            if (result is null) return Constants.Return400("ไม่พบข้อมูล");

            context.CartItems.Remove(result);
            await context.SaveChangesAsync();
            return Constants.Return200("ลบสินค้า");
        }

        public async Task<object> GetCartItemByAccountId(string id)
        {
            var result = await context.CartItems.Where(a => a.UserId.Equals(id))
                .Include(a => a.Product)
                    .ThenInclude(a => a.Category)
                .Include(a => a.Product)
                    .ThenInclude(a => a.ProductImg)
                .ToListAsync();
            return new
            {
                statusCode = 200,
                message = "success",
                total = result.Sum(a => a.Amount * a.Product.ProductPrice),
                data = result.Select(CartItemResponse.CartItemProductCateOneImg)
            }; 
        }

        public async Task<object> ItemPlus(int id)
        {
            var result = await context.CartItems.Include(a => a.Product).FirstOrDefaultAsync(a => a.Id.Equals(id));
            if (result is null) return Constants.Return400("ไม่พบข้อมูล");
            if (result.Amount >= result.Product.ProductStock) return Constants.Return400("สินค้าไม่เพียงพอ");

            result.Amount = result.Amount+=1;
            context.CartItems.Update(result);
            await context.SaveChangesAsync();
            return Constants.Return200("เพิ่มจำนวนสำเร็จ");
        }

        public async Task<object> ItemRemove(int id)
        {
            var result = await context.CartItems.Include(a => a.Product).FirstOrDefaultAsync(a => a.Id.Equals(id));
            if (result is null) return Constants.Return400("ไม่พบข้อมูล");
            if (result.Amount <= 1) return Constants.Return400("เกิดข้อผิดพลาด ไม่สามารถลบข้อมูล");

            result.Amount = result.Amount-=1;
            context.CartItems.Update(result);
            await context.SaveChangesAsync();
            return Constants.Return200("ลบจำนวนสำเร็จ");
        }

    }
}
