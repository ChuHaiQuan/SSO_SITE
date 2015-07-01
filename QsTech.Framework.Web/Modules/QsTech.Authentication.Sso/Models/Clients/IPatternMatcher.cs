namespace QsTech.Authentication.Sso.Clients
{
    public interface IPatternMatcher
    {
        bool Match(string url, string pattern);
    }
}