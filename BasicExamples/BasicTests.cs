using System;
using System.IO;

namespace BasicExamples
{
    using Dicom;
    using Dicom.Imaging.Codec;

    public class BasicTests
    {
        public static void TestLoadDicomFile()
        {
            TestLoadDicomImageFile(
                @"./TestData/GH342.dcm",
                @"./TestData/GH538-jpeg14sv1.dcm"
                );
        }

        public static void TestJPEGLSNearLosslessEncode()
        {
            TestJPEGLSNearLosslessEncode(
                @"./TestData/GH342.dcm"
                );
        }

        private static void TestLoadDicomImageFile(params String[] otherFiles)
        {
            if (otherFiles == null) return;
            foreach (var item in otherFiles) TestLoadDicomImageFileCore(item);
        }

        private static void TestLoadDicomImageFileCore(String filePath)
        {
            try
            {
                DicomFile dicomFile = DicomFile.Open(filePath);
                bool isContainsPixelData = dicomFile.Dataset.Contains(DicomTag.PixelData);
                double rescaleIntercept = dicomFile.Dataset.Get<double>(DicomTag.RescaleIntercept, 0.0);
                double rescaleSlope = dicomFile.Dataset.Get<double>(DicomTag.RescaleSlope, 1.0);
                Console.WriteLine($"Contains PixelData: {isContainsPixelData},Rescale Slope: {rescaleSlope}, Rescale Intercept: { rescaleIntercept}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private static void TestJPEGLSNearLosslessEncode(String filePath)
        {
            DicomFile dicomFile = DicomFile.Open(filePath);
            var lossyFile = dicomFile.Clone(DicomTransferSyntax.JPEGLSNearLossless,
                new DicomJpegLsParams { AllowedError = 12 });
            using (MemoryStream ms = new MemoryStream())
            {
                lossyFile.Save(ms);
                Console.WriteLine(ms.Length / 1000);
            }
        }
    }
}
