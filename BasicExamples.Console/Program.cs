using System;

namespace BasicExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            BasicTests.TestLoadDicomFile();
            BasicTests.TestJPEGLSNearLosslessEncode();
            Console.Read();
        }
    }
}
