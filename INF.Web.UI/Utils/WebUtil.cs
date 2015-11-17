using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using INF.Web.UI.Shopping;
using Microsoft.VisualBasic;

namespace INF.Web.UI.Utils
{
    public static class WebUtil
    {

        public static string AspxPage(this Uri source)
        {
            if (source== null|| source.Segments.Length<2)
                return "";

            return (source.Segments[1]).ToLower();
        }

        public static string AspxPage(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return "";

            string aspxPage = "";
            var segments = source.Split('/');
            if (segments.Length > 0)
            {
                for (int index   = 0; index < segments.Length; index++)
                {
                    string seg = segments[index];
                    if (!string.IsNullOrEmpty(seg) && seg.Contains(".aspx"))
                    {
                        aspxPage = seg.Trim().ToLower();
                        break;
                    }
                }
            }

            return aspxPage;
        }

        public static string GetParameterValueAsString(HttpRequest request, string key)
        {
            if (request[key] != null)
            {
                return request[key];
            }
            return string.Empty;
        }

        public static int GetParameterValueAsInteger(HttpRequest request, string key)
        {

            if (request[key] != null)
            {
                object obj = request[key];
                if (Information.IsNumeric(obj))
                {
                    return Convert.ToInt32(obj);
                }
            }

            return 0;
        }

        public static bool IsWebsiteClosed()
        {
            bool websiteClosed = false;
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["PizzaWebConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("IsWebsiteClosed", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if ((reader.Read()))
                            {
                                websiteClosed = true;
                            }
                            else
                            {
                                websiteClosed = false;
                            }
                        }
                    }
                    catch
                    {
                        throw;
                    }
                }
            }

            return websiteClosed;
        }

        public static string BuildCssGradientBackgound(string vBaseColor, string vBackColor)
        {
            string vStyleStr = "background: " + vBackColor + ";" + "background: -moz-linear-gradient(top,  " + vBaseColor + " 0%, " + vBackColor + " 100%);" + "background: -webkit-gradient(linear, left top, left bottom, color-stop(0%," + vBaseColor + "), color-stop(100%," + vBackColor + "));" + "background: -webkit-linear-gradient(top,  " + vBaseColor + " 0%," + vBackColor + " 100%);" + "background: -o-linear-gradient(top,  " + vBaseColor + " 0%," + vBackColor + " 100%);" + "background: -ms-linear-gradient(top,  " + vBaseColor + " 0%," + vBackColor + " 100%);" + "background: linear-gradient(to bottom, " + vBaseColor + " 0%," + vBackColor + " 100%);" + "filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='" + vBackColor + "', endColorstr='" + vBackColor + "',GradientType=0 );";
            return vStyleStr;
        }

        public static void StoreValueToCookies(HttpContext vContext, string vKey, string vValue)
        {
            if ((vContext == null) || (vContext.Response == null) || (vContext.Response.Cookies == null))
            {
                return;
            }

            if ((vKey == null) || string.IsNullOrWhiteSpace(vKey))
            {
                return;
            }

            HttpCookie ckPostCode = new HttpCookie(BxShoppingCart.CK_POST_CODE, vValue);
            ckPostCode.Expires = DateTime.Now.AddHours(1);
            vContext.Response.Cookies.Add(ckPostCode);
        }

        public static string RetrieveValueFromCookies(HttpContext vContext, string vKey, string vValueDefault = "")
        {
            string strVal = vValueDefault;
            if ((vContext == null) || (vContext.Request == null) || (vContext.Request.Cookies == null))
            {
                return strVal;
            }

            if ((vKey == null) || string.IsNullOrWhiteSpace(vKey))
            {
                return strVal;
            }

            if ((vContext.Request.Cookies.Get(vKey) != null))
            {
                strVal = vContext.Request.Cookies.Get(vKey).Value;
            }
            return strVal;
        }

        public static int COOKIES_TIME_OUT_IN_MINUTES = 60;
    }
}