namespace Company.Pro.PL.Helpers
{
    public static class DocumentSetting
    {
        // In This Class We Will Put Two Static Methods 

        #region Method To Upload Any File

        // 1. Upload Any File 
        public static string UploadFile(IFormFile file, string folderName)
        {
            // IFormFile => To Upload Any File
            // folderName => To Specify The Folder Name To Save The File In It
            // 1. Get Folder Path:
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName);

            // 2. Get File Name && Make Sure It Unique:
            var fileName = $"{Guid.NewGuid()}_{file.FileName}"; // To Make Sure The File Name Is Unique

            // 3. Get File Path(Full Path):
            var fullPath = Path.Combine(folderPath, fileName);

            // 4. File Stream:
            using var fileStream = new FileStream(fullPath, FileMode.Create); // FileMode.Create => To Create The File If Not Exist + UnManaged Resource

            // 5. Copy File To File Stream:
            file.CopyTo(fileStream);

            // 6. Return File Name To Save It In Database:
            return fileName; //Guid
        }
        #endregion


        #region Method To Delete Any File

        // 2. Delete Any File
        public static void DeleteFile(string fileName, string folderName)
        {
            // fileName => To Specify The File Name To Delete It
            // folderName => To Specify The Folder Name To Get The File From It

            // 1. Get File Path:
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName, fileName);

            // 2. Check If File Exist:
            if (File.Exists(filePath))
            {
                // 3. Delete File:
                File.Delete(filePath);
            }
        }
        #endregion


    }
}
