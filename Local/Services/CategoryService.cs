using Local.Settings;
using Local.Interfaces;
using Local.Models;
using Local.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace Local.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly LocaldbContext context;
        public CategoryService(LocaldbContext context) => this.context = context;

        public async Task<object> CreateCategory(string id,string name)
        {
            await context.Categories.AddAsync(new Category
            {
                CategoryId = id,
                CategoryName = name,
                //CreatedDate = DateTime.Now,
                Isused = "1"
            });
            await context.SaveChangesAsync();
            return Constants.Return200("บันทึกข้อมูลสำเร็จ");
        }

        public async Task<object> DeleteCategory(string id)
        {
            var result = await context.Categories.FindAsync(id);
            if (result is null) return Constants.Return400("ไม่พบข้อมูล");

            result.Isused = "0";
            context.Categories.Update(result);
            //context.Categories.Remove(result);
            await context.SaveChangesAsync();
            return Constants.Return200("ลบข้อมูลสำเร็จ");
        }

        public async Task<object> Getcategory() =>
            await context.Categories.Where(a => a.Isused.Equals("1")).Select(a => new { 
                a.CategoryId,
                a.CategoryName,
                //a.CreatedDate
            }).ToListAsync();

        public async Task<object> UpdateCategory(string id, string name)
        {
            var result = await context.Categories.FindAsync(id);
            if (result is null) return Constants.Return400("ไม่พบข้อมูล");

            result.CategoryName = name;
            context.Categories.Update(result);
            await context.SaveChangesAsync();
            return Constants.Return200("อัพเดตข้อมูลสำเร็จ");
        }
    }
}
