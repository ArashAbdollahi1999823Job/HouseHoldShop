﻿using System;
using System.Collections.Generic;
using _0_Framework.Application;
using ShopManagement.Application.contracts.Slider;
using ShopManagement.Domain.SliderAgg;

namespace ShopManagement.Application
{
    public class SliderApplication : ISliderApplication
    {
        private readonly ISliderRepository _sliderRepository;

        public SliderApplication(ISliderRepository sliderRepository)
        {
            _sliderRepository = sliderRepository;
        }

        public OperationResult Create(CreateSlider command)
        {
            var operation = new OperationResult();

            var slider = new Slider(command.Picture, command.PictureAlt, command.PictureTitle
            , command.Heading, command.Title, command.Text, command.Link,command.BtnText);

            _sliderRepository.Create(slider);
            _sliderRepository.SaveChanges();

            return operation.IsSuccess();

        }

        public OperationResult Edit(EditSlider command)
        {
            var operation = new OperationResult();

            var slider = _sliderRepository.GetBy(command.Id);

            if (slider == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            else
            {
                slider.Edit(command.Picture, command.PictureAlt, command.PictureTitle
                    , command.Heading, command.Title, command.Text,command.Link, command.BtnText);

                _sliderRepository.SaveChanges();
                return operation.IsSuccess();
            }



        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();

            var slider = _sliderRepository.GetBy(id);

            if (slider == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            else
            {
                slider.Remove();

                _sliderRepository.SaveChanges();
                return operation.IsSuccess();
            }
        }

        public OperationResult Restore(long id)
        {

            var operation = new OperationResult();

            var slider = _sliderRepository.GetBy(id);

            if (slider == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            else
            {
                slider.Restore();

                _sliderRepository.SaveChanges();
                return operation.IsSuccess();
            }
        }

        public EditSlider GetDetails(long id)
        {
            return _sliderRepository.GetDetails(id);
        }


        public List<SliderViewModel> GetList()
        {
            return _sliderRepository.GetList();
        }
    }
}
