//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Zadatak_1.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblEmployee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblEmployee()
        {
            this.tblEmployees1 = new HashSet<tblEmployee>();
        }
    
        public int EmployeeID { get; set; }
        public string NameandSurname { get; set; }
        public string JMBG { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public string RegistrationNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public Nullable<int> Manager { get; set; }
        public Nullable<int> SectorID { get; set; }
        public Nullable<int> LocationID { get; set; }
    
        public virtual tblLocation tblLocation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblEmployee> tblEmployees1 { get; set; }
        public virtual tblEmployee tblEmployee1 { get; set; }
        public virtual tblSector tblSector { get; set; }
    }
}
