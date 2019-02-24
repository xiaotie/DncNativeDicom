using System;

namespace BasicExamples
{
    using Dicom;

    public class BasicTests
    {
        public static void TestLoadDicomFile()
        {
            TestLoadDicomImageFile(
                @"./TestData/GH342.dcm",
                @"./TestData/GH538-jpeg14sv1.dcm"
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
    }
}
