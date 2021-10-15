using System.Collections.Generic;
using _0_Framework.Application;

namespace ShopManagement.Application.contracts.Slider
{
    public interface ISliderApplication
    {
        public OperationResult Create(CreateSlider command);

        public OperationResult Edit(EditSlider command);

        public OperationResult Remove(long id);

        public OperationResult Restore(long id);

        public EditSlider GetDetails(long id);

        public List<SliderViewModel> GetList();
    }
}
