//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StepCount.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class dbo_Participants
    {
        public dbo_Participants()
        {
            this.LogEntries = new HashSet<LogEntry>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> TeamId { get; set; }
    
        public virtual Team Team { get; set; }
        public virtual ICollection<LogEntry> LogEntries { get; set; }
    }
}
