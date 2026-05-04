using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Bank_ATM.Models;

namespace Bank_ATM.Repositories
{
    public class ServiceCategoryRepository
    {
        private readonly string _connectionString;

        public ServiceCategoryRepository()
        {
            _connectionString = Config.ConnectionString;
        }

        public IEnumerable<ServiceCategoryDto> GetActiveCategories()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<ServiceCategoryDto>(@"
                    SELECT
                        c.id as Id,
                        c.name as Name,
                        c.icon_emoji as IconEmoji,
                        c.sort_order as SortOrder,
                        c.is_active as IsActive,
                        c.created_at as CreatedAt,
                        (
                            SELECT COUNT(*)
                            FROM services s
                            WHERE s.category_id = c.id AND s.is_active = 1
                        ) as ServiceCount
                    FROM service_categories c
                    WHERE c.is_active = 1
                    ORDER BY c.sort_order, c.name").ToList();
            }
        }

        public IEnumerable<ServiceCategoryDto> GetAllCategories()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<ServiceCategoryDto>(@"
                    SELECT
                        c.id as Id,
                        c.name as Name,
                        c.icon_emoji as IconEmoji,
                        c.sort_order as SortOrder,
                        c.is_active as IsActive,
                        c.created_at as CreatedAt,
                        (
                            SELECT COUNT(*)
                            FROM services s
                            WHERE s.category_id = c.id AND s.is_active = 1
                        ) as ServiceCount
                    FROM service_categories c
                    ORDER BY c.sort_order, c.name").ToList();
            }
        }

        public ServiceCategoryDto GetCategoryById(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.QueryFirstOrDefault<ServiceCategoryDto>(@"
                    SELECT
                        id as Id,
                        name as Name,
                        icon_emoji as IconEmoji,
                        sort_order as SortOrder,
                        is_active as IsActive,
                        created_at as CreatedAt
                    FROM service_categories
                    WHERE id = @Id", new { Id = id });
            }
        }

        public int CreateCategory(ServiceCategoryDto category)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int id = db.QuerySingle<int>(@"
                    INSERT INTO service_categories (name, icon_emoji, sort_order, is_active)
                    VALUES (@Name, @IconEmoji, @SortOrder, @IsActive);
                    SELECT CAST(SCOPE_IDENTITY() as int);",
                    new
                    {
                        category.Name,
                        IconEmoji = category.IconEmoji ?? string.Empty,
                        category.SortOrder,
                        category.IsActive
                    });
                category.Id = id;
                return id;
            }
        }

        public void UpdateCategory(ServiceCategoryDto category)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Execute(@"
                    UPDATE service_categories
                    SET name = @Name,
                        icon_emoji = @IconEmoji,
                        sort_order = @SortOrder,
                        is_active = @IsActive
                    WHERE id = @Id",
                    new
                    {
                        category.Id,
                        category.Name,
                        IconEmoji = category.IconEmoji ?? string.Empty,
                        category.SortOrder,
                        category.IsActive
                    });
            }
        }

        public void DeleteCategory(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Execute("UPDATE service_categories SET is_active = 0 WHERE id = @Id", new { Id = id });
            }
        }
    }
}
