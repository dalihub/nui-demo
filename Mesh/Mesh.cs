using System.Numerics;
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
        public struct Vec3
        {
            float x;
            float y;
            float z;
            public Vec3(float xIn, float yIn, float zIn)
            {
                x = xIn;
                y = yIn;
                z = zIn;
            }
        }

        struct TexturedQuadVertex {
            public Vec3 position;
            public Vec3 normal;
            public Vec2 texcoord;
        }

        static readonly string VERTEX_SHADER =
        "attribute mediump vec3 aPosition;\n"+
        "attribute mediump vec3 aNormal;\n"+
        "attribute mediump vec2 aTexCoord;\n"+
        "uniform mediump mat4 uMvpMatrix;\n"+
        "uniform mediump mat3 uNormalMatrix;\n"+
        "uniform mediump vec3 uSize;\n"+
        "varying mediump vec3 vNormal;\n"+
        "varying mediump vec2 vTexCoord;\n"+
        "varying mediump vec3 vPosition;\n"+
        "void main()\n"+
        "{\n"+
        //"    vec4 pos = vec4(aPosition, 1.0)*vec4(uSize,1.0);\n"+
        "    vec4 pos = vec4(aPosition, 1.0)*vec4(uSize.xy, 400.0 ,1.0);\n"+
        "    gl_Position = uMvpMatrix*pos;\n"+
        "    vPosition = aPosition;\n"+
        "    vNormal   = normalize(uNormalMatrix * aNormal);\n"+
        "    vTexCoord = aTexCoord;\n"+
        "}\n";

        static readonly string FRAGMENT_SHADER =
        "uniform lowp vec4 uColor;\n"+
        "uniform sampler2D sTexture;\n"+
        "varying mediump vec3 vNormal;\n"+
        "varying mediump vec2 vTexCoord;\n"+
        "varying mediump vec3 vPosition;\n"+
        "mediump vec3 uLightDir = vec3(2.0, 0.5, 1.0);\n"+
        "mediump vec3 uViewDir  = vec3(0.0, 0.0, 1.0);\n"+
        "mediump vec3 uAmbientColor = vec3(0.2, 0.2, 0.2);\n"+
        "mediump vec3 uDiffuseColor = vec3(0.8, 0.8, 0.8);\n"+
        "mediump vec3 uSpecularColor = vec3(0.5, 0.5, 0.5);\n"+
        "void main()\n"+
        "{\n"+
        "    mediump vec3 lightdir = normalize(uLightDir);\n"+
        "    mediump vec3 eyedir   = normalize(uViewDir);\n"+
        "    mediump vec4 texColor = texture2D( sTexture, vTexCoord ) * uColor;\n" +
        "    mediump float diffuse = min(max(-dot(vNormal, lightdir) + 0.1, 0.0), 1.0);\n"+
        "    mediump vec3 reflectdir = reflect(-lightdir, vNormal);\n"+
        "    mediump float specular = pow(max(0.0, dot(reflectdir, eyedir)), 50.0);\n"+
        "    mediump vec4 color = texColor * vec4(uAmbientColor + uDiffuseColor * diffuse, 1.0) + vec4(uSpecularColor, 0.0) * specular;\n"+
        "    gl_FragColor = color;\n"+
        "}\n";

        protected override void OnCreate()
        {
            base.OnCreate();
            Window window = NUIApplication.GetDefaultWindow();
            window.BackgroundColor = Color.Black;
            Layer layer = new Layer();
            layer.Behavior = Layer.LayerBehavior.Layer3D;

            window.Add(layer);

            //int windowWidth  = window.WindowSize.Width;
            //int windowHeight = window.WindowSize.Height;
            //int viewSize = (windowWidth < windowHeight ? windowWidth : windowHeight);
            int viewSize = 400;

            View view = new View()
            {
                Size = new Size(viewSize, viewSize, viewSize),
                PositionUsesPivotPoint = true,
                PivotPoint = PivotPoint.Center,
                ParentOrigin = ParentOrigin.Center,
            };

            layer.Add(view);


            PropertyMap vertexFormat = new PropertyMap();
            vertexFormat.Add("aPosition", new PropertyValue((int)PropertyType.Vector3));
            vertexFormat.Add("aNormal", new PropertyValue((int)PropertyType.Vector3));
            vertexFormat.Add("aTexCoord", new PropertyValue((int)PropertyType.Vector2));
            PropertyBuffer vertexBuffer = new PropertyBuffer(vertexFormat);

            vertexBuffer.SetData( SphereVertexDataPtr(), SPHERE_VERTEX_NUMBER );

            ushort[] indexBuffer = SphereIndexData();

            Geometry geometry = new Geometry();
            geometry.AddVertexBuffer(vertexBuffer);
            geometry.SetIndexBuffer(indexBuffer, SPHERE_INDEX_NUMBER);
            geometry.SetType(Geometry.Type.TRIANGLES);

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

            Animation animation = new Animation(3000);
            animation.AnimateBy(view,"Orientation", new Rotation(new Radian(MathF.PI * 2.0f), new Tizen.NUI.Vector3(0.0f, 1.0f, 0.0f)));
            animation.Looping = true;
            animation.Play();
        }

        // Copy from dali-toolkit/internal/visuals/primitive/primitive-visual.cpp
        private global::System.IntPtr SphereVertexDataPtr()
        {
            TexturedQuadVertex[] vertices = new TexturedQuadVertex[SPHERE_VERTEX_NUMBER];

            const int slices = SPHERE_SLICES;
            const int stacks = SPHERE_STACKS;
            // Build start.
            {

                int   vertexIndex = 0; //Track progress through vertices.
                float x;
                float y;
                float z;

                //Top stack.
                vertices[vertexIndex].position = new Vec3(0.0f, 0.5f, 0.0f);
                vertices[vertexIndex].normal   = new Vec3(0.0f, 1.0f, 0.0f);
                vertices[vertexIndex].texcoord = new Vec2(0.5f, 1.0f);
                vertexIndex++;

                //Middle stacks.
                for(int i = 1; i < stacks; i++)
                {
                    for(int j = 0; j < slices; j++, vertexIndex++)
                    {
                        float cos_j = MathF.Cos(2.0f * MathF.PI * j / (float)slices);
                        float sin_j = MathF.Sin(2.0f * MathF.PI * j / (float)slices);
                        float cos_i = MathF.Cos(MathF.PI * i / (float)stacks);
                        float sin_i = MathF.Sin(MathF.PI * i / (float)stacks);
                        x = cos_j * sin_i;
                        y = cos_i;
                        z = sin_j * sin_i;

                        vertices[vertexIndex].position = new Vec3(x / 2.0f, y / 2.0f, z / 2.0f);
                        vertices[vertexIndex].normal   = new Vec3(x, y, z);
                        vertices[vertexIndex].texcoord = new Vec2((float)j / (float)slices, 1.0f - (float)i / (float)stacks);
                    }
                }

                //Bottom stack.
                vertices[vertexIndex].position = new Vec3(0.0f, -0.5f, 0.0f);
                vertices[vertexIndex].normal   = new Vec3(0.0f, -1.0f, 0.0f);
                vertices[vertexIndex].texcoord = new Vec2(0.5f, 0.0f);
            }
            // Build done.

            int length = Marshal.SizeOf(vertices[0]);
            global::System.IntPtr pA = Marshal.AllocHGlobal(length * SPHERE_VERTEX_NUMBER);

            for (int i = 0; i < SPHERE_VERTEX_NUMBER; i++)
            {
                Marshal.StructureToPtr(vertices[i], pA + i * length, true);
            }

            return pA;
        }

        private ushort[] SphereIndexData()
        {
            ushort[] indices = new ushort[SPHERE_INDEX_NUMBER];
            const int slices = SPHERE_SLICES;
            const int stacks = SPHERE_STACKS;

            // Build start.
            {
                int indiceIndex            = 0; //Used to keep track of progress through indices.
                int previousCycleBeginning = 1; //Stores the index of the vertex that started the cycle of the previous stack.
                int currentCycleBeginning  = 1 + slices;

                //Top stack. Loop from index 1 to index slices, as not counting the very first vertex.
                for(int i = 1; i <= slices; i++, indiceIndex += 3)
                {
                    indices[indiceIndex] = 0;
                    if(i == slices)
                    {
                        //End, so loop around.
                        indices[indiceIndex + 1] = 1;
                    }
                    else
                    {
                        indices[indiceIndex + 1] = (ushort)(i + 1);
                    }
                    indices[indiceIndex + 2] = (ushort)i;
                }

                //Middle Stacks. Want to form triangles between the top and bottom stacks, so loop up to the number of stacks - 2.
                for(int i = 0; i < stacks - 2; i++, previousCycleBeginning += slices, currentCycleBeginning += slices)
                {
                    for(int j = 0; j < slices; j++, indiceIndex += 6)
                    {
                        if(j == slices - 1)
                        {
                            //End, so loop around.
                            indices[indiceIndex]     = (ushort)(previousCycleBeginning + j);
                            indices[indiceIndex + 1] = (ushort)previousCycleBeginning;
                            indices[indiceIndex + 2] = (ushort)(currentCycleBeginning + j);
                            indices[indiceIndex + 3] = (ushort)(currentCycleBeginning + j);
                            indices[indiceIndex + 4] = (ushort)previousCycleBeginning;
                            indices[indiceIndex + 5] = (ushort)currentCycleBeginning;
                        }
                        else
                        {
                            indices[indiceIndex]     = (ushort)(previousCycleBeginning + j);
                            indices[indiceIndex + 1] = (ushort)(previousCycleBeginning + 1 + j);
                            indices[indiceIndex + 2] = (ushort)(currentCycleBeginning + j);
                            indices[indiceIndex + 3] = (ushort)(currentCycleBeginning + j);
                            indices[indiceIndex + 4] = (ushort)(previousCycleBeginning + 1 + j);
                            indices[indiceIndex + 5] = (ushort)(currentCycleBeginning + 1 + j);
                        }
                    }
                }

                //Bottom stack. Loop around the last stack from the previous loop, and go up to the penultimate vertex.
                for(int i = 0; i < slices; i++, indiceIndex += 3)
                {
                    indices[indiceIndex]     = (ushort)(previousCycleBeginning + slices);
                    indices[indiceIndex + 1] = (ushort)(previousCycleBeginning + i);
                    if(i == slices - 1)
                    {
                        //End, so loop around.
                        indices[indiceIndex + 2] = (ushort)previousCycleBeginning;
                    }
                    else
                    {
                        indices[indiceIndex + 2] = (ushort)(previousCycleBeginning + i + 1);
                    }
                }
            }
            // Build done.

            return indices;
        }

        const int NUMBER_OF_SPHERES_ROW = 3;
        const int NUMBER_OF_SPHERES_COLUMN = 2;
        const int NUMBER_OF_SPHERES = NUMBER_OF_SPHERES_ROW * NUMBER_OF_SPHERES_COLUMN;
        const int SPHERE_SLICES = 150; // >= 3
        const int SPHERE_STACKS = 90; // >= 1
        const int SPHERE_VERTEX_NUMBER = SPHERE_SLICES * (SPHERE_STACKS - 1) + 2;
        const int SPHERE_INDEX_NUMBER = 6 * SPHERE_SLICES * (SPHERE_STACKS - 1);

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