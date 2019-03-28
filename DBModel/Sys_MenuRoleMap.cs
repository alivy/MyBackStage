namespace DBModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sys_MenuRoleMap
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string MendId { get; set; }

        [StringLength(50)]
        public string RoleId { get; set; }

        public virtual Sys_NavMenu Sys_NavMenu { get; set; }

        public virtual Sys_Role Sys_Role { get; set; }
    }
}
