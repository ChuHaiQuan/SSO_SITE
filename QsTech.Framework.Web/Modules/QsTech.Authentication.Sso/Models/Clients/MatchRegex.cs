using System.Text.RegularExpressions;

namespace QsTech.Authentication.Sso.Clients
{
    class MatchRegex : IPatternMatcher
    {
        public bool Match(string url, string pattern)
        {
            var reg = new Regex(pattern);
            return reg.IsMatch(url);
        }
    }
}