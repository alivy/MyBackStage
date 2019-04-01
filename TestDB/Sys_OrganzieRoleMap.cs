namespace TestDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sys_OrganzieRoleMap
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string OrganizeCode { get; set; }

        [StringLength(50)]
        public string RoleId { get; set; }

        public virtual Sys_Organize Sys_Organize { get; set; }

        public virtual Sys_Role Sys_Role { get; set; }
    }
}
