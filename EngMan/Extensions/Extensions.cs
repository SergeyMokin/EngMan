using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EngMan.Models;
using System.IO;

namespace EngMan.Extensions
{
    public static class Extensions
    {
        //save image to server
        public static string SaveImage(Image image)
        {
            var bytearr = new List<byte>();
            foreach (var ch in image.Data)
            {
                bytearr.Add(Convert.ToByte(ch));
            }
            var time = DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds;
            //path to folder with project
            var path = System.Web.Hosting.HostingEnvironment.MapPath(string.Format("~/uploads/" + time + image.Name));
            File.WriteAllBytes(path, bytearr.ToArray());
            return string.Format("http://localhost:58099/uploads/" + time + image.Name);
        }
    }
}