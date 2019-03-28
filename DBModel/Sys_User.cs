namespace DBModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sys_User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sys_User()
        {
            Sys_UserOrganizeMap = new HashSet<Sys_UserOrganizeMap>();
            Sys_UserRoleMap = new HashSet<Sys_UserRoleMap>();
        }

        [Key]
        [StringLength(50)]
        public string UserId { get; set; }

        public int? UserSeq { get; set; }

        [StringLength(10)]
        public string UserRoleName { get; set; }

        [StringLength(50)]
        public string UserNickName { get; set; }

        [StringLength(200)]
        public string Password { get; set; }

        [StringLength(200)]
        public string OrganizeName { get; set; }

        public int? IsEnable { get; set; }

        public int? CreateUserId { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int? UpdateUserId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sys_UserOrganizeMap> Sys_UserOrganizeMap { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sys_UserRoleMap> Sys_UserRoleMap { get; set; }
    }
}
