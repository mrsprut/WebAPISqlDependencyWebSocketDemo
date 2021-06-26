using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPISqlDependencyDemo
{
    public partial class AklContentTote
    {
        public int Id { get; set; }
        public string Vlenr { get; set; }
        public string Aufnr { get; set; }
        public string Matnr { get; set; }
        public string Charg { get; set; }
        public decimal? MengeAufnr { get; set; }
        public decimal? MengeSource { get; set; }
        public decimal? MengeTarget { get; set; }
        public string Meins { get; set; }
        public string TypeCounting { get; set; }
        public string Workstation { get; set; }
        public string PmsUser { get; set; }
        public DateTime? PmsDate { get; set; }
        public TimeSpan? PmsTime { get; set; }
    }
}
