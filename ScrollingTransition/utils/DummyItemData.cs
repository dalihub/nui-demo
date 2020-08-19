using System;
using System.Collections.Generic;

namespace Demo
{
    class ItemData
    {
        public string Name { get; private set; }
        public string ImageUrl { get; private set; }
        public string Origin { get; private set; }
        public string Price { get; private set; }

        public static List<object> CreateDummyItemData(int amout)
        {
            List<object> result = new List<object>();

            ItemData[] namePool =
            {
                new ItemData() { Name = "[국내산] 바나나밸리 달콤한 바나나 1송이", ImageUrl = "itemImage.jpg", Origin = "국내산", Price = "2,980원"},
                new ItemData() { Name = "[국내산] 사인머스켓 1~2송이/박스", ImageUrl = "itemImage2.jpg", Origin = "국내산", Price = "24,900원"},
                new ItemData() { Name = "[국내산] 아삭 복숭아 7~12입/박스", ImageUrl = "itemImage3.jpg", Origin = "국내산", Price = "15,800원"},
                new ItemData() { Name = "Y-MART FRESH [국내산] 당찬사과 4-7입/봉", ImageUrl = "itemImage4.jpg", Origin = "국내산", Price = "9,900원"},
                new ItemData() { Name = "[필리핀산] Dole 스위티오바나나 1.3kg", ImageUrl = "itemImage5.jpg", Origin = "필리핀산", Price = "4,980원"},
                new ItemData() { Name = "Y-MART FRESH [국내산] 산 수박 10kg", ImageUrl = "itemImage6.jpg", Origin = "국내산", Price = "15,900원"},
                new ItemData() { Name = "Y-MART FRESH [국내산] 향기진한 성주참외 1.5kg", ImageUrl = "itemImage7.jpg", Origin = "국내산", Price = "9,980원"},
            };

            for(int i = 0; i<amout; i++)
            {
                Random r = new Random();
                result.Add(namePool[r.Next(namePool.Length)]);
            }

            return result;
        }
    }
}
