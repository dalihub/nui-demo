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
using Tizen.NUI;
using Tizen.NUI.Components;
using Tizen.NUI.BaseComponents;
using System.Collections.Generic;

namespace Demo
{
    class HelloWorldExample : NUIApplication
    {
        /// <summary>
        /// Override to create the required scene
        /// </summary>
        protected override void OnCreate()
        {
            // Up call to the Base class first
            base.OnCreate();
            NUIApplication.GetDefaultWindow().BackgroundColor = Color.White;

            PageController.Instance.AddPage("Main",typeof(MainPage));
            PageController.Instance.AddPage("ItemDetail", typeof(ItemDetailPage));
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            HelloWorldExample example = new HelloWorldExample();
            example.Run(args);
        }
    }
}
