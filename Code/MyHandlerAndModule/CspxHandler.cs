using System.Web;

namespace MyHandlerAndModule
{
    public class CspxHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            context.Response.Write("hello http handler");
        }
    }
}