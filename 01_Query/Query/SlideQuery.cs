using System.Collections.Generic;
using System.Linq;
using _01_Query.Contracts.Slide;
using ShopManagement.Infrastructure.EfCore;

namespace _01_Query.Query
{
    public class SlideQuery : ISlideQuery
    {
        private readonly ShopContext _context;

        public SlideQuery(ShopContext context)
        {
            _context = context;
        }

        public List<SlideQueryModel> GetSlides()
        {
            return
                _context
                    .Sliders
                    .Where(x => x.IsRemoved == false)
                    .Select(x => new SlideQueryModel()
                    {
                        Picture = x.Picture,
                        Link = x.Link,
                        BtnText = x.BtnText,
                        Title = x.Title,
                        Heading = x.Heading,
                        PictureAlt = x.PictureAlt,
                        PictureTitle = x.PictureTitle,
                        Text = x.Text
                    })
                    .ToList();
        }
    }
}
