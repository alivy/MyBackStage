namespace DBModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sys_NavMenu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sys_NavMenu()
        {
            Sys_MenuButttonMap = new HashSet<Sys_MenuButttonMap>();
            Sys_MenuRoleMap = new HashSet<Sys_MenuRoleMap>();
            Sys_RoleMenuButttonMap = new HashSet<Sys_RoleMenuButttonMap>();
        }


        private const string _key = "NavMenu_{0}";
       
        /// <summary>
        ///  KeyFormat 格式
        /// </summary>
        public string KeyFormat
        {
            get { return _key; }
        }

        /// <summary>
        ///  获得缓存的Key
        /// </summary>
        /// <returns></returns>
        public static string GetKey(string  userId)
        {
            return string.Format(_key, userId);
        }



        [Key]
        [StringLength(50)]
        public string MenuId { get; set; }

        [StringLength(100)]
        public string MenuName { get; set; }

        [StringLength(50)]
        public string ParentMenId { get; set; }

        public int Level { get; set; }

        [StringLength(300)]
        public string Url { get; set; }

        [StringLength(50)]
        public string IconClass { get; set; }

        [StringLength(300)]
        public string IconUrl { get; set; }

        public int? Seq { get; set; }

        public int? IsVisible { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sys_MenuButttonMap> Sys_MenuButttonMap { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sys_MenuRoleMap> Sys_MenuRoleMap { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sys_RoleMenuButttonMap> Sys_RoleMenuButttonMap { get; set; }
    }
}
