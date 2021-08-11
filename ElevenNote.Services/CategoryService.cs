using ElevenNote.Data;
using ElevenNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services
{
    public class CategoryService
    {
        private readonly Guid _userId;
        public CategoryService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateCategory (CategoryCreate model)
        {
            var entity =
                new Category()
                {
                    OwnerId = _userId,
                    Name = model.Name,
                    CreatedUtc = DateTimeOffset.Now
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Categories.Add(entity);
                return ctx.SaveChanges() == 1;
            }          
        }
        public IEnumerable<CategoryListItem> GetCategory()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Categories
                    .Where(e => e.OwnerId == _userId)
                    .Select(
                        e =>
                        new CategoryListItem
                        {
                            CategoryID =e.CategoryID,
                            Name = e.Name,                            
                        }
                   );
                return query.ToArray();
            }
        }
        public CategoryDetail GetCategoryById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Categories
                        .Single(e => e.CategoryID == id && e.OwnerId == _userId);
                return
                    new CategoryDetail
                    {
                        Name= entity.Name,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc,
                    };
            }
        }
        public bool UpdateCategory(CategoryEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Categories
                        .Single(e => e.CategoryID == model.CategoryID && e.OwnerId == _userId);
                entity.Name = model.Name;               
                entity.ModifiedUtc = DateTimeOffset.UtcNow;
                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteCategory(int categoryId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Categories
                        .Single(e => e.CategoryID == categoryId && e.OwnerId == _userId);
                ctx.Categories.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
