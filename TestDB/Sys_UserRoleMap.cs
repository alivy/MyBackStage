namespace TestDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sys_UserRoleMap
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string UserId { get; set; }

        [StringLength(50)]
        public string RoleId { get; set; }

        public virtual Sys_Role Sys_Role { get; set; }

        public virtual Sys_User Sys_User { get; set; }
    }
}
