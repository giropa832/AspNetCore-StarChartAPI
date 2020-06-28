﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using StarChart.Data;
using StarChart.Models;

namespace StarChart.Controllers
{
    [ApiController]
    [Route("")]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("{id:int}",Name = "GetById")]
        public IActionResult GetById (int id)
        {
            var celestialObject = _context.CelestialObjects.FirstOrDefault(c => c.Id == id);
            if (celestialObject == null)
            {
                return NotFound();
            }
            celestialObject.Satellites = _context.CelestialObjects.Where(c => c.OrbitedObjectId == id).ToList();
            return Ok(celestialObject);
        }
        [HttpGet("{name}", Name = "GetByName")]
        public IActionResult GetByName(string name)
        {
            var celestialObjects = _context.CelestialObjects.Where(c => c.Name == name);
            if (celestialObjects.Count() == 0)
                return NotFound();
            foreach (var celestialObject in celestialObjects)
                celestialObject.Satellites = _context.CelestialObjects.Where(c => c.OrbitedObjectId == celestialObject.Id).ToList();
            return Ok(celestialObjects);
        }

    }
}
