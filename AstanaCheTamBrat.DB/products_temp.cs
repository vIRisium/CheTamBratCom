//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AstanaCheTamBrat.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class products_temp
    {
        public System.Guid product_id { get; set; }
        public string product_name { get; set; }
        public string product_shordescription { get; set; }
        public string product_fulldescription { get; set; }
        public Nullable<int> product_price { get; set; }
        public Nullable<bool> product_hit { get; set; }
        public Nullable<bool> product_enabled { get; set; }
        public Nullable<bool> product_approved { get; set; }
        public Nullable<System.Guid> category_id { get; set; }
        public Nullable<System.Guid> provider_id { get; set; }
    }
}
