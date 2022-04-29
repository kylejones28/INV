using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DiskInventoryApps.Models;

namespace DiskInventoryApps.Controllers
{
    public class DiskHasBorrowerController : Controller
    {
        private disk_inventorykjContext context { get; set; }
        public DiskHasBorrowerController(disk_inventorykjContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            var diskhasborrowers = context.DiskHasBorrowers.
                Include(d => d.Disk).OrderBy(d => d.Disk.DiskName).
                Include(b => b.Borrower).ToList();
            return View(diskhasborrowers);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            DiskHasBorrowerViewModel checkOut = new DiskHasBorrowerViewModel();
            checkOut.BorrowedDate = DateTime.Now;
            checkOut.Disks = context.Disks.OrderBy(d => d.DiskName).ToList();
            checkOut.Borrowers = context.Borrowers.OrderBy(b => b.Lname).ToList();
            return View("Edit", checkOut);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var diskhasborrower = context.DiskHasBorrowers.Find(id);
            DiskHasBorrowerViewModel checkOut = new DiskHasBorrowerViewModel();
            checkOut.Disks = context.Disks.OrderBy(d => d.DiskName).ToList();
            checkOut.Borrowers = context.Borrowers.OrderBy(b => b.Lname).ToList();
            checkOut.DiskHasBorrowerId = diskhasborrower.DiskHasBorrowerId;
            checkOut.BorrowerId = diskhasborrower.BorrowerId;
            checkOut.DiskId = diskhasborrower.DiskId;
            checkOut.BorrowedDate = diskhasborrower.BorrowedDate;
            checkOut.ReturnedDate = diskhasborrower.ReturnedDate;
            return View(checkOut);
        }
        [HttpPost]
        public IActionResult Edit(DiskHasBorrowerViewModel diskhasborrowerviewmodel)
        {
            DiskHasBorrower checkOut = new DiskHasBorrower();
            if (ModelState.IsValid)
            {
                checkOut.DiskHasBorrowerId = diskhasborrowerviewmodel.DiskHasBorrowerId;
                checkOut.BorrowerId = diskhasborrowerviewmodel.BorrowerId;
                checkOut.DiskId = diskhasborrowerviewmodel.DiskId;
                checkOut.BorrowedDate = diskhasborrowerviewmodel.BorrowedDate;
                checkOut.ReturnedDate = diskhasborrowerviewmodel.ReturnedDate;
                if (checkOut.DiskHasBorrowerId == 0)
                {
                    context.DiskHasBorrowers.Add(checkOut);
                }
                else
                {
                    context.DiskHasBorrowers.Update(checkOut);
                }
                context.SaveChanges();
                return RedirectToAction("Index", "DiskHasBorrower");
            }
            ViewBag.Action = (diskhasborrowerviewmodel.DiskHasBorrowerId == 0) ? "Add" : "Edit";
            return View(diskhasborrowerviewmodel);
        }
    }
}
