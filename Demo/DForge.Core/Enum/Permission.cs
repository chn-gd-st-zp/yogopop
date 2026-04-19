namespace DForge.Core.Enum;

public enum GlobalPermissionEnum
{
    [Description("默认、无")]
    None = 0,

    [Description("系统权限")]
    SystemPermission,

    [Description("基础控制")]
    Base,

    [Description("资源站点")]
    Resource,

    [Description("系统后台")]
    SAdmin,

    #region Base

    #region KVResource

    [Description("系统工具")]
    SysTool,

    [Description("刷新资源"), Operation(OperationTypeEnum.Update)]
    SysTool_KVRefresh,

    [Description("维护状态切换"), Operation(OperationTypeEnum.Update)]
    SysTool_StatusSet,

    #endregion

    #region Attachment

    [Description("附件")]
    Attachment,

    [Description("上传附件"), Operation(OperationTypeEnum.Create)]
    Attachment_Upload,

    #endregion

    #region AccountProperty

    [Description("用户字段权限")]
    Account_Property,

    [Description("创建")]
    Account_Property_Create,

    [Description("创建手机"), Operation(OperationTypeEnum.Create)]
    Account_Property_Create_Mobile,

    [Description("创建邮箱"), Operation(OperationTypeEnum.Create)]
    Account_Property_Create_Email,

    [Description("修改")]
    Account_Property_Update,

    [Description("修改手机"), Operation(OperationTypeEnum.Update)]
    Account_Property_Update_Mobile,

    [Description("修改邮箱"), Operation(OperationTypeEnum.Update)]
    Account_Property_Update_Email,

    [Description("查看")]
    Account_Property_Search,

    [Description("查看手机"), Operation(OperationTypeEnum.Search)]
    Account_Property_Search_Mobile,

    [Description("查看邮箱"), Operation(OperationTypeEnum.Search)]
    Account_Property_Search_Email,

    #endregion

    #endregion

    #region SAdmin

    #region Menu

    [Description("应用管理")]
    AppManage,

    [Description("系统管理")]
    SysManage,

    #endregion

    #region AppManage

    #region Project

    [Description("项目")]
    Project,

    [Description("项目创建"), Operation(OperationTypeEnum.Create)]
    Project_Create,

    [Description("项目删除"), Operation(OperationTypeEnum.Delete)]
    Project_Delete,

    [Description("项目修改"), Operation(OperationTypeEnum.Update)]
    Project_Update,

    [Description("项目启禁"), Operation(OperationTypeEnum.Update)]
    Project_Status,

    [Description("项目详情"), Operation(OperationTypeEnum.Search)]
    Project_Single,

    [Description("项目分页"), Operation(OperationTypeEnum.Search)]
    Project_Page,

    #endregion

    #region DSPChannel

    [Description("DSP通道")]
    DSPChannel,

    [Description("DSP通道创建"), Operation(OperationTypeEnum.Create)]
    DSPChannel_Create,

    [Description("DSP通道删除"), Operation(OperationTypeEnum.Delete)]
    DSPChannel_Delete,

    [Description("DSP通道修改"), Operation(OperationTypeEnum.Update)]
    DSPChannel_Update,

    [Description("DSP通道启禁"), Operation(OperationTypeEnum.Update)]
    DSPChannel_Status,

    [Description("DSP通道详情"), Operation(OperationTypeEnum.Search)]
    DSPChannel_Single,

    [Description("DSP通道分页"), Operation(OperationTypeEnum.Search)]
    DSPChannel_Page,

    [Description("DSP通道同步发起"), Operation(OperationTypeEnum.Create)]
    DSPChannelSyncRecord_Apply,

    [Description("DSP通道同步记录"), Operation(OperationTypeEnum.Search)]
    DSPChannelSyncRecord_Page,

    #endregion

    #region Domain

    [Description("域名")]
    Domain,

    [Description("域名分页"), Operation(OperationTypeEnum.Search)]
    Domain_Page,

    [Description("域名NS设置"), Operation(OperationTypeEnum.Update)]
    Domain_NSModify,

    [Description("域名同步发起"), Operation(OperationTypeEnum.Create)]
    DomainSyncRecord_Apply,

    [Description("域名同步记录"), Operation(OperationTypeEnum.Search)]
    DomainSyncRecord_Page,

    #endregion

    #region DNSRecord

    [Description("DNS解析")]
    DNSRecord,

    [Description("DNS解析列表"), Operation(OperationTypeEnum.Search)]
    DNSRecord_List,

    [Description("DNS解析设置"), Operation(OperationTypeEnum.Update)]
    DNSRecord_Set,

    #endregion

    #endregion

    #region SysManage

    #region AccessRecord

    [Description("系统日志")]
    AccessRecord,

    [Description("系统日志详情"), Operation(OperationTypeEnum.Search)]
    AccessRecord_Single,

    [Description("系统日志列表"), Operation(OperationTypeEnum.Search)]
    AccessRecord_Page,

    #endregion

    #region Permission

    [Description("权限")]
    Permission,

    [Description("权限修改"), Operation(OperationTypeEnum.Update)]
    Permission_Update,

    #endregion

    #region Role

    [Description("角色")]
    Role,

    [Description("角色创建"), Operation(OperationTypeEnum.Create)]
    Role_Create,

    [Description("角色删除"), Operation(OperationTypeEnum.Delete)]
    Role_Delete,

    [Description("角色修改"), Operation(OperationTypeEnum.Update)]
    Role_Update,

    [Description("角色启禁"), Operation(OperationTypeEnum.Update)]
    Role_Status,

    [Description("角色详情"), Operation(OperationTypeEnum.Search)]
    Role_Single,

    [Description("角色分页"), Operation(OperationTypeEnum.Search)]
    Role_Page,

    #endregion

    #region Menu

    [Description("菜单")]
    Menu,

    [Description("菜单创建"), Operation(OperationTypeEnum.Create)]
    Menu_Create,

    [Description("菜单删除"), Operation(OperationTypeEnum.Delete)]
    Menu_Delete,

    [Description("菜单修改"), Operation(OperationTypeEnum.Update)]
    Menu_Update,

    [Description("菜单排序"), Operation(OperationTypeEnum.Update)]
    Menu_Sort,

    #endregion

    #region Admin

    [Description("管理员")]
    Admin,

    [Description("管理员创建"), Operation(OperationTypeEnum.Create)]
    Admin_Create,

    [Description("管理员删除"), Operation(OperationTypeEnum.Delete)]
    Admin_Delete,

    [Description("管理员修改"), Operation(OperationTypeEnum.Update)]
    Admin_Update,

    [Description("管理员启禁"), Operation(OperationTypeEnum.Update)]
    Admin_Status,

    [Description("管理员重置密码"), Operation(OperationTypeEnum.Update)]
    Admin_ResetPassword,

    [Description("管理员重置MFA"), Operation(OperationTypeEnum.Update)]
    Admin_ResetMFA,

    [Description("管理员详情"), Operation(OperationTypeEnum.Search)]
    Admin_Single,

    [Description("管理员分页"), Operation(OperationTypeEnum.Search)]
    Admin_Page,

    #endregion

    #region Session

    [Description("会话")]
    Session,

    [Description("会话分页"), Operation(OperationTypeEnum.Search)]
    Session_Page,

    [Description("会话踢除"), Operation(OperationTypeEnum.Delete)]
    Session_Kick,

    #endregion

    #endregion

    #endregion
}