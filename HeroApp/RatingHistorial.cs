//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HeroApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class RatingHistorial
    {
        public int RatingHistorialId { get; set; }
        public int HeroId { get; set; }
        public Nullable<int> Rating { get; set; }
        public string RaterName { get; set; }
    
        public virtual Hero Hero { get; set; }
    }
}