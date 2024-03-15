
using Microsoft.AspNetCore.Http;
using Xsport.Common.Constants;
using Microsoft.AspNetCore.Hosting;
using System.Reflection.Emit;


namespace Xsport.Common.Utils;
public static class Utils
{

    public static async Task<string> UploadImageFileAsync(IFormFile file, long loggedInUserId, IWebHostEnvironment env)
    {
        string folderPath = env.WebRootPath + XsportConstants.ImagesPath;

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        if (file != null)
        {
            string extension = System.IO.Path.GetExtension(file.FileName);
            string fileName = $"{loggedInUserId}_{DateTime.UtcNow.Ticks.ToString()}{extension}";

            // CHECK IF THE SELECTED FILE ALREADY EXISTS IN FOLDER. (AVOID DUPLICATE)
            if (!File.Exists(folderPath + fileName))
            {
                //FILE DETAILED PATH
                string filePath = folderPath + fileName;
                // SAVE THE FILES IN THE FOLDER.
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);

                    // check if the file is less than max file size
                    if (fileStream.Length > XsportConstants.MaxFileSize)
                    {
                        throw new InvalidDataException("The file is too large.");
                    }
                }
            }
            return fileName;
        }
        else
            throw new InvalidDataException("No file provided to upload");
    }

    public static decimal CalculateDistanceBetweenTowUsers(decimal sLatitude, decimal sLongitude, decimal eLatitude, decimal eLongitude)
    {
        // Convert latitude and longitude from degrees to radians
        var sLatRad = DegreesToRadians(sLatitude);
        var sLonRad = DegreesToRadians(sLongitude);
        var eLatRad = DegreesToRadians(eLatitude);
        var eLonRad = DegreesToRadians(eLongitude);

        // Haversine formula
        var dLat = eLatRad - sLatRad;
        var dLon = eLonRad - sLonRad;
        var a = Math.Sin((double)dLat / 2) * Math.Sin((double)dLat / 2) +
                Math.Cos((double)sLatRad) * Math.Cos((double)eLatRad) *
                Math.Sin((double)dLon / 2) * Math.Sin((double)dLon / 2);
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        var distanceKm = XsportConstants.EarthRadiusKm * c;

        // Convert distance to meters
        var distanceMeters = distanceKm * 1000;
        return Convert.ToDecimal(distanceMeters);
    }
    private static decimal DegreesToRadians(decimal degrees)
    {
        return degrees * Convert.ToDecimal(Math.PI) / 180;
    }
}