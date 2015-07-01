using System;

namespace QsTech.Authentication.Sso.Clients
{
    static class PatternMatcherFactory
    {
        public static IPatternMatcher GetMatch(PatternMatchType type)
        {
            switch (type)
            {
                case PatternMatchType.Head:
                    return new MatchPatternHead();
                    break;
                case PatternMatchType.Any:
                    return new MatchPaternAny();
                    break;
                case PatternMatchType.Regex:
                    return new MatchRegex();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("type");
            }
        }
    }
}