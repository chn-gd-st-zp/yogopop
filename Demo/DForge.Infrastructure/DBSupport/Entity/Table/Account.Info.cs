namespace DForge.Infrastructure.DBSupport.Entity.Table;

[Table("TB_Account_Info")]
public partial class TBAccountInfo : TBAccountBase, IPermissionPropertyForCreate, IPermissionPropertyForUpdate, IPermissionPropertyForSearch
{
    [Column("RoleType")]
    [Sort("RoleType")]
    public RoleEnum RoleType { get; set; } = RoleEnum.None;

    [Column("UserType")]
    [Sort("UserType")]
    public UserTypeEnum UserType { get; set; } = UserTypeEnum.None;

    [Column("Prefix")]
    [Sort("Prefix")]
    public string Prefix { get; set; } = string.Empty;

    [Column("Mobile")]
    [Sort("Mobile")]
    [PropertyPermission(GlobalPermissionEnum.Account_Property_Create_Mobile, GlobalPermissionEnum.Account_Property_Create, PermissionPropertyFailHandleEnum.Throw, "")]
    [PropertyPermission(GlobalPermissionEnum.Account_Property_Update_Mobile, GlobalPermissionEnum.Account_Property_Update, PermissionPropertyFailHandleEnum.Ignore, "")]
    [PropertyPermission(GlobalPermissionEnum.Account_Property_Search_Mobile, GlobalPermissionEnum.Account_Property_Search, PermissionPropertyFailHandleEnum.Mosaic)]
    public string Mobile { get; set; } = string.Empty;

    [Column("Email")]
    [Sort("Email")]
    [PropertyPermission(GlobalPermissionEnum.Account_Property_Create_Email, GlobalPermissionEnum.Account_Property_Create, PermissionPropertyFailHandleEnum.Throw, "")]
    [PropertyPermission(GlobalPermissionEnum.Account_Property_Update_Email, GlobalPermissionEnum.Account_Property_Update, PermissionPropertyFailHandleEnum.Ignore, "")]
    [PropertyPermission(GlobalPermissionEnum.Account_Property_Search_Email, GlobalPermissionEnum.Account_Property_Search, PermissionPropertyFailHandleEnum.Mosaic)]
    public string Email { get; set; } = string.Empty;

    [Column("ProfilePhoto")]
    [Sort("ProfilePhoto")]
    public string ProfilePhoto { get; set; } = string.Empty;

    [Column("NickName")]
    [Sort("NickName")]
    public string NickName { get; set; } = string.Empty;

    [Column("TrueName")]
    [Sort("TrueName")]
    public string TrueName { get; set; } = string.Empty;

    [Column("MFASecret")]
    [Sort("MFASecret")]
    public string MFASecret { get; set; } = string.Empty;

    [Column("Gender")]
    [Sort("Gender")]
    public GenderEnum Gender { get; set; } = GenderEnum.None;

    [Column("Language")]
    [Sort("Language")]
    public LanguageEnum Language { get; set; } = LanguageEnum.None;
}