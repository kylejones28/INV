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
            checkOut.CurrentVM.BorrowedDate = DateTime.Now;
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
            checkOut.CurrentVM.DiskHasBorrowerId = diskhasborrower.DiskHasBorrowerId;
            checkOut.CurrentVM.BorrowerId = diskhasborrower.BorrowerId;
            checkOut.CurrentVM.DiskId = diskhasborrower.DiskId;
            checkOut.CurrentVM.BorrowedDate = diskhasborrower.BorrowedDate;
            checkOut.CurrentVM.ReturnedDate = diskhasborrower.ReturnedDate;
            return View(checkOut);
        }
        [HttpPost]
        public IActionResult Edit(DiskHasBorrowerViewModel diskhasborrowerviewmodel)
        {
            DiskHasBorrower checkOut = new DiskHasBorrower();
            if (ModelState.IsValid)
            {
                checkOut.DiskHasBorrowerId = diskhasborrowerviewmodel.CurrentVM.DiskHasBorrowerId;
                checkOut.BorrowerId = diskhasborrowerviewmodel.CurrentVM.BorrowerId;
                checkOut.DiskId = diskhasborrowerviewmodel.CurrentVM.DiskId;
                checkOut.BorrowedDate = diskhasborrowerviewmodel.CurrentVM.BorrowedDate;
                checkOut.ReturnedDate = diskhasborrowerviewmodel.CurrentVM.ReturnedDate;
                if (checkOut.DiskHasBorrowerId == 0)
                {
                    context.DiskHasBorrowers.Add(checkOut);
                    TempData["message"] = "Checkout added.";
                }
                else
                {
                    context.DiskHasBorrowers.Update(checkOut);
                    TempData["message"] = "Checkout updated.";
                }
                context.SaveChanges();
                return RedirectToAction("Index", "DiskHasBorrower");
            }
            ViewBag.Action = (diskhasborrowerviewmodel.CurrentVM.DiskHasBorrowerId == 0) ? "Add" : "Edit";
            diskhasborrowerviewmodel.Disks = context.Disks.OrderBy(d => d.DiskName).ToList();
            diskhasborrowerviewmodel.Borrowers = context.Borrowers.OrderBy(b => b.Lname).ToList();
            return View(diskhasborrowerviewmodel);
        }
    }
}
