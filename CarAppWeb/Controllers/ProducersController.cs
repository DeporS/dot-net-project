using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Interfaces;

namespace CarAppWeb.Controllers
{
    public class ProducersController : Controller
    {
        //private readonly DAOSqlite _context;
        private readonly IDAO dao;
        //public ProducersController(DAOSqlite context)
        public ProducersController(BLC.BLC blc)
        {
            dao = blc.DAO;
            //_context = context;
        }

        // GET: Producers
        public async Task<IActionResult> Index()
        {
            var Producers = dao.GetAllProducers();
            //return View(await _context.Producers.ToListAsync());
            return View(Producers);
        }

        // GET: Producers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var producer = await _context.Producers
            //    .FirstOrDefaultAsync(m => m.Id == id);
            var producer = dao.GetAllProducers().FirstOrDefault(x => x.Id == id);
            if (producer == null)
            {
                return NotFound();
            }

            return View(producer);
        }

        // GET: Producers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Producers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Name")] Producer producer)
        public async Task<IActionResult> Create(int Id, string Name)
        {
            IProducer producer = dao.CreateNewProducer();
            producer.Name = Name;

            this.ModelState.Clear();
            this.TryValidateModel(producer);
            
            if (ModelState.IsValid)
            {
                //_context.Add(producer);
                dao.AddProducer(producer);
                dao.SaveChanges();
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
        }

        // GET: Producers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var producer = await _context.Producers.FindAsync(id);
            var producer = dao.GetAllProducers().FirstOrDefault(x => x.Id == id);           
            if (producer == null)
            {
                return NotFound();
            }
            return View(producer);
        }

        // POST: Producers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int Id, string Name)
        {
            IProducer producer = dao.GetAllProducers().FirstOrDefault(x => x.Id == id);
            IProducer producertmp = dao.CreateNewProducer();
            if (id != producer.Id)
            {
                return NotFound();
            }

            producertmp.Id = id;
            producertmp.Name = Name;

            this.ModelState.Clear();
            this.TryValidateModel(producertmp);
            if (ModelState.IsValid)
            {
                producer.Name = producertmp.Name;
                try
                {
                    //_context.Update(producer);
                    //await _context.SaveChangesAsync();
                    dao.SaveChanges();
                }
                catch (Exception e)
                {
                    if (!ProducerExists(producer.Id))
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
            return View(producertmp);
        }

        // GET: Producers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var producer = await _context.Producers
            //    .FirstOrDefaultAsync(m => m.Id == id);
            var producer = dao.GetAllProducers().FirstOrDefault(x => x.Id == id);
            if (producer == null)
            {
                return NotFound();
            }

            return View(producer);
        }

        // POST: Producers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var producer = await _context.Producers.FindAsync(id);
            var producer = dao.GetAllProducers().FirstOrDefault(x => x.Id == id);
            if (producer != null)
            {
                //_context.Producers.Remove(producer);
                dao.RemoveProducer(producer);
            }

            //await _context.SaveChangesAsync();
            dao.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool ProducerExists(int id)
        {
            //return _context.Producers.Any(e => e.Id == id);
            return dao.GetAllProducers().Any(x => x.Id == id);
        }
    }
}
