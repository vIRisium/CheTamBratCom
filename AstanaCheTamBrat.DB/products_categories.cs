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
    
    public partial class products_categories
    {
        public products_categories()
        {
            this.products = new HashSet<products>();
            this.products_categories1 = new HashSet<products_categories>();
        }
    
        public System.Guid category_id { get; set; }
        public string category_name { get; set; }
        public Nullable<System.Guid> category_parent { get; set; }
    
        public virtual ICollection<products> products { get; set; }
        public virtual ICollection<products_categories> products_categories1 { get; set; }
        public virtual products_categories products_categories2 { get; set; }
    }
}