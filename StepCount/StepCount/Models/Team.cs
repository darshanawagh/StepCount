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
    
    public partial class Team
    {
        public Team()
        {
            this.Participants = new HashSet<Participant>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Motto { get; set; }
    
        public virtual ICollection<Participant> Participants { get; set; }
    }
}
