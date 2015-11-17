using System.Web;
using System.Web.UI;

namespace INF.Web.UI
{
    public static class HttpResponseExtensions
    {
        public static void RedirectTo(this HttpResponse response, string url)
        {
            if (response == null)
                return;
            if (response.IsRequestBeingRedirected)
                return;


            // Read basket temp key from cookies
            //HttpCookie ck = response.Cookies["AspNet_infinitysol"];
            //if( ck != null)
            //{
            //    // This case would be the cookie is expired,
            //    // so let redirect to ErrorPage
            //    object objKey = ck.Values[WebConstants.PLACING_ORDER_TEMP_BASKET_ID];
            //    if (objKey != null && !string.IsNullOrEmpty(objKey.ToString()))
            //    {
            //        ck.Values[WebConstants.PLACING_ORDER_TEMP_BASKET_ID] = objKey.ToString();
            //        response.Cookies.Add(ck);
            //    }
            //}
            response.Redirect(url, false);
            var context = HttpContext.Current;
            if (context != null && context.ApplicationInstance != null)
            {
                context.ApplicationInstance.CompleteRequest();
            }
        }
    }
}