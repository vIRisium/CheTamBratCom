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
    
    public partial class provider_requests
    {
        public System.Guid request_id { get; set; }
        public string request_type { get; set; }
        public string request_text { get; set; }
        public Nullable<System.DateTime> request_date { get; set; }
        public System.Guid user_id { get; set; }
        public Nullable<System.Guid> provider_id { get; set; }
    
        public virtual users users { get; set; }
        public virtual providers providers { get; set; }
    }
}
