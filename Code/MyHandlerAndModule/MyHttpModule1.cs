using System;
using System.Web;

namespace MyHandlerAndModule
{
    public class MyHttpModule1 : IHttpModule
    {
        // In the Init function, register for HttpApplication 
        // events by adding your handlers.
        public void Init(HttpApplication application)
        {
            application.BeginRequest += Application_BeginRequest;
            application.EndRequest += Application_EndRequest;
        }

        private void Application_BeginRequest(Object source, EventArgs e)
        {
            // Create HttpApplication and HttpContext objects to access
            // request and response properties.
            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;
            context.Response.Write("<h1><font color=green>MyHttpModule1: Beginning of Request</font></h1><hr>");
        }

        private void Application_EndRequest(Object source, EventArgs e)
        {
            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;
            context.Response.Write("<hr><h1><font color=green>MyHttpModule1: End of Request</font></h1>");
        }

        public void Dispose()
        {
        }
    }
}