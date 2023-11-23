using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace be_quanlytour.Helper
{  

    public static class Utils
    {
        public static async Task<string> SaveIMG(IWebHostEnvironment hostingEnvironment, string name, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, "Images");
                var fileName = $"{name}{Path.GetExtension(file.FileName)}";

                var fileExtension = Path.GetExtension(fileName);
                if (!string.Equals(fileExtension, ".jpg", StringComparison.OrdinalIgnoreCase))
                {
                    fileName = $"{Path.GetFileNameWithoutExtension(fileName)}.jpg";
                }

                var filePath = Path.Combine(uploads, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                return fileName;
            }

            return null;
        }
        public static string TaoMaTuDong(int number)
        {

            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string randomString = new string(Enumerable.Repeat(chars, number).Select(s => s[random.Next(s.Length)]).ToArray());                   
            return randomString;
        }

        public static string GetIMGAsBase64(IWebHostEnvironment hostingEnvironment, string imageName)
        {
            var imagePath = Path.Combine(hostingEnvironment.WebRootPath, "Images", imageName);
            if (File.Exists(imagePath))
            {
                byte[] imageBytes = File.ReadAllBytes(imagePath);
                return Convert.ToBase64String(imageBytes);
            }

            return null;
        }
        public static void DeleteIMG(IWebHostEnvironment hostingEnvironment, string imageName)
        {
            if (imageName != null)
            {
                var imagePath = Path.Combine(hostingEnvironment.WebRootPath, "Images", imageName);
                if (File.Exists(imagePath))
                {
                    try
                    {
                        File.Delete(imagePath);
                    }
                    catch (IOException ex)
                    {
                       
                        Console.WriteLine($"Error deleting image: {ex.Message}");
                    }
                }
            }
        }
    }

}
