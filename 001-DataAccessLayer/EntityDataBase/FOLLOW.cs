//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AlphaTestSystem.EntityDataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class FOLLOW
    {
        public int followCounter { get; set; }
        public int followedID { get; set; }
        public int followerID { get; set; }
    
        public virtual USER USER { get; set; }
        public virtual USER USER1 { get; set; }
    }
}
