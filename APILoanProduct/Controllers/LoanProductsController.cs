using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APILoanProduct.Models;
using APILoanProduct.Data;

namespace APILoanProduct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanProductsController : ControllerBase
    {
        private readonly context _context;

        public LoanProductsController(context context)
        {
            _context = context;
        }

        // GET: api/LoanProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoanProduct>>> GetLoanProducts()
        {
            return await _context.LoanProducts.ToListAsync();
        }

        // GET: api/LoanProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LoanProduct>> GetLoanProduct(string id)
        {
            var loanProduct = await _context.LoanProducts.FindAsync(id);

            if (loanProduct == null)
            {
                return NotFound();
            }

            return loanProduct;
        }

        // PUT: api/LoanProducts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoanProduct(string id, LoanProduct loanProduct)
        {
            if (id != loanProduct.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(loanProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoanProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/LoanProducts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LoanProduct>> PostLoanProduct(LoanProduct loanProduct)
        {
            _context.LoanProducts.Add(loanProduct);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LoanProductExists(loanProduct.ProductId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLoanProduct", new { id = loanProduct.ProductId }, loanProduct);
        }

        // DELETE: api/LoanProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoanProduct(string id)
        {
            var loanProduct = await _context.LoanProducts.FindAsync(id);
            if (loanProduct == null)
            {
                return NotFound();
            }

            _context.LoanProducts.Remove(loanProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LoanProductExists(string id)
        {
            return _context.LoanProducts.Any(e => e.ProductId == id);
        }
    }
}
