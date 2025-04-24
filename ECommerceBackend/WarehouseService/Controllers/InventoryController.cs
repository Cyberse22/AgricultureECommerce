using AutoMapper;
using WarehouseService.Repositories;
using Microsoft.AspNetCore.Mvc;
using WarehouseService.Data;
using WarehouseService.Models;
using WarehouseService.Services;

namespace WarehouseService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;
        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInventories()
        {
            var result = await _inventoryService.GetAllInventoriesAsync();
            return Ok(result);
        }

        [HttpGet("{inventoryId}")]
        public async Task<IActionResult> GetInventoryById(string inventoryId)
        {
            var result = await _inventoryService.GetInventoryByIdAsync(inventoryId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateInventory([FromBody] InventoryModel model)
        {
            var result = await _inventoryService.CreateInventoryAsync(model);
            return Ok(result);
        }

        [HttpPut("{inventoryId}")]
        public async Task<IActionResult> UpdateInventory(string inventoryId, [FromBody] InventoryModel model)
        {
            var result = await _inventoryService.UpdateInventoryAsync(inventoryId, model);
            return Ok(result);
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportTransaction([FromBody] TransactionModel model)
        {
            var result = await _inventoryService.ImportTransaction(model);
            return Ok(new { message = "Import successful", transactionId = result });
        }

        [HttpPost("export")]
        public async Task<IActionResult> ExportTransaction([FromBody] TransactionModel model)
        {
            var result = await _inventoryService.ExportTransaction(model);
            return Ok(new { message = "Export successful", transactionId = result });
        }

        [HttpGet("transactions/{transactionId}")]
        public async Task<IActionResult> GetTransactionById(string transactionId)
        {
            var result = await _inventoryService.GetTransactionByIdAsync(transactionId);
            return Ok(result);
        }

        [HttpGet("{inventoryId}/transactions")]
        public async Task<IActionResult> GetTransactionsByInventoryId(string inventoryId)
        {
            var result = await _inventoryService.GetTransactionsByInventoryIdAsync(inventoryId);
            return Ok(result);
        }
    }
}