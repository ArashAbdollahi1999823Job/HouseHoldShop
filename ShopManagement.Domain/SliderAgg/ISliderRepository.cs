using System.Collections.Generic;
using _0_Framework.Domain;
using ShopManagement.Application.contracts.Slider;

namespace ShopManagement.Domain.SliderAgg
{
     public interface ISliderRepository:IRepository<long,Slider>
     {
         EditSlider GetDetails(long id);

         List<SliderViewModel> GetList();

     }
}
