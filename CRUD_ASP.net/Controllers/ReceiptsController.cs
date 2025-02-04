using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUD_ASP.net.Data;
using CRUD_ASP.net.Models;
using System.Text.Json;
using CRUD_ASP.net.ViewModels;
using NuGet.Protocol;

namespace CRUD_ASP.net.Controllers
{
    public class ReceiptsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ReceiptsController> _logger;

        public ReceiptsController(ApplicationDbContext context, ILogger<ReceiptsController> logger)
        {
            _logger = logger;
            _context = context;
        }

        // GET: Receipts
        public async Task<IActionResult> Index()
        {

            var listModel = await _context.Receipts
                                    .GroupJoin(
                                        _context.ReceiptDetails,
                                        receipt => receipt.Id,
                                        receiptDetail => receiptDetail.ReceiptId,
                                        (receipt, details) => new
                                        {
                                            Receipt = receipt,
                                            Counted = details.Count()
                                        }
                                    ).ToListAsync();

            var viewModel = listModel.Select(r => new
            {
                r.Receipt.Id,
                r.Receipt.TypeReceipt,
                r.Receipt.ReceiptNumber,
                r.Receipt.Observations,
                r.Receipt.DateEmision,
                r.Receipt.AmountTotal,
                r.Counted // Número de detalles
            }).ToList();

            return View(viewModel);
        }

        // GET: Receipts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receipt = await _context.Receipts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receipt == null)
            {
                return NotFound();
            }

            var ReceiptDetails = await _context.ReceiptDetails.Where(rd => rd.ReceiptId == id).ToListAsync();
            if (receipt == null)
            {
                return NotFound();
            }

            var viewModel = new ReceiptViewModel
            {
                Receipt = receipt,
                ReceiptDetails = ReceiptDetails
            };

            return View(viewModel);
        }

        // GET: Receipts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Receipts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Receipt,ReceiptDetails")] ReceiptViewModel receiptViewModel)
        {
            _context.Receipts.Add(receiptViewModel.Receipt);
            await _context.SaveChangesAsync();

            //var lDetails = JsonSerializer.Deserialize<List<ReceiptDetail>>(receiptViewModel.Receipt);

            try
            {

                foreach (var item in receiptViewModel.ReceiptDetails)
                {
                    item.ReceiptId = receiptViewModel.Receipt.Id;
                    _context.ReceiptDetails.Add(item);
                    await _context.SaveChangesAsync();

                }
                return RedirectToAction("Index");

            }
            catch
            {

                return View(receiptViewModel);
            }

        }

        // GET: Receipts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receipt = await _context.Receipts.FindAsync(id);
            var ReceiptDetails = await _context.ReceiptDetails.Where(rd => rd.Receipt.Id == id).ToListAsync();
            if (receipt == null)
            {
                return NotFound();
            }

            var viewModel = new ReceiptViewModel
            {
                Receipt = receipt,
                ReceiptDetails = ReceiptDetails
            };

            return View(viewModel);
        }

        // POST: Receipts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Receipt,ReceiptDetails")] ReceiptViewModel receiptViewModel)
        {
            _logger.LogInformation("datas");
            _logger.LogInformation("ReceiptViewModel JSON: {JsonData}", receiptViewModel.ToJson());
            if (id != receiptViewModel.Receipt.Id)
            {
                return NotFound();
            }

            try
            {
                _context.Receipts.Update(receiptViewModel.Receipt);
                await _context.SaveChangesAsync();


                // Bulk update/delete/insert for receipt details
                var existingDetails = _context.ReceiptDetails.Where(rd => rd.ReceiptId == id);
                _context.ReceiptDetails.RemoveRange(existingDetails);
                _logger.LogInformation("datas 2");

                foreach (var item in receiptViewModel.ReceiptDetails)
                {
                    item.ReceiptId = receiptViewModel.Receipt.Id;
                    _context.ReceiptDetails.Add(item);
                }
                _logger.LogInformation("datas 3");

                await _context.SaveChangesAsync();
                _logger.LogInformation("datas 4");

                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReceiptExists(receiptViewModel.Receipt.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return View(receiptViewModel);
        }

        // GET: Receipts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receipt = await _context.Receipts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receipt == null)
            {
                return NotFound();
            }

            return View(receipt);
        }

        // POST: Receipts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var receipt = await _context.Receipts.FindAsync(id);
            if (receipt != null)
            {
                _context.Receipts.Remove(receipt);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceiptExists(int id)
        {
            return _context.Receipts.Any(e => e.Id == id);
        }
    }
}
