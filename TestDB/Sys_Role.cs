namespace TestDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sys_Role
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sys_Role()
        {
            Sys_MenuRoleMap = new HashSet<Sys_MenuRoleMap>();
            Sys_OrganzieRoleMap = new HashSet<Sys_OrganzieRoleMap>();
            Sys_RoleMenuButttonMap = new HashSet<Sys_RoleMenuButttonMap>();
            Sys_UserRoleMap = new HashSet<Sys_UserRoleMap>();
        }

        [Key]
        [StringLength(50)]
        public string RoleId { get; set; }

        [StringLength(10)]
        public string RoleSeq { get; set; }

        [StringLength(200)]
        public string RoleName { get; set; }

        public int? CreateUserId { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? UpdateUserId { get; set; }

        public DateTime? UpdateDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sys_MenuRoleMap> Sys_MenuRoleMap { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sys_OrganzieRoleMap> Sys_OrganzieRoleMap { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sys_RoleMenuButttonMap> Sys_RoleMenuButttonMap { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sys_UserRoleMap> Sys_UserRoleMap { get; set; }
    }
}
