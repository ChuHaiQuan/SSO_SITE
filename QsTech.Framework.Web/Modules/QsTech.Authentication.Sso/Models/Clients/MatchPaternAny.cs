namespace QsTech.Authentication.Sso.Clients
{
    class MatchPaternAny : IPatternMatcher
    {
        public bool Match(string url, string pattern)
        {
            return url.Contains(pattern);
        }
    }
}