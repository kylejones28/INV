using System.Linq;
using System.Threading.Tasks;
using DiskInventoryApps.Models;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiskInventoryApps.Controllers
{
    public class DiskController : Controller
    {
        private disk_inventorykjContext context { get; set; }
        public DiskController(disk_inventorykjContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            List<Disk> disk = context.Disks.OrderBy(b => b.DiskName).Include(g => g.Genre).Include(s => s.Status).Include(t => t.DiskType).ToList();
            return View(disk);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Genres = context.Genres.OrderBy(g => g.Description).ToList();
            ViewBag.Statuses = context.Statuses.OrderBy(s => s.Description).ToList();
            ViewBag.DiskTypes = context.DiskTypes.OrderBy(t => t.Description).ToList();
            return View("Edit", new Disk());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Genres = context.Genres.OrderBy(g => g.Description).ToList();
            ViewBag.Statuses = context.Statuses.OrderBy(s => s.Description).ToList();
            ViewBag.DiskTypes = context.DiskTypes.OrderBy(t => t.Description).ToList();
            var disk = context.Disks.Find(id);
            return View(disk);
        }

        [HttpPost]
        public IActionResult Edit(Disk disk)
        {
            if (ModelState.IsValid)
            {
                if (disk.DiskId == 0)
                    context.Disks.Add(disk);
                else
                    context.Disks.Update(disk);
                context.SaveChanges();
                return RedirectToAction("Index", "Disk");
            }
            else
            {

                ViewBag.Action = (disk.DiskId == 0) ? "Add" : "Edit";
                ViewBag.Genres = context.Genres.OrderBy(g => g.Description).ToList();
                ViewBag.Statuses = context.Statuses.OrderBy(s => s.Description).ToList();
                ViewBag.DiskTypes = context.DiskTypes.OrderBy(t => t.Description).ToList();
                return View(disk);

            }

        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var disk = context.Disks.Find(id);
            return View(disk);
        }
        [HttpPost]
        public IActionResult Delete(Disk disk)
        {
            context.Disks.Remove(disk);
            context.SaveChanges();
            return RedirectToAction("Index", "Disk");
        }
    }
}
