namespace DForge.Database.EF;

public class DomainForgeEFDBContext : EFDBContext
{
    public DomainForgeEFDBContext(EFDBContextOptionsBuilder<DomainForgeEFDBContext> builder) : base(builder) { }

    public DbSet<TBSessionDevice> TBSessionDevice { get; set; }
    public DbSet<TBSessionAccount> TBSessionAccount { get; set; }

    public DbSet<TBSysAccessRecord> TBSysAccessRecord { get; set; }
    public DbSet<TBSysSettings> TBSysSettings { get; set; }
    public DbSet<TBSysPermission> TBSysPermission { get; set; }
    public DbSet<TBSysRole> TBSysRole { get; set; }
    public DbSet<TBSysRolePermission> TBSysRolePermission { get; set; }
    public DbSet<TBSysMenu> TBSysMenu { get; set; }

    public DbSet<TBAccountAdmin> TBAccountAdmin { get; set; }
    public DbSet<TBAccountInfo> TBAccountInfo { get; set; }

    public DbSet<TBAppProject> TBAppProject { get; set; }
    public DbSet<TBAppDSPChannel> TBAppDSPChannel { get; set; }
    public DbSet<TBAppDomain> TBAppDomain { get; set; }
    public DbSet<TBAppDNSRecord> TBAppDNSRecord { get; set; }
    public DbSet<TBAppDynSchRecord> TBAppDynSchRecord { get; set; }

    protected override void InitDBSets()
    {
        AddDBSet(TBSessionDevice);
        AddDBSet(TBSessionAccount);

        AddDBSet(TBSysAccessRecord);
        AddDBSet(TBSysSettings);
        AddDBSet(TBSysPermission);
        AddDBSet(TBSysRole);
        AddDBSet(TBSysRolePermission);
        AddDBSet(TBSysMenu);

        AddDBSet(TBAccountAdmin);
        AddDBSet(TBAccountInfo);

        AddDBSet(TBAppProject);
        AddDBSet(TBAppDSPChannel);
        AddDBSet(TBAppDomain);
        AddDBSet(TBAppDNSRecord);
        AddDBSet(TBAppDynSchRecord);
    }

    protected override void BindMaps(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TBSessionDeviceMapping());
        modelBuilder.ApplyConfiguration(new TBSessionAccountMapping());

        modelBuilder.ApplyConfiguration(new TBSysAccessRecordMapping());
        modelBuilder.ApplyConfiguration(new TBSysSettingsMapping());
        modelBuilder.ApplyConfiguration(new TBSysPermissionMapping());
        modelBuilder.ApplyConfiguration(new TBSysRoleMapping());
        modelBuilder.ApplyConfiguration(new TBSysRolePermissionMapping());
        modelBuilder.ApplyConfiguration(new TBSysMenuMapping());

        modelBuilder.ApplyConfiguration(new TBAccountAdminMapping());
        modelBuilder.ApplyConfiguration(new TBAccountInfoMapping());

        modelBuilder.ApplyConfiguration(new TBAppProjectMapping());
        modelBuilder.ApplyConfiguration(new TBAppDSPChannelMapping());
        modelBuilder.ApplyConfiguration(new TBAppDomainMapping());
        modelBuilder.ApplyConfiguration(new TBAppDNSRecordMapping());
        modelBuilder.ApplyConfiguration(new TBAppDynSchRecordMapping());
    }
}