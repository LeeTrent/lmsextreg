using System;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lmsextreg.Utils
{
    public static class PageModelUtil
    {
        public static string EnsureLocalUrl(PageModel pageModel, string passedInUrl)
        {
            /*************************************************************************************************
                The IsLocalUrl method protects users from being inadvertently redirected to a malicious site.
                You can log the details of the URL that was provided when a non-local URL is supplied in a
                situation where you expected a local URL. Logging redirect URLs may help in diagnosing
                redirection attacks.
            *************************************************************************************************/

            Console.WriteLine("[PageModelUtil.EnsureLocalUrl] passedInUrl: '" + passedInUrl + "'" );
            Console.WriteLine("[PageModelUtil.EnsureLocalUrl] passedInUrl IS NULL: " + (passedInUrl == null) );        

            string returnUrl = null;

            if  ( passedInUrl != null 
                    && pageModel.Url.IsLocalUrl(passedInUrl)
                )
            {
                Console.WriteLine("[PageModelUtil.EnsureLocalUrl] " + passedInUrl + " IS local");
                returnUrl = passedInUrl;
            }

            if ( passedInUrl != null )
            {
                Console.WriteLine("[PageModelUtil.EnsureLocalUrl] " +  passedInUrl + " IS NOT local - returning NULL");
            }

            return returnUrl;                  
        }
    }
}