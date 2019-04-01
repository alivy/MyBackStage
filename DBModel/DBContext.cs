namespace DBModel
{
    using System.Data.Entity;
    using System.Runtime.Remoting.Messaging;

    public partial class DBContext : DbContext
    {
        public DBContext(): base("name=DbContext")
        {
        }
        #region 创建DBContext
        /// <summary>
        /// 创建DBContext
        /// </summary>
        /// <returns></returns>
        public static DBContext CreateContext()
        {
            //DBContext db = CallContext.GetData("DbContext") as DBContext;
            //if (db == null)
            //{
            //    db = new DBContext();
            //    CallContext.SetData("DbContext", db);
            //}
            //return db;
            return new DBContext();
        }
        #endregion
        public virtual DbSet<Sys_button> Sys_button { get; set; }
        public virtual DbSet<Sys_MenuButttonMap> Sys_MenuButttonMap { get; set; }
        public virtual DbSet<Sys_MenuRoleMap> Sys_MenuRoleMap { get; set; }
        public virtual DbSet<Sys_NavMenu> Sys_NavMenu { get; set; }
        public virtual DbSet<Sys_Organize> Sys_Organize { get; set; }
        public virtual DbSet<Sys_OrganzieRoleMap> Sys_OrganzieRoleMap { get; set; }
        public virtual DbSet<Sys_Role> Sys_Role { get; set; }
        public virtual DbSet<Sys_RoleMenuButttonMap> Sys_RoleMenuButttonMap { get; set; }
        public virtual DbSet<Sys_User> Sys_User { get; set; }
        public virtual DbSet<Sys_UserRoleMap> Sys_UserRoleMap { get; set; }
        public virtual DbSet<Sys_UserOrganizeMap> Sys_UserOrganizeMap { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sys_button>()
                .Property(e => e.ButtonId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_button>()
                .Property(e => e.ButtonName)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_button>()
                .Property(e => e.ButtonIcon)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_MenuButttonMap>()
                .Property(e => e.ButtonId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_MenuButttonMap>()
                .Property(e => e.MenuId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_MenuRoleMap>()
                .Property(e => e.MendId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_MenuRoleMap>()
                .Property(e => e.RoleId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_NavMenu>()
                .Property(e => e.MenuId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_NavMenu>()
                .Property(e => e.MenuName)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_NavMenu>()
                .Property(e => e.ParentMenId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_NavMenu>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_NavMenu>()
                .Property(e => e.IconClass)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_NavMenu>()
                .Property(e => e.IconUrl)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_NavMenu>()
                .HasMany(e => e.Sys_MenuRoleMap)
                .WithOptional(e => e.Sys_NavMenu)
                .HasForeignKey(e => e.MendId);

            modelBuilder.Entity<Sys_Organize>()
                .Property(e => e.OrganizeCode)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Organize>()
                .Property(e => e.ParentCode)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Organize>()
                .Property(e => e.OrganizeName)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_OrganzieRoleMap>()
                .Property(e => e.OrganizeCode)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_OrganzieRoleMap>()
                .Property(e => e.RoleId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Role>()
                .Property(e => e.RoleId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Role>()
                .Property(e => e.RoleSeq)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Role>()
                .Property(e => e.RoleName)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Role>()
                .HasMany(e => e.Sys_UserRoleMap)
                .WithOptional(e => e.Sys_Role)
                .HasForeignKey(e => e.RoleId);

            modelBuilder.Entity<Sys_RoleMenuButttonMap>()
                .Property(e => e.RoleId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_RoleMenuButttonMap>()
                .Property(e => e.MenuId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_RoleMenuButttonMap>()
                .Property(e => e.ButtonId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_User>()
                .Property(e => e.UserId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_User>()
                .Property(e => e.UserRoleName)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_User>()
                .Property(e => e.UserNickName)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_User>()
                .Property(e => e.OrganizeName)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_User>()
                .HasMany(e => e.Sys_UserRoleMap)
                .WithOptional(e => e.Sys_User)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Sys_UserRoleMap>()
                .Property(e => e.UserId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_UserRoleMap>()
                .Property(e => e.RoleId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_UserOrganizeMap>()
                .Property(e => e.OrganizeCode)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_UserOrganizeMap>()
                .Property(e => e.UserId)
                .IsUnicode(false);
        }
    }
}
