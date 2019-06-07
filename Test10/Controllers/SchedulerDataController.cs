using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Test10.Data;

namespace Test10.Models.Controllers
{
    [Route("api/[controller]/[action]")]
    public class SchedulerDataController : Controller
    {
        private ApplicationDbContext _context;

        public SchedulerDataController(ApplicationDbContext context) {
            this._context = context;
        }

        [HttpGet]
        public IActionResult Get(DataSourceLoadOptions loadOptions) {
            // var tereni = _context.Teren.Where(i => i.TeniskiKlubId == id).Select(i =>i.IdTeren).ToList();
            //  var rezervacija = _context.Rezervacija.Where(i=>tereni.Contains(i.TerenId)).Select(i => new {
            var rezervacija = _context.Rezervacija.Select(i => new {
                i.IdRezervacija,
                i.DatumVrijeme,
                i.Kraj,
                i.TerenId,
                i.UpraviteljId,
                i.IgracId
            });
            return Json(DataSourceLoader.Load(rezervacija, loadOptions));
        }

        [HttpPost]
        public IActionResult Post(string values) {
            var model = new Rezervacija();
            var _values = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, _values);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Rezervacija.Add(model);
            _context.SaveChanges();

            return Json(result.Entity.IdRezervacija);
        }

        [HttpPut]
        public IActionResult Put(int key, string values) {
            var model = _context.Rezervacija.FirstOrDefault(item => item.IdRezervacija == key);
            if(model == null)
                return StatusCode(409, "Rezervacija not found");

            var _values = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, _values);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public void Delete(int key) {
            var model = _context.Rezervacija.FirstOrDefault(item => item.IdRezervacija == key);

            _context.Rezervacija.Remove(model);
            _context.SaveChanges();
        }


        [HttpGet]
        public IActionResult TerenLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Teren
                         orderby i.NazivTerena
                         select new {
                             Value = i.IdTeren,
                             Text = i.NazivTerena
                         };
            return Json(DataSourceLoader.Load(lookup, loadOptions));
        }

        private void PopulateModel(Rezervacija model, IDictionary values) {
            string ID_REZERVACIJA = nameof(Rezervacija.IdRezervacija);
            string DATUM_VRIJEME = nameof(Rezervacija.DatumVrijeme);
            string KRAJ = nameof(Rezervacija.Kraj);
            string TEREN_ID = nameof(Rezervacija.TerenId);
            string UPRAVITELJ_ID = nameof(Rezervacija.UpraviteljId);
            string IGRAC_ID = nameof(Rezervacija.IgracId);

            if(values.Contains(ID_REZERVACIJA)) {
                model.IdRezervacija = Convert.ToInt32(values[ID_REZERVACIJA]);
            }

            if(values.Contains(DATUM_VRIJEME)) {
                model.DatumVrijeme = Convert.ToDateTime(values[DATUM_VRIJEME]);
            }

            if(values.Contains(KRAJ)) {
                model.Kraj = Convert.ToDateTime(values[KRAJ]);
            }

            if(values.Contains(TEREN_ID)) {
                model.TerenId = Convert.ToInt32(values[TEREN_ID]);
            }

            if(values.Contains(UPRAVITELJ_ID)) {
                model.UpraviteljId = Convert.ToString(values[UPRAVITELJ_ID]);
            }

            if(values.Contains(IGRAC_ID)) {
                model.IgracId = Convert.ToString(values[IGRAC_ID]);
            }
        }

        private string GetFullErrorMessage(ModelStateDictionary modelState) {
            var messages = new List<string>();

            foreach(var entry in modelState) {
                foreach(var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" ", messages);
        }
    }
}