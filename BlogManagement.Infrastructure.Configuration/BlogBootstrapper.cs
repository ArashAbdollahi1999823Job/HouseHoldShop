using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogManagement.Application;
using BlogManagement.Application.Contract.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;
using BlogManagement.Infrastructure.EFCore;
using BlogManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlogManagement.Infrastructure.Configuration
{
    public  class BlogBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IArticleCategoryApplication, ArticleCategoryApplication>();
            services.AddTransient<IArticleCategoryRepository, ArticleCategoryRepository>();

            services.AddDbContext<BlogContext>(x => x.UseSqlServer(connectionString));

        }
    }
}
