//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MobileBackend.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Departments
    {
        public int Department_id { get; set; }
        public Nullable<int> Employee_id { get; set; }
        public Nullable<int> Customer_id { get; set; }
        public Nullable<int> Contractor_id { get; set; }
        public Nullable<int> Kustannuspaikka_id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    
        public virtual Employees Employees { get; set; }
        public virtual Kustannuspaikat Kustannuspaikat { get; set; }
    }
}
