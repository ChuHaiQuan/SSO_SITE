using System;

namespace QsTech.Authentication.Sso.Clients
{
    public class ClientAccessPolicy
    {
        public AccessMode AccessMode { get; set; }

        public string Pattern { get; set; }

        public PatternMatchType PatternMatchType { get; set; }

        public bool Match(string url)
        {
            var patternMatcher = PatternMatcherFactory.GetMatch(PatternMatchType);

            if(patternMatcher.Match(url, Pattern))
            {
                if (AccessMode ==  AccessMode.OnlySSL)
                {
                    var uri = new Uri(url);
                    return uri.Scheme.Equals("https", StringComparison.OrdinalIgnoreCase);
                }
                return true;

            }
            return false;
        }
    }
}