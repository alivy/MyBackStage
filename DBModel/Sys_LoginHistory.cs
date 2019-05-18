namespace DBModel
{
    using System;
    using System.ComponentModel.DataAnnotations;

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

        public Sys_LoginHistory CreateInstance(string userId,string hostName,string hostIP,
            string loginLocal,string loginBrowser)
        {

            return new Sys_LoginHistory()
            {
                UserId = userId,
                HostIP = hostIP,
                HostName = hostName, 
                LoginBrowser = loginBrowser,
                LoginLocal = loginLocal,
                LoginDate = DateTime.Now
            };
        }


    }
}
