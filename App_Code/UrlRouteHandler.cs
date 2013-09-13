using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Xml;

/// <summary>
/// Summary description for UrlRouteHandler
/// </summary>
public class UrlRouteHandler
{
	public UrlRouteHandler()
    { }

    public static void SetUrlRoute()
    {
        XmlDocument document = new XmlDocument();
        string xmlPath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["urlRouteMap"].ToString());
        document.Load(xmlPath);
        XmlNodeList routes = document.SelectNodes("Routes/Route");
        foreach (XmlNode item in routes)
        {
            string routeName = item.Attributes["Name"].InnerText;
            string virtualPath = item.SelectSingleNode("VirtualPath").InnerText + "/";
            string physcialPath = item.SelectSingleNode("PhysicalPath").InnerText;

            RouteValueDictionary defaultValues = new RouteValueDictionary();
            XmlNodeList parameters = item.SelectNodes("Parameters/Parameter");
            foreach (XmlNode parameter in parameters)
            {
                string parameterName = parameter.InnerText;
                string defaultValue = parameter.Attributes["DefaultValue"].InnerText;
                virtualPath += "{" + parameterName + "}/";
                if (!String.IsNullOrEmpty(defaultValue))
                {
                    defaultValues.Add(parameterName, defaultValue);
                }
            }
            virtualPath = virtualPath.Substring(0, virtualPath.Length - 1);

            RouteTable.Routes.Add(routeName, new Route(virtualPath, defaultValues, new PageRouteHandler(physcialPath)));
        }
    }
}