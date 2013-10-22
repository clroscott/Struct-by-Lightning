using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Checklist.Models
{

    public class Consultant
    {
        [Key]
        public int LocationId { get; set; }

        public string LocationName { get; set; }

        public string BusinessConsultant { get; set; }

        public string Province { get; set; }

        public string Email { get; set; }
    }

    public class ConsultContext : DbContext
    {
        public DbSet<Consultant> qEntry { get; set; }
    }
}
