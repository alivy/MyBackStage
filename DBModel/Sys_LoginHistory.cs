namespace DBModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sys_LoginHistory
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string UserId { get; set; }

        [StringLength(50)]
        public string HostName { get; set; }

        [StringLength(30)]
        public string HostIP { get; set; }

        [StringLength(50)]
        public string LoginLocal { get; set; }

        [StringLength(50)]
        public string LoginBrowser { get; set; }

        public DateTime? LoginDate { get; set; }

       
    }
}
