/*
 * Copyright (c) 2020 Samsung Electronics Co., Ltd.
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
 */

using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using System.Runtime.InteropServices;

namespace Mesh
{
    class MainApp : NUIApplication
    {
        public struct Vec2
        {
            float x;
            float y;
            public Vec2(float xIn, float yIn)
            {
                x = xIn;
                y = yIn;
            }
        }

        struct TexturedQuadVertex {
            public Vec2 position;
        }

        static readonly string VERTEX_SHADER = 
        "attribute mediump vec2 aPosition;\n"+
        "uniform mediump mat4 uMvpMatrix;\n"+
        "uniform mediump vec3 uSize;\n"+
        "varying mediump vec2 vTexCoord;\n"+
        "void main()\n"+
        "{\n"+
        "    vec4 pos=vec4(aPosition, 0.0, 1.0)*vec4(uSize,1.0);\n"+
        "    gl_Position = uMvpMatrix*pos;\n"+
        "    vTexCoord = aPosition+vec2(0.5);\n"+
        "}\n";

        static readonly string FRAGMENT_SHADER = 
        "uniform lowp vec4 uColor;\n"+
        "uniform sampler2D sTexture;\n"+
        "varying mediump vec2 vTexCoord;\n"+
        "void main()\n"+
        "{\n"+
        "    gl_FragColor = texture2D( sTexture, vTexCoord ) * uColor;\n"+
        "}\n";

        protected override void OnCreate()
        {
            base.OnCreate();
            Window window = NUIApplication.GetDefaultWindow();
            window.BackgroundColor = Color.Black;

            View view = new View()
            {
                Size = new Size(window.WindowSize)
            };

            window.Add(view);


            PropertyMap vertexFormat = new PropertyMap();
            vertexFormat.Add("aPosition", new PropertyValue((int)PropertyType.Vector2));
            PropertyBuffer vertexBuffer = new PropertyBuffer(vertexFormat);

            vertexBuffer.SetData( RectangleDataPtr(), 4 );   
            Geometry geometry = new Geometry();
            geometry.AddVertexBuffer(vertexBuffer);
            geometry.SetType(Geometry.Type.TRIANGLE_STRIP);
            Shader shader = new Shader(VERTEX_SHADER, FRAGMENT_SHADER);

            PixelData pixelData = PixelBuffer.Convert(ImageLoading.LoadImageFromFile(
                "./res/background_image.jpg",
                new Size2D(),
                FittingModeType.ScaleToFill
            ));
            Texture texture = new Texture(
                TextureType.TEXTURE_2D,
                pixelData.GetPixelFormat(),
                pixelData.GetWidth(),
                pixelData.GetHeight()
            );
            texture.Upload(pixelData);
            TextureSet textureSet = new TextureSet();
            textureSet.SetTexture(0u, texture);
            Renderer renderer = new Renderer(geometry, shader);
            renderer.SetTextures(textureSet);
            view.AddRenderer(renderer);
        }

        private global::System.IntPtr RectangleDataPtr()
        {
            TexturedQuadVertex v1 = new TexturedQuadVertex();
            TexturedQuadVertex v2 = new TexturedQuadVertex();
            TexturedQuadVertex v3 = new TexturedQuadVertex();
            TexturedQuadVertex v4 = new TexturedQuadVertex();
            v1.position= new Vec2(-0.5f, -0.5f);
            v2.position= new Vec2(-0.5f,  0.5f);
            v3.position= new Vec2( 0.5f, -0.5f);
            v4.position= new Vec2( 0.5f,  0.5f);

            TexturedQuadVertex[] texturedQuadVertexData = new TexturedQuadVertex[4] { v1, v2, v3, v4 };

            int length = Marshal.SizeOf(v1);
            global::System.IntPtr pA = Marshal.AllocHGlobal(length * 4);

            for (int i = 0; i < 4; i++)
            {
                Marshal.StructureToPtr(texturedQuadVertexData[i], pA + i * length, true);
            }

            return pA;
        }

        /// The main entry point for the application.
        /// </summary>
        [STAThread] // Forces app to use one thread to access NUI
        static void Main(string[] args)
        {
            MainApp example = new MainApp();
            example.Run(args);
        }
 
    }
}