namespace Local.Interfaces
{
    public interface ICategoryService
    {
        Task<object> Getcategory();
        Task<object> CreateCategory(string id,string name); 
        Task<object> UpdateCategory(string id,string name);
        Task<object> DeleteCategory(string id);
    }
}
