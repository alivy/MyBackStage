namespace DBModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sys_UserRoleMap
    {
        [Key]
        [Column("Id#编号自增")]
        public int Id { get; set; }

        [Column("UserId#用户id")]
        [StringLength(50)]
        public string UserId { get; set; }

        [Column("RoleId#角色Id")]
        [StringLength(50)]
        public string RoleId { get; set; }

        public virtual Sys_Role Sys_Role { get; set; }

        public virtual Sys_User Sys_User { get; set; }
    }
}
