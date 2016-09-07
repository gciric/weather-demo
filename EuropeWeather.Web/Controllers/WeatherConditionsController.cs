using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using EuropeWeather.DataAccess;

namespace EuropeWeather.Web.Controllers
{
    public class WeatherConditionsController : Controller
    {
        private readonly EuropeWeatherEntities _db = new EuropeWeatherEntities();

        // GET: WeatherConditions
        public async Task<ActionResult> Index()
        {
            return View(await _db.WeatherConditions.ToListAsync());
        }

        // GET: WeatherConditions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WeatherConditions weatherConditions = await _db.WeatherConditions.FindAsync(id);
            if (weatherConditions == null)
            {
                return HttpNotFound();
            }
            return View(weatherConditions);
        }

        // GET: WeatherConditions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WeatherConditions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "WeatherConditionId,ExternalId,Description")] WeatherConditions weatherConditions)
        {
            if (ModelState.IsValid)
            {
                _db.WeatherConditions.Add(weatherConditions);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(weatherConditions);
        }

        // GET: WeatherConditions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WeatherConditions weatherConditions = await _db.WeatherConditions.FindAsync(id);
            if (weatherConditions == null)
            {
                return HttpNotFound();
            }
            return View(weatherConditions);
        }

        // POST: WeatherConditions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "WeatherConditionId,ExternalId,Description")] WeatherConditions weatherConditions)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(weatherConditions).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(weatherConditions);
        }

        // GET: WeatherConditions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WeatherConditions weatherConditions = await _db.WeatherConditions.FindAsync(id);
            if (weatherConditions == null)
            {
                return HttpNotFound();
            }
            return View(weatherConditions);
        }

        // POST: WeatherConditions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            WeatherConditions weatherConditions = await _db.WeatherConditions.FindAsync(id);
            _db.WeatherConditions.Remove(weatherConditions);
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
