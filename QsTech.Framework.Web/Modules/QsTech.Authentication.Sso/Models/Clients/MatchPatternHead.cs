namespace QsTech.Authentication.Sso.Clients
{
    class MatchPatternHead : IPatternMatcher
    {
        public bool Match(string url, string pattern)
        {
            string trimedUrl = null;
            int i = -1;
            if ((i = url.IndexOf("://")) >= 0)
            {
                trimedUrl = url.Substring(i);
            }
            else
            {
                trimedUrl = url;
            }
            return url.StartsWith(pattern);
        }
    }
}