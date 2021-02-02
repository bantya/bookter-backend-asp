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
        public static List<string> Upload(string directory, int count, HttpServerUtilityBase server, HttpRequestBase request)
        {
            List<string> fnames = new List<string>();
            string filename = "";

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
                                    filename = Guid.NewGuid() + Path.GetExtension(file.FileName);

                                    file.SaveAs(
                                        Path.Combine(
                                            filePath,
                                            filename
                                        )
                                    );

                                    fnames.Add(Path.Combine(
                                            directory.Substring(1),
                                            filename
                                        ));

                                    //Images.Resize(
                                    //    file,
                                    //    Path.Combine(
                                    //        filePath,
                                    //        "small-" + Guid.NewGuid() + Path.GetExtension(file.FileName)
                                    //    ),
                                    //    50,
                                    //    100
                                    //);
                                }
                            }
                        } else
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                fnames = null;
            }

            return fnames;
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
