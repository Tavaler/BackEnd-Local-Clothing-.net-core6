//using Local.DTOS.Account;
//using Local.DTOS.Addresses;
//using Local.DTOS.OrderItems;
//using Local.DTOS.Payments;
//using Local.Settings;
//using Local.Models;

//namespace Local.DTOS.Orders
//{
//    public class OrderResponse
//    {
//        static public object OrdersProductOneImg(Order data) => new
//        {
//            data.Id,
//            data.Status,
//            data.Total,
//            data.CreateDate,
//            data.TransportationCode,
//            ItemCount = data.OrderItem.Count,
//            ProductName = data.OrderItem.First().Product.ProductName,
//            ProductImage = data.OrderItem.Count > 0
//                ? data.OrderItem.First().Product.ProductImg.Count > 0 
//                    ? Constants.PathServer + data.OrderItem.First().Product.ProductImg.First().ProductImgName
//                    : null
//                : null, 
//        };

//        static public object OrderItemsAddressPaymentProductOneImg(Order data) => new
//        {
//            data.Id,
//            data.Status,
//            data.Total,
//            data.CreateDate,
//            data.TransportationCode,
//            data.Address,
//            Payment = data.Payment.Count > 0 
//                ? data.Payment.Select(PaymentResponse.Payment)
//                : null,
//            OrderItem = data.OrderItem.Count > 0
//                ? data.OrderItem.Select(OrderItemResponse.OrderItemsProductOneImg) 
//                : null,
//        };

//        static public object OrderItemsAccountAddressPaymentProductOneImg(Order data) => new
//        {
//            data.Id,
//            data.Status,
//            data.Total,
//            data.CreateDate,
//            data.Address,
//            data.TransportationCode,
//            User = UserResponse.User(data.Address),
//            //Address = AddressResponse.AddressRes(data.Address),
//            Payment = data.Payment.Count > 0
//                ? data.Payment.Select(PaymentResponse.Payment)
//                : null,
//            OrderItem = data.OrderItem.Count > 0
//                ? data.OrderItem.Select(OrderItemResponse.OrderItemsProductOneImg)
//                : null,
//        };

//        static public object OrderItemsAccountAddressPaymentProductCateOneImg(Order data) => new
//        {
//            data.Id,
//            data.Status,
//            data.Total,
//            data.CreateDate,
//            data.TransportationCode,
//            Account = AccountResponse.Account(data.Address.Account),
//            Address = AddressResponse.AddressRes(data.Address),
//            Payment = data.Payment.Count > 0
//                ? data.Payment.Select(PaymentResponse.Payment)
//                : null,
//            OrderItem = data.OrderItem.Count > 0
//                ? data.OrderItem.Select(OrderItemResponse.OrderItemsProductCateOneImg)
//                : null,
//        };
//    }
//}
