using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Utils
{
    public class Files
    {
        public static bool Upload(string directory, int count, HttpServerUtilityBase server, HttpRequestBase request)
        {
            bool status = false;

            try
            {
                string filePath = server.MapPath(directory);

                CreateDir(filePath);

                HttpFileCollectionBase files = request.Files;

                if (files.Count > 0)
                {
                    for (int index = 0; index < files.Count; index++)
                    {
                        if (index < count)
                        {
                            HttpPostedFileBase file = files[index];

                            if (file != null && file.ContentLength > 0)
                            {
                                if (IsImage(file))
                                {

                                    file.SaveAs(
                                        Path.Combine(
                                            filePath,
                                            Guid.NewGuid() + Path.GetExtension(file.FileName)
                                        )
                                    );

                                    Images.Resize(
                                        file,
                                        Path.Combine(
                                            filePath,
                                            "small-" + Guid.NewGuid() + Path.GetExtension(file.FileName)
                                        ),
                                        50,
                                        100
                                    );
                                }
                            }
                        } else
                        {
                            break;
                        }
                    }
                }

                status = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return status;
        }

        private static bool IsImage(HttpPostedFileBase file)
        {
            if (
                Path.GetExtension(file.FileName).ToLower() == ".jpg" ||
                Path.GetExtension(file.FileName).ToLower() == ".jpeg" ||
                Path.GetExtension(file.FileName).ToLower() == ".png"
            )
            {
                return true;
            }

            return false;
        }

        private static bool CreateDir(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                return true;
            } catch(Exception e)
            {
                return false;
            }
        }
    }
}
