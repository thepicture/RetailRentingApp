//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RetailRentingApp.Backend
{
    using System;
    using System.Collections.Generic;
    
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            this.PhoneNumberOfCustomers = new HashSet<PhoneNumberOfCustomer>();
            this.Cheques = new HashSet<Cheque>();
            this.Rentings = new HashSet<Renting>();
        }
    
        public int CustomerId { get; set; }
        public string CompanyName { get; set; }
        public string Requisite { get; set; }
        public string HeadOfficeAddress { get; set; }
        public string ContactFaceFirstName { get; set; }
        public string ContactFaceLastName { get; set; }
        public string ContactFaceMiddleName { get; set; }
        public byte[] ImageOfCustomer { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhoneNumberOfCustomer> PhoneNumberOfCustomers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cheque> Cheques { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Renting> Rentings { get; set; }
    }
}
