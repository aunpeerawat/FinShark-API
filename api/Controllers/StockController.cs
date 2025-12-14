using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.DTOs.Stock;
using api.Models;
using api.Mappers;      
using api.Interfaces;       
using api.Helper;
using Microsoft.AspNetCore.Authorization;

namespace api.Controllers
{
    [Authorize]
    [Route("api/stock")]
    [ApiController]
    public class StockController: ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepo;
        public StockController(ApplicationDBContext context, IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid) // ModelState comes from ControllerBase
                return BadRequest(ModelState);
            // var stocks = await _context.Stock.ToListAsync();
            var stocks = await _stockRepo.GetAllAsync(query);
            var stockDto = stocks.Select(s => s.ToStockDto());
            return Ok(stockDto);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid) // ModelState comes from ControllerBase
                return BadRequest(ModelState);
            // var stock = await _context.Stock.FindAsync(id);
            // Find is better than firstordefault if search by id

            var stock = await _stockRepo.GetByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            if (!ModelState.IsValid) // ModelState comes from ControllerBase
                return BadRequest(ModelState);
            var stockModel = stockDto.ToStockFromCreateDTO();
            // await _context.Stock.AddAsync(stockModel);
            // await _context.SaveChangesAsync();
            await _stockRepo.CreateAsync(stockModel);
            return CreatedAtAction(nameof(GetById), new { Id = stockModel.Id }, stockModel.ToStockDto());
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            if (!ModelState.IsValid) // ModelState comes from ControllerBase
                return BadRequest(ModelState);
            // var stockModel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
            var stockModel = await _stockRepo.UpdateAsync(id,updateDto);
            if (stockModel == null)
            {
                return NotFound();
            }
            // stockModel.Symbol = updateDto.Symbol;
            // stockModel.CompanyName = updateDto.CompanyName;
            // stockModel.Purchase = updateDto.Purchase;
            // stockModel.LastDiv = updateDto.LastDiv;
            // stockModel.Industry = updateDto.Industry;
            // stockModel.MarketCap = updateDto.MarketCap;
            // await _context.SaveChangesAsync();
            return Ok(stockModel.ToStockDto());
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid) // ModelState comes from ControllerBase
                return BadRequest(ModelState);
            // var stockModel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
            var stockModel = await _stockRepo.DeleteAsync(id);
            if (stockModel == null)
            {
                return NotFound();
            }
            // _context.Stock.Remove(stockModel); // Don't add the async to delete because delete is not the async function
            // await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}