using System.Text.Json;

namespace Blog_Management_API.Model
{
    public class Repository
    {
        private readonly string _jsonFilePath;

        public Repository(IWebHostEnvironment webHostEnvironment)
        {
            _jsonFilePath = Path.Combine(webHostEnvironment.WebRootPath, "blogs.json");
        }



        public async Task<List<Blogs>> GetAllBlogsAsync()
        {
            if (!File.Exists(_jsonFilePath))
            {
                return new List<Blogs>();
            }

            var json = await File.ReadAllTextAsync(_jsonFilePath);
            return JsonSerializer.Deserialize<List<Blogs>>(json);
        }
        public async Task<Blogs> GetBlogsByIdAsync(int id)
        {
            var blogs = await GetAllBlogsAsync();
            return blogs.FirstOrDefault(c => c.Id == id);
        }
        public async Task AddBlogsAsync(Blogs blog)
        {
            var blogs = await GetAllBlogsAsync();
            blog.Id = blogs.Any() ? blogs.Max(e => e.Id) + 1 : 1;
            blogs.Add(blog);
            await SaveBlogsAsync(blogs);
        }
        private async Task SaveBlogsAsync(List<Blogs> blogs)
        {
            var json = JsonSerializer.Serialize(blogs, new JsonSerializerOptions { WriteIndented = true });

            try
            {
                await File.WriteAllTextAsync(_jsonFilePath, json);
                Console.WriteLine("Blogs saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving Blogs: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateBlogsAsync(int id, Blogs updatedBlog)
        {
            var blogs = await GetAllBlogsAsync();
            var cI = blogs.FindIndex(e => e.Id == id);
            if (cI == -1)
            {
                throw new Exception("Blog not found");
            }
            updatedBlog.Id = id;
            blogs[cI] = updatedBlog;
            await SaveBlogsAsync(blogs);
        }
        public async Task DeleteBlogAsync(int id)
        {
            var blogs = await GetAllBlogsAsync();
            var blog = blogs.FirstOrDefault(e => e.Id == id);
            if (blog == null)
            {
                throw new Exception("Blog not found");
            }
            blogs.Remove(blog);
            await SaveBlogsAsync(blogs);
            
        }
    }
}
