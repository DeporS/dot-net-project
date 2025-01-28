using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Interfaces;

namespace CarAppWeb.Controllers
{
    public class CarsController : Controller
    {
        //private readonly DAOSqlite _context;
        private readonly IDAO dao;
        //public CarsController(DAOSqlite context)
        public CarsController(BLC.BLC blc)
        {
            dao = blc.DAO;
            //_context = context;
        }

        // GET: Cars
        public async Task<IActionResult> Index()
        {
            //var dAOSqlite = _context.Cars.Include(c => c.Producer);
            var Cars = dao.GetAllCars();
            //return View(await dAOSqlite.ToListAsync());
            return View(Cars);
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var car = await _context.Cars
            //    .Include(c => c.Producer)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            var car = dao.GetAllCars().FirstOrDefault(x => x.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Cars/Create
        public IActionResult Create()
        {
            ViewData["Producer"] = new SelectList(dao.GetAllProducers(), "Id", "Name");
            
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int Id, string Name, int ProdYear, int Transmission, int ProducerId)
        {
            IShoe car = dao.CreateNewCar();
            car.Name = Name;
            car.ProdYear = ProdYear;
            car.Transmission = (Interfaces.Type)Transmission;
            car.Producer = dao.GetAllProducers().First(p => p.Id == ProducerId);

            this.ModelState.Clear();
            this.TryValidateModel(car);

            if (ModelState.IsValid)
            {
                //_context.Add(car);
                //await _context.SaveChangesAsync();
                dao.AddCar(car);
                dao.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Producer"] = new SelectList(dao.GetAllProducers(), "Id", "Name");
            return View(car);
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var car = await _context.Cars.FindAsync(id);
            var car = dao.GetAllCars().FirstOrDefault(p => p.Id == id);

            if (car == null)
            {
                return NotFound();
            }
            //ViewData["ProducerId"] = new SelectList(_context.Producers, "Id", "Name", car.ProducerId);
            ViewData["Producer"] = new SelectList(dao.GetAllProducers(), "Id", "Name", car.Producer.Id);
            //ViewData["Producer"] = new SelectList(dao.GetAllProducers(), "Id", "Name");
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string Name,

            int ProdYear, int Transmission, int ProducerId)

        {

            IShoe car = dao.GetAllCars().FirstOrDefault(c => c.Id == id);

            IShoe cartmp = dao.CreateNewCar();

            if (id != car.Id)

            {
                return NotFound();
            }

            cartmp.Id = id;
            cartmp.Producer = dao.GetAllProducers().First(p => p.Id == ProducerId);
            cartmp.Name = Name;
            cartmp.ProdYear = ProdYear;
            cartmp.Transmission = (Interfaces.Type)Transmission;

            this.ModelState.Clear();
            this.TryValidateModel(cartmp);

            if (ModelState.IsValid)
            {
                car.Name = cartmp.Name;
                car.Transmission = cartmp.Transmission;
                car.ProdYear = cartmp.ProdYear;
                car.Producer = cartmp.Producer;

                try
                {
                    //_context.Update(car);
                    //await _context.SaveChangesAsync();
                    dao.SaveChanges();
                }
                catch (Exception e)
                {
                    if (!CarExists(car.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            //ViewData["ProducerId"] = new SelectList(_context.Producers, "Id", "Name", car.ProducerId);
            //ViewData["Producer"] = new SelectList(dao.GetAllProducers(), "Id", "Name");
            ViewData["Producer"] = new SelectList(dao.GetAllProducers(), "Id", "Name", car.Producer.Id);
            return View(cartmp);
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var car = await _context.Cars
            //    .Include(c => c.Producer)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            var car = dao.GetAllCars().FirstOrDefault(c => c.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var car = await _context.Cars.FindAsync(id);
            var car = dao.GetAllCars().FirstOrDefault(c => c.Id == id);
            if (car != null)
            {
                //_context.Cars.Remove(car);
                dao.RemoveCar(car);
            }

            //await _context.SaveChangesAsync();
            dao.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            //return _context.Cars.Any(e => e.Id == id);
            return dao.GetAllCars().Any(c => c.Id == id);
        }
    }
}
