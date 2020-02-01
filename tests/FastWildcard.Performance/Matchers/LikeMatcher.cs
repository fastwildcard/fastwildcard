#if !NETCOREAPP2_1
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace FastWildcard.Performance.Matchers
{
    public class LikeMatcher
    {
        public bool Match(string str, string pattern)
        {
            return LikeOperator.LikeString(str, pattern, CompareMethod.Text);
        }
    }
}
#endif