using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using EuropeWeather.DataAccess;

namespace EuropeWeather.Web.Controllers
{
    public class CountriesController : Controller
    {
        private readonly EuropeWeatherEntities _db = new EuropeWeatherEntities();

        // GET: Countries
        public async Task<ActionResult> Index()
        {
            return View(await _db.Countries.ToListAsync());
        }

        // GET: Countries/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Countries countries = await _db.Countries.FindAsync(id);
            if (countries == null)
            {
                return HttpNotFound();
            }
            return View(countries);
        }

        // GET: Countries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CountryId,Name,Code2,Code3")] Countries countries)
        {
            if (ModelState.IsValid)
            {
                _db.Countries.Add(countries);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(countries);
        }

        // GET: Countries/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Countries countries = await _db.Countries.FindAsync(id);
            if (countries == null)
            {
                return HttpNotFound();
            }
            return View(countries);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CountryId,Name,Code2,Code3")] Countries countries)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(countries).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(countries);
        }

        // GET: Countries/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Countries countries = await _db.Countries.FindAsync(id);
            if (countries == null)
            {
                return HttpNotFound();
            }
            return View(countries);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Countries countries = await _db.Countries.FindAsync(id);
            _db.Countries.Remove(countries);
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
