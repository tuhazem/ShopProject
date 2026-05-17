﻿using AutoMapper;
using ClosedXML.Excel;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Application.Features.Orders;
using ShopProject.Application.Features.Products;
using ShopProject.Domain.Entities;
using ShopProject.Infrastructure.Implementations;

namespace ShopProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IUnitOfWork uow;

        private readonly IMapper mapper;

        public ActionController(IMediator _mediator, IExcelService _excelService, IUnitOfWork uow, IMapper mapper)
        {
            mediator = _mediator;
            ExcelService = _excelService;
            this.uow = uow;
            this.mapper = mapper;
        }

        public IExcelService ExcelService { get; }

        [HttpGet("download-inventory")]
        public async Task<IActionResult> DownloadInventory()
        {
            var products = await mediator.Send(new GetAllProductQuery());

            var fileContent = ExcelService.GenerateProductsReport(products);

            string fileName = $"Inventory_{DateTime.Now:yyyyMMdd}.xlsx";
            return File(
                fileContent,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileName
            );
        }

        [HttpGet("order-invoice/{orderId}")]
        public async Task<IActionResult> DownloadInvoice(int orderId)
        {
            var order = await uow.Orders.GetQueryable()
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null) return NotFound();

            var pdfFile = ExcelService.GeneratePdfInvoice(order);

            return File(pdfFile, "application/pdf", $"Invoice_{orderId}.pdf");
        }


    }
}
