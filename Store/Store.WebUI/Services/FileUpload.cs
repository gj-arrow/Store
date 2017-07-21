using System;
using System.IO;
using System.Web;

namespace Store.WebUI.Services
{
        public static class FileUpload
        {
            public static bool isPicture(string filename)
            {
                string[] imgFormats = new string[] { ".jpg", ".jpeg", ".png", ".gif" };
                return !Array.Exists(imgFormats, element => element == filename.ToLower());
            }

            public static string getFilename(string filename)
            {
                string fn = filename;
                int count = 1;
                while (File.Exists(Path.Combine(HttpContext.Current.Server.MapPath("~/Images"), fn)))
                {
                    fn = insertIndexInFilename(fn, count);
                    count++;
                }
                return fn;
            }

            public static string insertIndexInFilename(string filename, int index)
            {
                if (index > 1)
                {
                    filename = filename.Remove(filename.LastIndexOf('.') - index.ToString().Length - 1, index.ToString().Length + 1);
                }

                return filename.Insert(filename.LastIndexOf('.'), "_" + index);
            }
    }
}