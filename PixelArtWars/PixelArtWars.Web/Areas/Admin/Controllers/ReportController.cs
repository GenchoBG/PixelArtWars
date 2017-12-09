using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using PixelArtWars.Services.Interfaces;
using PixelArtWars.Web.Areas.Admin.Models.ReportViewModels;

namespace PixelArtWars.Web.Areas.Admin.Controllers
{
    public class ReportController : AdminBaseController
    {
        private const int ReportsPerPage = 50;

        private readonly IReportService reportService;
        private readonly IMapper mapper;

        public ReportController(IReportService reportService, IMapper mapper)
        {
            this.reportService = reportService;
            this.mapper = mapper;
        }

        public IActionResult All(int page = 1)
        {
            if (page < 1)
            {
                page = 1;
            }
            this.ViewData["page"] = page;

            var reports = this.reportService
                .All()
                .Skip((page - 1) * ReportsPerPage)
                .Take(ReportsPerPage)
                .ProjectTo<ReportListViewModel>()
                .ToList();

            return this.View(reports);
        }

        public IActionResult Details(int id)
        {
            var report = this.reportService.Get(id);

            var model = this.mapper.Map<ReportDetailsViewModel>(report);

            return this.View(model);
        }
    }
}
