//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BankApp_V2_MVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Lokaty
    {
        public int LokataID { get; set; }
        public string UserID { get; set; }
        public Nullable<decimal> Saldo { get; set; }
        public Nullable<int> OkresTrwania { get; set; }
        public Nullable<decimal> OprocentowanieLokaty { get; set; }
        public Nullable<System.DateTime> DataOtwarcia { get; set; }
        public Nullable<System.DateTime> DataWyga { get; set; }
        public Nullable<System.DateTime> DataNajNaliczeniaOdsetek { get; set; }
        public Nullable<decimal> NaliczoneOdsetki { get; set; }
        public Nullable<int> KontoID { get; set; }
        public string Status { get; set; }
    }
}