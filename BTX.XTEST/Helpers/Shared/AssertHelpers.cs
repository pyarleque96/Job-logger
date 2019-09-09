using System;
using Xunit;

namespace BTX.XTEST.Helpers.Shared
{
    public static class AssertHelpers
    {
        public static void DoesNotThrows<T>(Action action) where T : Exception
        {
            try
            {
                action();
            }
            catch (T)
            {
                Assert.True(false);
            }
            catch (Exception)
            {
                Assert.True(false);
            }

            Assert.True(true);
        }
    }
}
