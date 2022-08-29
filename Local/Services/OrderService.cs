//using BackCoffeeV3.DTOS.OrderItems;
//using BackCoffeeV3.DTOS.Orders;
//using BackCoffeeV3.Helper;
//using BackCoffeeV3.Interfaces;
//using BackCoffeeV3.Models;
//using BackCoffeeV3.Models.Data;
//using Mapster;
//using Microsoft.EntityFrameworkCore;

//namespace BackCoffeeV3.Services
//{
//    public class OrderService : IOrderService
//    {
//        private readonly DatabaseContext context;
//        private readonly IUploadFileService uploadFileService;
//        private readonly ICartItemService cartItemService;
//        public OrderService(DatabaseContext context, IUploadFileService uploadFileService, ICartItemService cartItemService)
//        {
//            this.context = context;
//            this.uploadFileService = uploadFileService;
//            this.cartItemService = cartItemService;
//        }

//        public async Task<object> CancelStatusOrder(string id)
//        {
//            var result = await context.Orders.FirstOrDefaultAsync(a => a.Id.Equals(id));
//            if (result is null) return Constants.Return400("ไม่พบข้อมูล");

//            result.Status = "0";
//            context.Orders.Update(result);
//            await context.SaveChangesAsync();
//            return Constants.Return200("บันทึกข้อมูลสำเร็จ");
//        }

//        public async Task<object> ConfirmStatusOrder(string id)
//        {
//            var result = await context.Orders.FirstOrDefaultAsync(a => a.Id.Equals(id));
//            if (result is null) return Constants.Return400("ไม่พบข้อมูล");

//            result.Status = "3";
//            context.Orders.Update(result);
//            await context.SaveChangesAsync();
//            return Constants.Return200("บันทึกข้อมูลสำเร็จ");
//        }

//        public async Task<object> ConfirmStatusPayment(string id)
//        {
//            var result = await context.Orders.FirstOrDefaultAsync(a => a.Id.Equals(id));
//            if (result is null) return Constants.Return400("ไม่พบข้อมูล");

//            result.Status = "2";
//            context.Orders.Update(result);
//            await context.SaveChangesAsync();
//            return Constants.Return200("บันทึกข้อมูลสำเร็จ");
//        }

//        public async Task<object> CreateOrder(OrderCreate data)
//        {
//            if (data.OrderItem.Count == 0) return Constants.Return400("เกิดข้อพิดพลาด");

//            var errMsg = await OrderCheckStock(data.OrderItem);
//            if (!string.IsNullOrEmpty(errMsg)) return Constants.Return400(errMsg);

//            #region New Order
//            var newOrder = data.Adapt<Order>();
//            newOrder.Id = Constants.DateId16();
//            newOrder.Status = "1";
//            newOrder.Isused = "1";
//            newOrder.CreateDate = DateTime.Now;
//            #endregion  

//            #region New OrderItem
//            for (var i = 0; i < data.OrderItem.Count; i++)
//            {  
//                newOrder!.OrderItem!.Skip(i).First().Id = Constants.uuid24();
//                newOrder!.OrderItem!.Skip(i).First().OrderId = newOrder.Id;

//                var product = await context.Products.FirstOrDefaultAsync(a => a.Id.Equals(newOrder!.OrderItem!.Skip(i).First().ProductId));
//                if (product is null) return Constants.Return400("เกิดข้อผิดพลาด");
//                product.Stock -= newOrder!.OrderItem!.Skip(i).First().Amount;
//                context.Products.Update(product);
//                await cartItemService.DeleteCartItem(data.OrderItem!.Skip(i).First().IdCartItem);
//            }
//            #endregion 

//            await context.Orders.AddAsync(newOrder);
//            await context.SaveChangesAsync();
//            return Constants.Return200("บันทึกใบสั่งซื้อสำเร็จ");
//        }

//        public async Task<object> GetByAccountId(string status, string accountId, int pageSize)
//        {
//            var query = context.Orders
//                .Include(a => a.Address)
//                    .ThenInclude(a => a.Account)
//                .Include(a => a.OrderItem)
//                    .ThenInclude(a => a.Product)
//                        .ThenInclude(a => a.ProductImage)
//                .Where(a => a.Address.AccountId.Equals(accountId)).ToList();
//            if (!string.IsNullOrEmpty(status)) query = query.Where(a => a.Status.Equals(status)).ToList();
//            query = query.OrderByDescending(a => a.CreateDate).ToList();
//            int count = query.Count;
//            query = query.Take(pageSize).ToList();
//            return Constants.Return200PaginData("success", new { status, count, pageSize, totalPage = (int)Math.Ceiling((double)count / pageSize), }, query.Select(OrderResponse.OrdersProductOneImg));
//        }

//        public async Task<object> GetById(string id)
//        {
//            var result = await context.Orders
//                .Include(a => a.Address)
//                    .ThenInclude(a => a.Account)
//                .Include(a => a.OrderItem)
//                    .ThenInclude(a => a.Product)
//                        .ThenInclude(a => a.ProductImage)
//                .Include(a => a.OrderItem)
//                    .ThenInclude(a => a.Product)
//                        .ThenInclude(a => a.Category)
//                .Include(a => a.Payment)
//                .FirstOrDefaultAsync(a => a.Id.Equals(id));
//            if (result is null) return Constants.Return400("ไม่พบข้อมูล");
//            return Constants.Return200Data("success", OrderResponse.OrderItemsAccountAddressPaymentProductCateOneImg(result!));
//        }

//        public async Task<object> GetOrders(string status, int currentPage, int pageSize)
//        {
//            var query = context.Orders
//                .Include(e => e.OrderItem)
//                    .ThenInclude(e => e.Product)
//                        .ThenInclude(e => e.ProductImage).ToList();
//            // Status Query
//            if (!string.IsNullOrEmpty(status)) query = query.Where(a => a.Status.Equals(status)).ToList();
//            query = query.OrderByDescending(a => a.CreateDate).ToList();
//            // Count Data
//            int count = query.Count;
//            // Query Data
//            var data = query.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
//            return Constants.Return200PaginData("success", new
//            {
//                status,
//                currentPage,
//                pageSize,
//                count,
//                totalPage = (int)Math.Ceiling((double)count / pageSize),
//            }, data.Select(OrderResponse.OrdersProductOneImg));
//        }

//        public async Task<object> SuccessStatusOrder(string id)
//        {
//            var result = await context.Orders.FirstOrDefaultAsync(a => a.Id.Equals(id));
//            if (result is null) return Constants.Return400("ไม่พบข้อมูล");

//            result.Status = "4";
//            context.Orders.Update(result);
//            await context.SaveChangesAsync();
//            return Constants.Return200("บันทึกข้อมูลสำเร็จ");
//        }

//        private async Task<string> OrderCheckStock(List<OrderItemCreate> ItemList)
//        {
//            string errMsg = string.Empty;
//            foreach (var item in ItemList)
//            {
//                var result = await context.Products.FirstOrDefaultAsync(a => a.Id.Equals(item.ProductId));
//                if (result is null) errMsg = "เกิดข้อพิดพลาด";
//                else if (result.Stock < item.Amount) errMsg = "สินค้าหมด";
//            }
//            return errMsg;
//        }

//        private async Task<(string errorMessage, string imageName)> UpLoadImage(IFormFileCollection formFiles)
//        {
//            string errorMessage = string.Empty;
//            string imageName = string.Empty;
//            if (uploadFileService.IsUpload(formFiles))
//            {
//                errorMessage = uploadFileService.Validation(formFiles);
//                if (string.IsNullOrEmpty(errorMessage))
//                    imageName = (await uploadFileService.UploadImages(formFiles))[0];
//            }
//            return (errorMessage, imageName);
//        }
//    }
//}
