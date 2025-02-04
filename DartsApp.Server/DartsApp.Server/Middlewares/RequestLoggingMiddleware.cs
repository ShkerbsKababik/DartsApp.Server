using NLog;
using System.Text;

namespace DartsApp.Server.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly NLog.ILogger logger = LogManager.GetCurrentClassLogger();

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Log Request
            context.Request.EnableBuffering(); // Enable buffering to read the request body
            var requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
            context.Request.Body.Position = 0; // Reset the stream position for the next middleware

            var requestInfo = new StringBuilder();
            requestInfo.AppendLine($"Request Method: {context.Request.Method}");
            requestInfo.AppendLine($"Request Path: {context.Request.Path}");
            requestInfo.AppendLine($"Request Query: {context.Request.QueryString}");
            requestInfo.AppendLine($"Request Headers: {string.Join(", ", context.Request.Headers)}");
            requestInfo.AppendLine($"Request Body: {requestBody}");

            logger.Info(requestInfo.ToString());

            // Call the next middleware in the pipeline
            await _next(context);

            // Log Response
            var responseInfo = new StringBuilder();
            responseInfo.AppendLine($"Response Status Code: {context.Response.StatusCode}");
            responseInfo.AppendLine($"Response Headers: {string.Join(", ", context.Response.Headers)}");

            logger.Info(responseInfo.ToString());
        }
    }
}
