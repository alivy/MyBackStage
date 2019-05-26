namespace DBModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sys_button
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sys_button()
        {
            Sys_MenuButttonMap = new HashSet<Sys_MenuButttonMap>();
            Sys_RoleMenuButttonMap = new HashSet<Sys_RoleMenuButttonMap>();
        }

        private const string _key = "button_{0}";

        /// <summary>
        ///  KeyFormat 格式
        /// </summary>
        public string KeyFormat
        {
            get { return _key; }
        }

        /// <summary>
        ///  获得CouchBase的Key
        /// </summary>
        /// <returns></returns>
        public static string GetKey(string userId)
        {
            return string.Format(_key, userId);
        }


        [Key]
        [StringLength(50)]
        public string ButtonId { get; set; }

        [StringLength(200)]
        public string ButtonName { get; set; }

        public int? ButtonSeq { get; set; }

        [StringLength(200)]
        public string ButtonIcon { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sys_MenuButttonMap> Sys_MenuButttonMap { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sys_RoleMenuButttonMap> Sys_RoleMenuButttonMap { get; set; }
    }
}
