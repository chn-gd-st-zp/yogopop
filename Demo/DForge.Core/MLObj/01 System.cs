namespace DForge.Core.MLObj;

//6位，以1开头

[MultiLanguageInit("SysAlert", "100000", "提示信息-操作成功")]
public class LogicSucceed : MultiLanguageObject { }

[MultiLanguageInit("SysAlert", "100001", "提示信息-操作失败")]
public class LogicFailed : MultiLanguageObject { }


[MultiLanguageInit("SysAlert", "100101", "提示信息-禁止的操作")]
public class ForbiddenAction : MultiLanguageObject { }

[MultiLanguageInit("SysAlert", "100102", "提示信息-目标对象不存在")]
public class TargetNotFound : MultiLanguageObject { }


[MultiLanguageInit("SysAlert", "100201", "提示信息-角色不存在")]
public class RoleNotFound : MultiLanguageObject { }

[MultiLanguageInit("SysAlert", "100202", "提示信息-角色不可用")]
public class RoleDisabled : MultiLanguageObject { }

[MultiLanguageInit("SysAlert", "100203", "提示信息-角色重复")]
public class RoleDuplicated : MultiLanguageObject { }

[MultiLanguageInit("SysAlert", "100204", "提示信息-角色类型无效")]
public class RoleTypeInvalid : MultiLanguageObject { }

[MultiLanguageInit("SysAlert", "100205", "提示信息-角色超过最大限制")]
public class RoleOverLimit : MultiLanguageObject { }


[MultiLanguageInit("SysAlert", "100301", "提示信息-账号不存在")]
public class AccountNotFound : MultiLanguageObject { }

[MultiLanguageInit("SysAlert", "100302", "提示信息-账号不可用")]
public class AccountDisabled : MultiLanguageObject { }

[MultiLanguageInit("SysAlert", "100303", "提示信息-账号重复")]
public class AccountDuplicated : MultiLanguageObject { }

[MultiLanguageInit("SysAlert", "100304", "提示信息-用户名格式错误")]
public class UsernameIncorrectFormat : MultiLanguageObject { }


[MultiLanguageInit("SysAlert", "100401", "提示信息-登录失败")]
public class SignInFailed : MultiLanguageObject { }

[MultiLanguageInit("SysAlert", "100402", "提示信息-旧密码无效")]
public class OldPasswordInvalid : MultiLanguageObject { }