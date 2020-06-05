using System;
using System.Collections.Generic;
using System.Text;

namespace WearableGallerySample
{
    public class CommonResource
    {
        public class ResourceData
        {
            public String name;
            public String path;
            public ResourceData(string name, string path)
            {
                this.name = name;
                this.path = path;
            }
        }

        public static string GetResourcePath()
        {
            return Tizen.Applications.Application.Current.DirectoryInfo.Resource + "images/";
        }
    }
}