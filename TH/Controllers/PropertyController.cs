﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TH.Data;
using TH.Models;
using TH.Classes;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace TH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly AppDBContext _context;

        public PropertyController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet]        
        public async Task<IActionResult> GetPropertiesAsync()
        {
            try
            {
                List<Property> listProperties = await _context.Properties.ToListAsync();
                return Ok(listProperties);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetPropertyByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                Property property = await _context.Properties.FirstOrDefaultAsync(p => p.id == id);

                if(property == null)
                {
                    return NotFound(id);
                }

                return Ok(property);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpPut]
        public async Task<IActionResult> AddProperty(Property property)
        {
            try
            {
                property.status = "Active";

                if(!property.IsValid())
                {
                    return BadRequest(property);
                }

                _context.Properties.Add(property);
                await _context.SaveChangesAsync();

                return Ok(property);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DisableProperty(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound(id);
                }

                Property property = await _context.Properties.FirstOrDefaultAsync(p => p.id == id);
                if (property == null)
                {
                    return NotFound(id);
                }

                if(property.IsDisabled())
                {
                    return Accepted(id);
                }

                property.status = "Disable";
                property.disabled_at = DateTime.Now;

                _context.Attach(property).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Accepted(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

    }
}
