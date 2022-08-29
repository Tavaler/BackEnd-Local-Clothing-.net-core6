using Local.Models;
using Local.Settings;

namespace Local.DTOS.Products
{
    //public class ProductResponse
    //{
    //    public string ProductId { get; set; }
    //    public string ProductName { get; set; }
    //    public string ProductDetail { get; set; }
    //    public string? ProductImg { get; set; }
    //    public int ProductStock { get; set; }
    //    public int ProductPrice { get; set; }
    //    public DateTime ProductDate { get; set; } = DateTime.Now;

    //    public string CategoryName { get; set; }
    //    //public string LocalName { get; set; }

    //    static public ProductResponse FromProduct(Product product)
    //    {
    //        return new ProductResponse
    //        {
    //            ProductId = product.ProductId,
    //            ProductName = product.ProductName,
    //            //ProductImg = product.ProductImg.,
    //            ProductImg = product.ProductImg.Count > 0 ? Constants.PathServer + product.ProductImg.First().ProductImgName : null,
    //            ProductStock = product.ProductStock,
    //            ProductPrice = product.ProductPrice,
    //            ProductDetail = product.ProductDetail,
    //            //Cal = product.Price*product.Stock,
    //            CategoryName = product.Category.CategoryName
                
    //        };
    //    }

    //    //static public object ProductCateImages(Product data) => new
    //    //{
    //    //    ProductId = data.ProductId,
    //    //    ProductName =data.ProductName,
    //    //    ProductPrice =data.ProductPrice,
    //    //    ProductStock =data.ProductStock,
    //    //    ProductDetail=data.ProductDetail,
    //    //    ProductDate = data.ProcuctDate,
    //    //    Category=data.CategoryId,
    //    //    CategoryName = data.Category.CategoryName,
    //    //    ProductImg = data.ProductImg.Count > 0 ? data.ProductImg.Select(a => new { a.ProductImgId, img = Constants.PathServer + a.ProductImgName }) : null,
    //    //};
    //    //public int ProductId { get; set; }
    //    //public string ProductName { get; set; }
    //    //public string ProductDetail { get; set; }
    //    //public int? ProductPrice { get; set; }
    //    //public int? ProcuctStock { get; set; }
    //    //public DateTime ProcuctDate { get; set; } = DateTime.Now;
    //    //public string? ProductImage { get; set; }

    //    //public string CategoryId { get; set; }
    //    //[ForeignKey("CategoryId")]
    //    //[ValidateNever]
    //    //internal Category Category { get; set; }

    //    //public string LocalId { get; set; }
    //    //[ForeignKey("LocalId")]
    //    //[ValidateNever]
    //    //internal LocalProduct LocalProducrt { get; set; }
    //}

    public class ProductResponse
    {
        static public object ProductCateOneImageForItemCart(Product data) => new
        {
            data.ProductId,
            data.ProductName,
            data.CategoryId,
            data.ProductPrice,
            CategoryName = data.Category.CategoryName,
            ProductImage = data.ProductImg.Count > 0 ? Constants.PathServer + data.ProductImg.First().ProductImgName : null,
        };

        static public object ProductCateImages(Product data) => new
        {
            data.ProductId,
            data.ProductName,
            data.ProductPrice,
            data.ProductStock,
            //data.,
            data.ProductDetail,
            //data.,
            //data.level,
            data.ProcuctDate,
            data.CategoryId,
            CategoryName = data.Category.CategoryName,
            ProductImages = data.ProductImg.Count > 0 ? data.ProductImg.Select(a => new { a.ProductId, img = Constants.PathServer + a.ProductImgName }) : null,
        };

        static public object ProductCateOneImage(Product data) => new
        {
            data.ProductId,
            data.ProductName,
            data.ProductPrice,
            data.ProductStock,
            //data.StockSell,
            data.ProductDetail,
            //data.Seed,
            //data.level,
            data.ProcuctDate,
            data.CategoryId,
            CategoryName = data.Category.CategoryName,
            ProductImage = data.ProductImg.Count > 0 ? Constants.PathServer + data.ProductImg.First().ProductImgName : null,
        };

    }
}
