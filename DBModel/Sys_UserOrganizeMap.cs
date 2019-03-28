namespace DBModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sys_UserOrganizeMap
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string OrganizeCode { get; set; }

        [StringLength(50)]
        public string UserId { get; set; }

        public virtual Sys_Organize Sys_Organize { get; set; }

        public virtual Sys_User Sys_User { get; set; }
    }
}
