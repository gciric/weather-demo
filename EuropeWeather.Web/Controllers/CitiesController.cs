using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EuropeWeather.DataAccess;

namespace EuropeWeather.Web.Controllers
{
    public class CitiesController : Controller
    {
        private readonly EuropeWeatherEntities _db = new EuropeWeatherEntities();

        // GET: Cities
        public async Task<ActionResult> Index()
        {
            var cities = _db.Cities.Include(c => c.Countries);
            return View(await cities.ToListAsync());
        }

        // GET: Cities/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cities cities = await _db.Cities.FindAsync(id);
            if (cities == null)
            {
                return HttpNotFound();
            }
            return View(cities);
        }

        // GET: Cities/Create
        public ActionResult Create()
        {
            ViewBag.CountryId = new SelectList(_db.Countries, "CountryId", "Name");
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CityId,CountryId,CityName,ExternalId,Longitude,Latitude")] Cities cities)
        {
            if (ModelState.IsValid)
            {
                _db.Cities.Add(cities);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CountryId = new SelectList(_db.Countries, "CountryId", "Name", cities.CountryId);
            return View(cities);
        }

        // GET: Cities/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cities cities = await _db.Cities.FindAsync(id);
            if (cities == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryId = new SelectList(_db.Countries, "CountryId", "Name", cities.CountryId);
            return View(cities);
        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CityId,CountryId,CityName,ExternalId,Longitude,Latitude")] Cities cities)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(cities).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CountryId = new SelectList(_db.Countries, "CountryId", "Name", cities.CountryId);
            return View(cities);
        }

        // GET: Cities/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cities cities = await _db.Cities.FindAsync(id);
            if (cities == null)
            {
                return HttpNotFound();
            }
            return View(cities);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Cities cities = await _db.Cities.FindAsync(id);
            _db.Cities.Remove(cities);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
