using System;
using System.Collections.Generic;

#nullable disable

namespace DiskInventoryApps.Models
{
    public partial class Status
    {
        public Status()
        {
            Disks = new HashSet<Disk>();
        }

        public int StatusId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Disk> Disks { get; set; }
    }
}
