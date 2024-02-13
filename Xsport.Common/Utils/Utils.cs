
using Microsoft.AspNetCore.Http;
using Xsport.Common.Constants;
using Microsoft.AspNetCore.Hosting;

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
}