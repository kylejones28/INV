﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace DiskInventoryApps.Models
{
    public class DiskHasBorrowerViewModel
    {
        //public int DiskHasBorrowerId { get; set; }
        //[Required]
        //public int BorrowerId { get; set; }
        //[Required]
        //public int DiskId { get; set; }
        //public DateTime BorrowedDate { get; set; }
        //public DateTime? ReturnedDate { get; set; }

        public DiskHasBorrowerViewModel()
        {
            CurrentVM = new DiskHasBorrower();
        }

        public virtual Borrower Borrower { get; set; }
        public virtual Disk Disk { get; set; }
        public List<Borrower> Borrowers { get; set; }
        public List<Disk> Disks { get; set; }
        public DiskHasBorrower CurrentVM { get; set; }
    }
}
