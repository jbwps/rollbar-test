using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    public class TestService : ITestService
    {
        public void ThrowError()
        {
            var message = $"Test Exception";

            throw new Exception(message);
        }
    }
}
