namespace Front_5.Extensions
{
    public static class FormFLExtensions
    {
        public static bool IsImage(this IFormFile file)
        {
            return file.ContentType == "image/jpg" ||
                   file.ContentType == "image/jpeg" ||
                   file.ContentType == "image/png" ||
                   file.ContentType == "image/gif";
        }

        public static bool IsSmaller(this IFormFile file, int mb)
        {
            return file.Length / 1024 / 1024 <= mb;
        }

        public async static Task<string> SaveFileAsync(this IFormFile file, string webroot, string folder)
        {
            var filename = Guid.NewGuid().ToString() + Path.GetFileName(file.FileName);

            string path = Path.Combine(webroot, folder, filename);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return filename;

        }

        public static void Delete(string webroot, string folder, string filename)
        {
            string path = Path.Combine(webroot, folder, filename);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

    }
}
