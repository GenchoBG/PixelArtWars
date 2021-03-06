﻿using System.Collections.Generic;
using AutoMapper;
using PixelArtWars.Data.Models;
using PixelArtWars.Web.Areas.Admin.Models.DrawingViewModels;
using PixelArtWars.Web.Infrastructure.Mapping;

namespace PixelArtWars.Web.Areas.Admin.Models.ReportViewModels
{
    public class ReportDetailsViewModel : IHaveCustomMapping
    {
        public int Id { get; set; }

        public string ReporterId { get; set; }

        public IEnumerable<DrawingInReportViewModel> Drawings { get; set; }

        public void ConfigureMapping(Profile profile)
            => profile
                .CreateMap<Report, ReportDetailsViewModel>()
                .ForMember(m => m.Drawings, opts => opts.MapFrom(r => r.Game.Players));
    }
}
