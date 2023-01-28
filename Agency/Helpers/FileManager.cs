namespace Agency.Helpers
{
    public static class FileManager
    {
        public static string Save(string root ,string folderName,IFormFile file)
        {
            string fileName = (file.FileName.Length > 24 ? Guid.NewGuid().ToString() + file.FileName.Substring(file.FileName.Length - 24, 24) : Guid.NewGuid().ToString() + file.FileName);
            string path = Path.Combine(root,folderName,fileName);
            using (FileStream fileStream = new FileStream(path,FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            return fileName;
        }
        public static void Delete(string root, string folderName,string fileName)
        {
            string path = Path.Combine(root, folderName, fileName);
            if(File.Exists(path)) 
            {
                File.Delete(path);
            }
        }
    }
}
