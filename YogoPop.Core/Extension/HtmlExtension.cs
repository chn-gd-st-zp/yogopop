namespace YogoPop.Core.Extension;

public static class HtmlExtension
{
    /// <summary>
    /// 判断字符串中是否包含 HTML 标签或包含 http、javascript 等内容
    /// </summary>
    public static bool ContainsHtml(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;

        // 匹配 HTML 标签（如 <div>、<a href=""> 等）
        var htmlTagRegex = @"<[^>]+>";

        // 匹配包含 http 或 javascript 的字符串（可加上 ftp、data 等其他协议）
        var scriptOrLinkRegex = @"(http[s]?://|javascript:|data:|ftp://)";

        return Regex.IsMatch(input, htmlTagRegex, RegexOptions.IgnoreCase) ||
               Regex.IsMatch(input, scriptOrLinkRegex, RegexOptions.IgnoreCase);
    }

    /// <summary>
    /// 移除HTML标签
    /// </summary>
    /// <param name="html"></param>
    /// <param name="unRemoveTags"></param>
    /// <returns></returns>
    public static string RemoveHTML(this string html, string[] unRemoveTags = null)
    {
        unRemoveTags = unRemoveTags.IsNotEmpty() ? unRemoveTags : new string[] { };
        unRemoveTags = unRemoveTags.Select(o => o.ToLower()).ToArray();

        var otherTags = new string[] { "!doctype html", "base", "!", "object" };
        otherTags = otherTags.Select(o => o.ToLower()).ToArray();

        var regex = string.Empty;

        regex = @" <!--([^(-){2}])*-->";
        html = RemoveHTMLByPattern(html, regex);

        foreach (var tag in otherTags)
        {
            if (unRemoveTags.Contains(tag))
                continue;

            regex = @"<[\/]{0,1}(" + tag + @" [^<>]*>)|<[\/]{0,1}(" + tag + ">)";
            html = RemoveHTMLByPattern(html, regex);
        }

        foreach (var tag in EnumExtension.ToEnumList<HtmlType>().Select(o => o.ToString().ToLower()))
        {
            if (unRemoveTags.Contains(tag))
                continue;

            regex = @"<" + tag + @"([^>])*>(\w|\W)*?</" + tag + "([^>])*>";
            html = RemoveHTMLByPattern(html, regex);
        }

        foreach (var tag in EnumExtension.ToEnumList<HtmlElement>().Select(o => o.ToString().ToLower()))
        {
            if (unRemoveTags.Contains(tag))
                continue;

            regex = @"<[\/]{0,1}(" + tag + @" [^<>]*>)|<[\/]{0,1}(" + tag + ">)";
            html = RemoveHTMLByPattern(html, regex);
        }

        return html;
    }

    /// <summary>
    /// 移除JScript代码
    /// </summary>
    /// <param name="html"></param>
    /// <returns></returns>
    public static string RemoveStyle(this string html) => RemoveHTMLByPattern(html, @"(<style){1,}[^<>]*>[^\0]*(<\/style>){1,}");

    /// <summary>
    /// 移除JScript代码
    /// </summary>
    /// <param name="html"></param>
    /// <returns></returns>
    public static string RemoveJS(this string html) => RemoveHTMLByPattern(html, @"(<script){1,}[^<>]*>[^\0]*(<\/script>){1,}");

    /// <summary>
    /// 移除Iframe代码
    /// </summary>
    /// <param name="html"></param>
    /// <returns></returns>
    public static string RemoveIframe(this string html) => RemoveHTMLByPattern(html, @"(<iframe){1,}[^<>]*>[^\0]*(<\/iframe>){1,}");

    /// <summary>
    /// 移除标记
    /// </summary>
    /// <param name="html"></param>
    /// <param name="pattern"></param>
    /// <returns></returns>
    public static string RemoveHTMLByPattern(this string html, string pattern) => html.IsNotEmptyString() ? Regex.Replace(html.Trim(), pattern, string.Empty) : string.Empty;
}

internal enum HtmlType
{
    style,      // 定义文档的样式信息
    script,     // 定义客户端脚本
}

internal enum HtmlElement
{
    a,              // 定义锚
    abbr,           // 定义缩写
    acronym,        // 定义只取首字母的缩写
    address,        // 定义文档作者或拥有者的联系信息
    applet,         // 不赞成使用，定义嵌入的 applet
    area,           // 定义图像地图内部的区域
    b,              // 定义粗体文本
    baseont,        // 不赞成使用，定义页面中文本的默认字体、颜色或尺寸
    bdo,            // 定义文本的方向
    big,            // 定义大号文本
    blockquote,     // 定义块引用
    body,           // 定义文档的主体
    br,             // 定义简单的折行
    button,         // 定义按钮
    caption,        // 定义表格标题
    center,         // 不赞成使用，定义居中文本
    cite,           // 定义引用（citation）
    code,           // 定义计算机代码文本
    col,            // 定义表格中一个或多个列的属性值
    colgroup,       // 定义表格中供格式化的列组
    dd,             // 定义定义列表中项目的描述
    del,            // 定义被删除文本
    dir,            // 不赞成使用，定义目录列表
    div,            // 定义文档中的节
    dl,             // 定义定义列表
    dn,             // （应为 dt）定义定义项目（疑为笔误）
    dt,             // 定义定义列表中的项目
    em,             // 定义强调文本
    fieldset,       // 定义围绕表单中元素的边框（原 ieldset，应为 fieldset）
    form,           // 定义供用户输入的 HTML 表单
    h1,             // 定义 HTML 一级标题
    h2,             // 定义 HTML 二级标题
    h3,             // 定义 HTML 三级标题
    h4,             // 定义 HTML 四级标题
    h5,             // 定义 HTML 五级标题
    h6,             // 定义 HTML 六级标题
    head,           // 定义关于文档的信息
    hr,             // 定义水平线
    html,           // 定义 HTML 文档
    i,              // 定义斜体文本
    iframe,         // 定义内联框架
    img,            // 定义图像
    input,          // 定义输入控件
    ins,            // 定义被插入文本
    isindex,        // 不赞成使用，定义与文档相关的可搜索索引
    kbd,            // 定义键盘文本
    label,          // 定义 input 元素的标注
    legend,         // 定义 fieldset 元素的标题（原为 ieldset，应为 fieldset）
    li,             // 定义列表的项目
    link,           // 定义文档与外部资源的关系
    map,            // 定义图像映射
    menu,           // 不赞成使用，定义菜单列表
    meta,           // 定义关于 HTML 文档的元信息
    norames,        // （应为 noframes）定义针对不支持框架的用户的替代内容
    noscript,       // 定义针对不支持客户端脚本的用户的替代内容
    //object,         // 定义嵌入的对象
    ol,             // 定义有序列表
    ont,            // 不赞成使用，定义文本的字体、尺寸和颜色
    optgroup,       // 定义选择列表中相关选项的组合
    option,         // 定义选择列表中的选项
    oot,            // （应为 tfoot）定义表格中的表注内容（脚注）（疑为笔误）
    p,              // 定义段落
    param,          // 定义对象的参数
    pre,            // 定义预格式文本
    q,              // 定义短的引用
    script,         // 定义客户端脚本（可补充）
    select,         // 定义选择列表（下拉列表）
    small,          // 定义小号文本
    span,           // 定义文档中的节
    s,              // 不赞成使用，定义加删除线的文本
    samp,           // 定义计算机代码样本
    strike,         // 不赞成使用，定义加删除线的文本
    strong,         // 定义语气更为强烈的强调文本
    sub,            // 定义下标文本
    sup,            // 定义上标文本
    table,          // 定义表格
    tbody,          // 定义表格中的主体内容
    td,             // 定义表格中的单元
    textarea,       // 定义多行的文本输入控件
    tfoot,          // 定义表格的页脚（修正 oot）
    th,             // 定义表格中的表头单元格
    thead,          // 定义表格中的表头内容
    title,          // 定义文档的标题
    tr,             // 定义表格中的行
    tt,             // 定义打字机文本
    u,              // 不赞成使用，定义下划线文本
    ul,             // 定义无序列表
    var,            // 定义文本的变量部分
    xmp             // 不赞成使用，定义预格式文本
}
