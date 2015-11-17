using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web;
using System.Web.Caching;

namespace INF.Web.Data.BLL
{
    public abstract class BaseBusinessLogic
    {
        protected const int MAXROWS = int.MaxValue;

        protected static Cache Cache
        {
            get { return HttpContext.Current.Cache; }
        }

        protected static IPrincipal CurrentUser
        {
            get { return HttpContext.Current.User; }
        }

        protected static string CurrentUserName
        {
            get
            {
                string userName = "";
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                    userName = HttpContext.Current.User.Identity.Name;
                return userName;
            }
        }

        protected static string CurrentUserIP
        {
            get { return HttpContext.Current.Request.UserHostAddress; }
        }

        protected static int GetPageIndex(int startRowIndex, int maximumRows)
        {
            if (maximumRows <= 0)
                return 0;
            return (int)Math.Floor((double)startRowIndex / (double)maximumRows);
        }

        protected static string ConvertNullToEmptyString(string input)
        {
            return (input ?? "");
        }

        /// <summary>
        /// Remove from the ASP.NET cache all items whose key starts with the input prefix
        /// </summary>
        protected static void PurgeCacheItems(string prefix)
        {
            prefix = prefix.ToLower();
            var itemsToRemove = new List<string>();

            IDictionaryEnumerator enumerator = BaseBusinessLogic.Cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Key.ToString().ToLower().StartsWith(prefix))
                    itemsToRemove.Add(enumerator.Key.ToString());
            }

            foreach (string itemToRemove in itemsToRemove)
                BaseBusinessLogic.Cache.Remove(itemToRemove);
        }

        /// <summary>
        /// Cache the input data, if caching is enabled
        /// </summary>
        protected static void CacheData(string key, object data)
        {
            if (WebSettings.Instance.EnableCaching && data != null)
            {
                BaseBusinessLogic.Cache.Insert(key, data, null,
                   DateTime.Now.AddSeconds(WebSettings.Instance.CacheDuration), TimeSpan.Zero);
            }
        }


    }
}