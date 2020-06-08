using System;
using System.Collections.Generic;

namespace Example
{
    class PictureData
    {
        public PictureData(string filePath)
        {
            FilePath = filePath;
        }

        public string FilePath { get; set; }
    }

    class DummyData
    {
        public static List<object> CreateDummyPictureData(int amount)
        {
            List<object> result = new List<object>();
            for (int i = 0; i < amount; i++)
            {
                result.Add(new PictureData("./res/gallery-large-"+(i%20 + 1)+".jpg"));
            }

            return result;
        }
    }
}