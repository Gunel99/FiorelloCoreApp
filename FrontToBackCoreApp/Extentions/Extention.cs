using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBackCoreApp.Extentions
{
    public static class Extention
    {
        public static bool IsImage(this IFormFile file)
        {
            return file.ContentType.Contains("image/");
        }

        public static bool MaxLengthFile(this IFormFile file, int kb)
        {
            return file.Length / 1024 >= kb;
        }

        public async static Task<string> SaveFile(this IFormFile file, string root, string folder)
        {
            string path = Path.Combine(root, folder);
            string photoName = Guid.NewGuid().ToString() + file.FileName;
            string resultPath = Path.Combine(path, photoName);

            using (FileStream fileStream = new FileStream(resultPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return photoName;
        }
    }
}
