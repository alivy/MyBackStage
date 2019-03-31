namespace TestDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sys_Organize
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sys_Organize()
        {
            Sys_OrganzieRoleMap = new HashSet<Sys_OrganzieRoleMap>();
            Sys_UserOrganizeMap = new HashSet<Sys_UserOrganizeMap>();
        }

        [Key]
        [StringLength(50)]
        public string OrganizeCode { get; set; }

        [StringLength(50)]
        public string ParentCode { get; set; }

        public int? OrganizeSeq { get; set; }

        [StringLength(200)]
        public string OrganizeName { get; set; }

        public int? CereateUserId { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? UpdateUserId { get; set; }

        public DateTime? UpdateDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sys_OrganzieRoleMap> Sys_OrganzieRoleMap { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sys_UserOrganizeMap> Sys_UserOrganizeMap { get; set; }
    }
}
