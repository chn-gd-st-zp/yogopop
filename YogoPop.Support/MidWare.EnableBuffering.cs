namespace YogoPop.Support;

public class EnableBufferingMiddleware
{
    private readonly RequestDelegate _next;

    public EnableBufferingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        // 启用请求体的缓冲区
        context.Request.EnableBuffering();

        await _next(context);
    }
}