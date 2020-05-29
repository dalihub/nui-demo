using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace NUIWHMain
{
    class NUISampleDllLoader
    {
        private delegate View TheMethodDelegate();
        private Assembly assembly;

        public NUISampleDllLoader(string dllPath)
        {
            assembly = Assembly.LoadFile(dllPath);
        }


        //Class - "Tizen.NUI.ComponentsSample.NUISampleCreator",
        //Function -"CreateSampleView" 
        public View CreateDllView(string className, string functionName)
        {
            Module[] modules = assembly.GetModules();
            Type type = modules[0].GetType(className);
            MethodInfo minfo = type.GetMethod(functionName);
            TheMethodDelegate dllViewCreator = (TheMethodDelegate)Delegate.CreateDelegate(typeof(TheMethodDelegate), minfo);
            return dllViewCreator();
        }
    }
}
