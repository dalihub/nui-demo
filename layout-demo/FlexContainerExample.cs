using System;
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.UIComponents;

/*
* Copyright (c) 2018 Samsung Electronics Co., Ltd.
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
using System.Threading.Tasks;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace LayoutDemo
{
    class FlexContainerExample : Example
    {
        private FlexContainer flexContainer;

        public override void Create()
        {
            Initialize();
            //InitializeWithFlexLayout();
        }
        public void Initialize()
        {
            Window window = Window.Instance;

            flexContainer = new FlexContainer();
            flexContainer.Size2D = new Size2D(480, 800);
            flexContainer.FlexDirection = FlexContainer.FlexDirectionType.Column;
            flexContainer.FlexWrap = FlexContainer.WrapType.Wrap;
            flexContainer.Name = "FlexContainer";
            flexContainer.BackgroundColor = Color.Green;

            for(int i=0; i< 50; i++) {
                TextLabel temp = new TextLabel("tl" + i);
                temp.BackgroundColor = (i%2==0)?Color.Red:Color.Blue;
                temp.Size2D = new Size2D(100, 100);
                temp.Name = "textLabel";
                flexContainer.Add(temp);
            }
            window.Add(flexContainer);
        }

        public override void Remove()
        {
            Window window = Window.Instance;
            window.Remove(flexContainer);
            flexContainer = null;
        }
    }
}