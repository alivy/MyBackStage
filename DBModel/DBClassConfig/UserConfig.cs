using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.DBClassConfig
{
    public class UserConfig:EntityTypeConfiguration<Sys_UserRoleMap>
    {

        public UserConfig()
        {
            ///一对多（不配置一端的集合属性）
            ///多端     public virtual ICollection<Sys_UserRoleMap> Sys_UserRoleMap { get; set; }
            ///一端     public virtual Sys_User Sys_User { get; set; }
            ///多段模型配置
            /// this.HasRequired(e => e.Sys_UserRoleMap).WithMany().
            /// Map(x => x.ToTable("Sys_UserRoleMap").MapLeftKey("").
            /// MapRightKey(""));
            
            /// 映射数据库表明
            ToTable("Sys_UserRoleMap");
             ///一对多配置
            this.HasRequired(e => e.Sys_User).WithMany(x=>x.Sys_UserRoleMap).HasForeignKey(e => e.UserId);
            ///一对多最佳方式
            this.HasRequired(e => e.Sys_Role).WithMany().HasForeignKey(e => e.RoleId);

            //Map(x => x.ToTable("Sys_UserRoleMap").MapLeftKey("").
            //MapRightKey(""));
        }
    }
}
