using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace GameStore.WebUI.Utils
{
    public static class ImageManager
    {
        private const string FileExtensionPattern = @"^(\.png)|(\.jpg)|(\.jpeg|(\.gif))$";
        private const string ImagesFolder = "~/Content/Images/Loaded/";

        public static bool IsImage(HttpPostedFileBase file)
        {
            string fileExtension = Path.GetExtension(file.FileName);

            return Regex.IsMatch(fileExtension, FileExtensionPattern);
        }

        public static bool IsImage(HttpPostedFile file)
        {
            string fileExtension = Path.GetExtension(file.FileName);

            return Regex.IsMatch(fileExtension, FileExtensionPattern);
        }

        public static string SaveImage(HttpPostedFileBase image, Func<string, string> mapPath)
        {
            string serverImagePath = GetServerImageName(image.FileName);
            image.SaveAs(mapPath(serverImagePath));

            return serverImagePath;
        }

        public static Task<string> SaveImageAsync(HttpPostedFileBase image, Func<string, string> mapPath)
        {
            string serverImagePath = GetServerImageName(image.FileName);

            return Task.Run(() =>
            {
                image.SaveAs(mapPath(serverImagePath));
                return serverImagePath;
            });
        }

        public static Task<string> SaveImageAsync(HttpPostedFile image, Func<string, string> mapPath)
        {
            string serverImagePath = GetServerImageName(image.FileName);

            return Task.Run(() =>
            {
                image.SaveAs(mapPath(serverImagePath));
                return serverImagePath;
            });
        }

        private static string GetServerImageName(string fileName)
        {
            string imageName = Path.GetFileName(fileName);
            string serverImagePath = ImagesFolder + imageName;
            return serverImagePath;
        }
    }
}