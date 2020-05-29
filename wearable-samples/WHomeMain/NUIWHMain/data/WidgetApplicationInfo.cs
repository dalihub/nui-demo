using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace NUIWHMain
{
    class WidgetApplicationInfo
    {
        public static string app1 = "org.tizen.apptray-widget";
        public static string app2 = "org.tizen.alarm.widget";
        public static string app3 = "org.tizen.w-contacts.widget";
        public static string app4 = "org.tizen.example.NUIWidgetSample";

        public static List<string> LoadAllParameters()
        {
            List<string> widgetList = new List<string>();

            TypeInfo t = typeof(WidgetApplicationInfo).GetTypeInfo();
            IEnumerable<FieldInfo> pList = t.DeclaredFields;

            Tizen.Log.Error("MYLOG", "widget app list : ");
            foreach (FieldInfo p in pList)
            {
                object appName = p.GetValue(null);
                Tizen.Log.Error("MYLOG", "val : " + appName);
                widgetList.Add(appName as string);
                //Tizen.Log.Error("MYLOG", "p.Name : " + p.Name);
            }
            return widgetList;
        }
    }
}
