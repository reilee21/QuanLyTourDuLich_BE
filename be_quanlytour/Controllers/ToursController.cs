﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using be_quanlytour.Models;

namespace be_quanlytour.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToursController : ControllerBase
    {
        private readonly QltourDuLichContext _context;

        public ToursController(QltourDuLichContext context)
        {
            _context = context;
        }

        // GET: api/Tours
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tour>>> GetTours()
        {
          if (_context.Tours == null)
          {
              return NotFound();
          }
            return await _context.Tours.ToListAsync();
        }

        // GET: api/Tours/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tour>> GetTour(string id)
        {
          if (_context.Tours == null)
          {
              return NotFound();
          }
            var tour = await _context.Tours.FindAsync(id);

            if (tour == null)
            {
                return NotFound();
            }

            return tour;
        }

        // PUT: api/Tours/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTour(string id, Tour tour)
        {
            if (id != tour.MaTour)
            {
                return BadRequest();
            }

            _context.Entry(tour).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TourExists(id))
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

        // POST: api/Tours
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tour>> PostTour(Tour tour)
        {
          if (_context.Tours == null)
          {
              return Problem("Entity set 'QltourDuLichContext.Tours'  is null.");
          }
            _context.Tours.Add(tour);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TourExists(tour.MaTour))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTour", new { id = tour.MaTour }, tour);
        }

        // DELETE: api/Tours/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTour(string id)
        {
            if (_context.Tours == null)
            {
                return NotFound();
            }
            var tour = await _context.Tours.FindAsync(id);
            if (tour == null)
            {
                return NotFound();
            }

            _context.Tours.Remove(tour);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TourExists(string id)
        {
            return (_context.Tours?.Any(e => e.MaTour == id)).GetValueOrDefault();
        }
    }
}
