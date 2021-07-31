using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Mio.NhaCuaVui.HappySite.Service
{
    public class FileService
    {
        public string AddNewFile(List<IFormFile> files, string filePath)
        {
            if (files == null || files.Any() == false) return string.Empty;

            var filesResult = string.Empty;
            foreach(var file in files)
            {
                string fileExtentsion = file.FileName.Split('.').Last();

                string fileNameNew = Guid.NewGuid().ToString() + "." + fileExtentsion;
                try
                {
                    using (var fileStream = new FileStream(Path.Combine(filePath, fileNameNew), FileMode.Create))
                    {

                        file.CopyTo(fileStream);
                        filesResult = filesResult + ";" + "/Upload/Photo/" + fileNameNew;
                    }

                }
                catch
                {
                    
                }
            }

            return filesResult;
        }
        public string RemoveImageFromList(string images, string removeImage)
        {
            if (string.IsNullOrEmpty(images)) return images;
            if (string.IsNullOrEmpty(removeImage)) return images;

            var list = images.Split(";").ToList();

            list = list.Where(x => x.ToLower() != removeImage.ToLower()).ToList();

            var imagesResult = string.Empty;
            foreach(var item in list)
            {
                imagesResult = imagesResult + ";" + item;
            }

            return imagesResult;

        }

    }
}
