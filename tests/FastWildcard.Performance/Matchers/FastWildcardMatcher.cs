namespace FastWildcard.Performance.Matchers
{
    public class FastWildcardMatcher
    {
        public bool Match(string pattern, string str)
        {
            return FastWildcard.IsMatch(pattern, str);
        }
    }
}
