using NUnit.Framework;


namespace Common.Tests
{
    public class AssertUtils
    {
        public static void AreNearlyEqual(float expected, float actual)
        {
            if (!Util.NearlyEqual(expected, actual))
            {
                throw new AssertionException(string.Format("Expected: {0:G17}\nBut was: {1:G17}", expected, actual));
            }
        }

    }
}
