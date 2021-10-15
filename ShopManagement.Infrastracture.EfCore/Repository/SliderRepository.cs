using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.contracts.Slider;
using ShopManagement.Domain.SliderAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class SliderRepository:RepositoryBase<long,Slider>, ISliderRepository
    {
        private readonly ShopContext _context;
        public SliderRepository(ShopContext context) : base(context)
        {
            _context = context;
        }
        public EditSlider GetDetails(long id)
        {
            return _context.Sliders.Select(x=>new EditSlider()
            {
                Id = x.Id,
                BtnText = x.BtnText,
                Heading = x.Heading,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Text = x.Text,
                Link = x.Link,
                Title = x.Title
            }).FirstOrDefault(x=>x.Id==id);
        }

        public List<SliderViewModel> GetList()
        {
            return _context.Sliders
                .Select(x => new SliderViewModel()
                {
                    Id = x.Id,
                    Heading = x.Heading,
                    Picture = x.Picture,
                    Title = x.Title,
                    IsRemoved = x.IsRemoved,
                    CreationDate=x.CreationDate.ToFarsi()
                })
                .OrderByDescending(x => x.Id)
                .ToList();
        }
    }
}
