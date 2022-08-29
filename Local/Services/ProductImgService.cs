
using Local.Interfaces;
using Local.Models.Data;
using Local.Settings;

namespace Local.Services
{
    internal class ProductImgService : IProductImgService
    {
        private readonly IUploadFileService uploadFileService;
        private readonly LocaldbContext context;
        public ProductImgService(IUploadFileService uploadFileService, LocaldbContext context)
        {
            this.uploadFileService = uploadFileService;
            this.context = context;
        }

        public async Task<object> Delete(string id)
        {
            var result = await context.ProductImgs.FindAsync(id);
            if (result is null) return Constants.Return400("ไม่พบข้อมูล");

            await uploadFileService.DeleteImages(result.ProductImgName);
            context.ProductImgs.Remove(result);
            await context.SaveChangesAsync();
            return Constants.Return200("ลบสำเร็จ");
        }
    }
}