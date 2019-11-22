/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */

using System;
using System.Diagnostics;
using Tizen.NUI;
using Tizen.NUI.UIComponents;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Constants;

class WidthHeightSpecificationsTests : NUIApplication
{

    private TextLabel testStatus;

    /// <summary>
    /// Override to create the required scene
    /// </summary>
    protected override void OnCreate()
    {
        // Up call to the Base class first
        base.OnCreate();

        // Get the window instance and change background color
        Window window = Window.Instance;
        window.BackgroundColor = Color.White;
        testStatus = new TextLabel();
        testStatus.Text = "Starting Tests";
        window.Add(testStatus);
        runTests();
    }

    private void runTests()
    {
        SpecificationTests1();
        SpecificationTests2();
        SpecificationTests3();
        SpecificationTests4();
        SpecificationTests5();
        SpecificationTests6();

        testStatus.Text = "Tests Complete";
    }

    private void SpecificationTests1()
    {
        Console.Write(System.Reflection.MethodBase.GetCurrentMethod().Name);
        View view = new View();
        view.BackgroundColor = Color.Blue;
        Window window = Window.Instance;
        window.Add(view);

        view.WidthSpecification = LayoutParamPolicies.MatchParent;
        view.HeightSpecification = LayoutParamPolicies.MatchParent;

        Debug.Assert(view.WidthSpecification == LayoutParamPolicies.MatchParent);
        Debug.Assert(view.HeightSpecification == LayoutParamPolicies.MatchParent);

        window.Remove(view);
        Console.WriteLine(":Passed");
    }

    private void SpecificationTests2()
    {
        Console.Write(System.Reflection.MethodBase.GetCurrentMethod().Name);
        View view = new View();
        view.BackgroundColor = Color.Blue;
        Window window = Window.Instance;
        window.Add(view);

        view.WidthSpecification = LayoutParamPolicies.MatchParent;
        view.HeightSpecification = 60;

        Debug.Assert(view.WidthSpecification == LayoutParamPolicies.MatchParent);
        Debug.Assert(view.HeightSpecification == 60);

        window.Remove(view);
        Console.WriteLine(":Passed");
    }

    private void SpecificationTests3()
    {
        Console.Write(System.Reflection.MethodBase.GetCurrentMethod().Name);
        View view = new View();
        view.BackgroundColor = Color.Blue;
        Window window = Window.Instance;
        window.Add(view);

        view.WidthSpecification = 60;
        view.HeightSpecification = LayoutParamPolicies.MatchParent;

        Debug.Assert(view.WidthSpecification == 60);
        Debug.Assert(view.HeightSpecification == LayoutParamPolicies.MatchParent);

        window.Remove(view);
        Console.WriteLine(":Passed");
    }

    private void SpecificationTests4()
    {
        Console.Write(System.Reflection.MethodBase.GetCurrentMethod().Name);
        View view = new View();
        view.BackgroundColor = Color.Blue;
        Window window = Window.Instance;
        window.Add(view);

        view.WidthSpecification = LayoutParamPolicies.MatchParent;
        view.HeightSpecification = 60;

        Debug.Assert(view.WidthSpecification == LayoutParamPolicies.MatchParent);
        Debug.Assert(view.HeightSpecification == 60);

        view.WidthSpecification = LayoutParamPolicies.WrapContent;

        Debug.Assert(view.WidthSpecification == LayoutParamPolicies.WrapContent);
        Debug.Assert(view.HeightSpecification == 60);

        window.Remove(view);
        Console.WriteLine(":Passed");
    }

    private void SpecificationTests5()
    {
        Console.Write(System.Reflection.MethodBase.GetCurrentMethod().Name);
        View view = new View();
        view.BackgroundColor = Color.Blue;
        Window window = Window.Instance;
        window.Add(view);

        view.WidthSpecification = LayoutParamPolicies.MatchParent;
        view.HeightSpecification = 60;

        Debug.Assert(view.WidthSpecification == LayoutParamPolicies.MatchParent);
        Debug.Assert(view.HeightSpecification == 60);

        view.WidthSpecification = 80;

        Debug.Assert(view.WidthSpecification == 80);
        Debug.Assert(view.HeightSpecification == 60);

        window.Remove(view);
        Console.WriteLine(":Passed");
    }

    private void SpecificationTests6()
    {
        Console.Write(System.Reflection.MethodBase.GetCurrentMethod().Name);
        View view = new View();
        view.BackgroundColor = Color.Blue;
        Window window = Window.Instance;
        window.Add(view);

        view.WidthSpecification = LayoutParamPolicies.MatchParent;
        view.HeightSpecification = 60;

        Debug.Assert(view.WidthSpecification == LayoutParamPolicies.MatchParent);
        Debug.Assert(view.HeightSpecification == 60);

        view.WidthSpecification = 80;

        Debug.Assert(view.WidthSpecification == 80);
        Debug.Assert(view.HeightSpecification == 60);

        view.Size2D = new Size2D(40,50);
        Debug.Assert(view.Size2D.Width == 40);
        Debug.Assert(view.Size2D.Height == 50);

        window.Remove(view);
        Console.WriteLine(":Passed");
    }

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread] // Forces app to use one thread to access NUI
    static void Main(string[] args)
    {
        WidthHeightSpecificationsTests example = new WidthHeightSpecificationsTests();
        example.Run(args);
    }
}

