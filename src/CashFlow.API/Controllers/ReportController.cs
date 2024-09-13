﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace CashFlow.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    [HttpGet("excel")]
    [ProducesResponseType(typeof(FileContentResult),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetExcel([FromHeader] DateOnly month)
    {
        byte [] file = new byte[1];

        if(file.Length > 0) return File(file, MediaTypeNames.Application.Octet, "report.xlsx");

        return NoContent();
    }
}
