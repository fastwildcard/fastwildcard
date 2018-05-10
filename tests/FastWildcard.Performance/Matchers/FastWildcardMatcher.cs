namespace FastWildcard.Performance.Matchers
{
    public class FastWildcardMatcher
    {
        private FastWildcard _fastWildcard;

        public bool Match(string pattern, string str)
        {
            _fastWildcard = new FastWildcard();
            return _fastWildcard.IsMatch(pattern, str);
        }
    }
}
