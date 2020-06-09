using System.Collections.Generic;
using System;

namespace WearableSample
{
    public class Message
    {
        public string Sender{get;set;}
        public string Text{get;set;}
        public string Time{get;set;}
    }


    public class MessageDummy
    {
        public static List<object> Create(int count)
        {
            List<object> result = new List<object>();

            string[] namePool = {
                "Gail Forcewind",
                "Mario Speedwagon",
                "Anna Mull",
                "Aaron Spacemuseum",
                "Chris P. Cream"
            };

            string[] textPool = {
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse mattis dignissim massa eu aliquam. Vivamus turpis augue, tincidunt non justo non, condimentum pretium lacus. Nulla venenatis sed augue id mattis. Morbi venenatis faucibus purus nec tincidunt. Nam ac malesuada leo, eu eleifend felis. Curabitur sed dui placerat ex suscipit ultrices vitae quis dui. In non dui id ante eleifend vestibulum ut non est. Morbi volutpat libero consectetur tempus accumsan.",
                "Maecenas vitae porta lacus, sed euismod turpis. Proin sit amet lacus eu metus aliquam pellentesque. In hac habitasse platea dictumst. Vestibulum pharetra, mi a tempor venenatis, enim mauris bibendum lacus, mattis ullamcorper dui urna sit amet augue. Sed viverra velit sit amet magna laoreet vulputate. Praesent vitae lorem pellentesque, vestibulum ligula quis, scelerisque neque. Interdum et malesuada fames ac ante ipsum primis in faucibus. Cras bibendum iaculis feugiat. Phasellus euismod nulla et turpis tincidunt, nec rutrum nulla accumsan. Aliquam in tortor sagittis mi condimentum volutpat id nec turpis. Aliquam dapibus ipsum leo, eget congue nulla interdum sed. Praesent nec mauris leo.",
                "Nulla luctus eget mauris ac dapibus. Donec eu arcu et neque tincidunt fermentum. In sodales sit amet neque vitae aliquam. Maecenas eget justo est. Sed molestie leo eget consectetur vestibulum. Fusce massa magna, efficitur eu pellentesque eu, consectetur in est. Proin in porta nulla. Curabitur ullamcorper est in est sollicitudin, a tincidunt nulla tempor.",
                "Morbi rhoncus fringilla fringilla. Curabitur sagittis erat a nisi rhoncus scelerisque. Maecenas ac risus accumsan, cursus diam quis, lobortis purus. Praesent rutrum accumsan sem, eu semper ligula feugiat a. In dignissim, tellus in semper lacinia, ex sapien condimentum elit, a dignissim tortor velit et leo. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Nulla porta libero non gravida interdum. Cras imperdiet euismod dolor vitae pellentesque. Curabitur eu nulla non erat ullamcorper tempor eu eget ex.",
                "Maecenas sit amet dui nulla. Sed ut aliquam eros. In condimentum massa tincidunt, accumsan lacus vitae, vestibulum eros. Aenean porta dolor ipsum, nec varius felis porta bibendum. Quisque sollicitudin ante est, a vulputate sapien tincidunt vel. Aenean maximus ex at venenatis placerat. Duis dui dolor, maximus ac quam et, semper convallis nunc.",
            };

            string[] timePool = {
                "12:01 AM",
                "04:00 AM",
                "08:39 AM",
                "11:20 PM",
                "07:45 PM"
            };


            Random rand = new Random();

            for(int i = 0 ; i < count ; i++)
            {
                result.Add(new Message()
                {
                    Sender = namePool[rand.Next(5)],
                    Text = textPool[rand.Next(5)],
                    Time = timePool[rand.Next(5)],
                });
            }

            return result;
        }
    }
}