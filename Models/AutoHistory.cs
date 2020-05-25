using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlimentosMarfim.Models
{
    public class AutoHistory
    {

        public int Id { get; set; }
        public string RowId { get; set; }
        public string TableName { get; set; }
        public string Changed { get; set; }
        public EntityState Kind { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;

    }
}
