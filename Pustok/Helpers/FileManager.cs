namespace Pustok.Helpers
{
    public static class FileManager
    {
        public static string SaveFile(string rooPath,string foldername,IFormFile file)
        {
            string name = file.FileName;
            name = name.Length > 64 ? name.Substring(name.Length - 64, 64) : name;
            name = Guid.NewGuid().ToString() + name;
            string savepath = Path.Combine(rooPath, foldername, name);

            using (FileStream fs = new FileStream(savepath,FileMode.Create))
            {
                file.CopyTo(fs);
            }
            return name;
        }
        public static void DeleteFile(string rootPath,string foldename,string filename)
        {
            string deletepath = Path.Combine(rootPath, foldename, filename);
                if (System.IO.File.Exists(deletepath))
                {
                    System.IO.File.Delete(deletepath);
                }
        }
    }
}
