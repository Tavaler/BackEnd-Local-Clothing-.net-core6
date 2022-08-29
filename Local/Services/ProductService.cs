using Local.Interfaces;
using Microsoft.EntityFrameworkCore;
using Local.DTOS.Products;
using Local.Models;
using Local.Models.Data;
using Local.Settings;
using Mapster;

namespace Local.Services
{
    public class ProductService : IProductService
    {

        private readonly LocaldbContext context;
        private readonly IUploadFileService uploadFileService;
        private readonly IProductImgService productImgService;

        public ProductService(LocaldbContext localdbContext, IUploadFileService uploadFileService, IProductImgService productImgService)
        {
            this.context = localdbContext;
            this.uploadFileService = uploadFileService;
            this.productImgService = productImgService;
        }

        //public async Task<IEnumerable<Product>> GetProducts()
        public async Task<object> GetProducts(int currentPage, int pageSize, int categoryId, string search)
        {
            //var products = await context.Products.
            //Include(x => x.Category).Include(e => e.ProductImg)
            //.ToListAsync();
            //return products;
            var query = context.Products.Include(e => e.Category).Include(e => e.ProductImg)
                .Where(a => a.Isused.Equals("1"));

            // Category Check
            var category = await context.Categories.FirstOrDefaultAsync(a => a.CategoryId.Equals(categoryId));
            if (category is not null) query = query.Where(a => a.CategoryId.Equals(categoryId));
            // Query by CreateDate last
            query = query.OrderByDescending(a => a.ProcuctDate);
            // Search Check
            //if (!string.IsNullOrEmpty(search)) query = query.Where(a => a.ProductName.Contains(search) || a.Seed!.Contains(search));
            if (!string.IsNullOrEmpty(search)) query = query.Where(a => a.ProductName.Contains(search) );

            // Count Data
            int count = query.Count();
            // Query Data
            var data = query.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            return Constants.Return200PaginData("success", new
            {
                category = category is not null ? category.CategoryName : null,
                currentPage,
                pageSize,
                count,
                totalPage = (int)Math.Ceiling((double)count / pageSize),
                search
            }, data.Select(ProductResponse.ProductCateOneImage));

        }

        public async Task<Product> FindById(string id)
        {
            var data = await context.Products.Include(x => x.Category).Include(x => x.ProductImg).
                FirstOrDefaultAsync((x => x.ProductId.Equals(id)));
            if (data is null) return (Product)Constants.Return400("ไม่พบข้อมูล");
            return data;
        }
        
        //public async Task<Product> FindById1(int id)
        //{
        //    var data = await context.Products.Include(x => x.Category).
        //        FirstOrDefaultAsync((x => x.ProductId.Equals(id)));
        //    return data;
        //}

        public async Task<object> Create(ProductRequest data)
        {
            #region New Product
            var newProduct = data.Adapt<Product>();
            newProduct.ProductId = Constants.uuid24();
            //newProduct.ProductStock = 0;
            newProduct.ProcuctDate = DateTime.Now;
            newProduct.Isused = "1";
            await context.Products.AddAsync(newProduct);
            #endregion

            #region Check Image and UpLoadImage
            (string errorMessage, List<string> imageListName) = await UpLoadImage(data.upfile!);
            if (!string.IsNullOrEmpty(errorMessage)) return Constants.Return400(errorMessage);
            #endregion

            #region AddProductImage
            if (imageListName.Count > 0)
                foreach (var img in imageListName)
                    await context.ProductImgs.AddAsync(new ProductImg
                    {
                        ProductImgId = Constants.NewIdImage(),
                        ProductImgName = img,
                        CreateDate = DateTime.Now,
                        ProductId = newProduct.ProductId,
                    });
            #endregion

           

            await context.SaveChangesAsync();
            return Constants.Return200("บันทึกข้อมูลเร็จ");
            //await context.Products.AddAsync(data);
            //await context.SaveChangesAsync();


        }
        //public async Task Update(ProductUpdate data)
        public async Task<object> Update(ProductUpdate data)
        {
            var result = await context.Products.SingleOrDefaultAsync(a => a.ProductId.Equals(data.ProductId));
            if (result is null) return Constants.Return400("ไม่พบข้อมูล");

            data.Adapt(result);
            #region Check Image and UpLoadImage
            (string errorMessage, List<string> imageListName) = await UpLoadImage(data.upfile);
            if (!string.IsNullOrEmpty(errorMessage)) return Constants.Return400(errorMessage);
            #endregion

            #region AddProductImage
            if (imageListName.Count > 0)
                foreach (var img in imageListName)
                    await context.ProductImgs.AddAsync(new ProductImg
                    {
                        ProductImgId = Constants.NewIdImage(),
                        ProductImgName= img,
                        CreateDate = DateTime.Now,
                        ProductId = result.ProductId,
                    });
            #endregion

            context.Products.Update(result);
            await context.SaveChangesAsync();
            return Constants.Return200("อัพเดตข้อมูลสำเร็จ");
            //localdbContext.Products.Update(data);
            ////databaseContext.Products.Update(product);
            //await localdbContext.SaveChangesAsync();

        }

        public async Task Delete(Product product)
        {
            context.Products.Remove(product);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> Search(string name)
        {
            var data = await context.Products.Where(x => x.ProductName.Contains(name))
                .Include(x => x.Category).ToListAsync();

            return data;
        }

        public async Task<object> DeleteProduct(string id)
        {
            var result = await context.Products.FindAsync(id);
            if (result is null) return Constants.Return400("ไม่พบข้อมูล");

            result.Isused = "0";
            context.Products.Update(result);
            await context.SaveChangesAsync();
            return Constants.Return200("ลบข้อมูลสำเร็จ");
        }

        //public async Task<(string errorMessage, string imageName)> UploadImage(IFormFileCollection formFiles)
        //{
        //    string errorMessge = string.Empty;
        //    string imageName = string.Empty;

        //   if (uploadFileService.IsUpload(formFiles))
        //    { 
        //        errorMessge = uploadFileService.Validation(formFiles);
        //        if (string.IsNullOrEmpty(errorMessge))
        //        {
        //           imageName = (await uploadFileService.UploadImages(formFiles))[0];
        //        }
        //    }
        //    return (errorMessge,imageName);
        //}

        private async Task<(string errorMessage, List<string> imageListName)> UpLoadImage(IFormFileCollection formFiles)
        {
            string errorMessage = string.Empty;
            List<string> imageListName = new List<string>();
            if (uploadFileService.IsUpload(formFiles))
            {
                errorMessage = uploadFileService.Validation(formFiles);
                if (string.IsNullOrEmpty(errorMessage))
                    imageListName = await uploadFileService.UploadImages(formFiles);
            }
            return (errorMessage, imageListName);
        }

        //public async Task<object> GetBestSell(int num)
        //{
        //    var newList = new List<Product>();
        //    var query = context.Products
        //        .Include(a => a.Category)
        //        .Include(a => a.ProductImage)
        //        .Where(a => a.Isused.Equals("1") && a.StockSell != 0);
        //    query = query.OrderByDescending(a => a.StockSell);
        //    query = query.Take(num);
        //    return Constants.Return200Data("success", query.Select(ProductResponse.ProductCateOneImage));
        //}

        public async Task DeleteImage(string fileName)
        {
            await uploadFileService.DeleteImages(fileName);
        }

        public async Task<Product> FindById1(string id)
        {
            var data = await context.Products.Include(x => x.Category).
                FirstOrDefaultAsync((x => x.ProductId.Equals(id)));
            return data;
        }
    }
}
