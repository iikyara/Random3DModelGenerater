using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Random3DModelGenerator
{
    struct VertexTK
    {
        public Vector3 position;
        public Vector3 normal;
        public Color4 color;

        public VertexTK(Vector3 position, Vector3 normal, Color4 color)
        {
            this.position = position;
            this.normal = normal;
            this.color = color;
        }

        public static readonly int Size = Marshal.SizeOf(default(VertexTK));
    }

    class ModelViewer : GameWindow
    {
        #region Camera__Field

        bool isCameraRotating;      //カメラが回転状態かどうか
        Vector2 mouseMoving;        //初期からのマウスのずれを記録
        Vector2 current, previous;  //現在の点、前の点
        Matrix4 rotate;             //回転行列
        float rotateSpeed;          //回転速度
        float zoom;                 //拡大度
        float wheelPrevious;        //マウスホイールの前の状態

        #endregion

        Vector4 light0Position;      //平行光源の方向
        Color4 light0Ambient;        //光源の環境光成分
        Color4 light0Diffuse;        //光源の拡散光成分
        Color4 light0Specular;       //光源の鏡面光成分

        Vector4 light1Position;      //平行光源の方向
        Color4 light1Ambient;        //光源の環境光成分
        Color4 light1Diffuse;        //光源の拡散光成分
        Color4 light1Specular;       //光源の鏡面光成分

        Color4 materialAmbient;     //材質の環境光成分
        Color4 materialDiffuse;	    //材質の拡散光成分
        Color4 materialSpecular;    //材質の鏡面光成分
        float materialShininess;    //材質の鏡面光の鋭さ

        VertexTK[] vertices;        //頂点
        int[] indices;              //頂点の指標
        Color4 modelColor = new Color4(1.0f, 0.5f, 0.5f, 0.5f);

        int vbo;                    //VBOのバッファの識別番号を保持
        int ibo;                    //IBOのバッファの識別番号を保持

        DateTime start;             //プログラムの開始時刻を保持

        //800x600のウィンドウを作る。タイトルは「1-6:VBO(3)」
        public ModelViewer() : base(800, 600, GraphicsMode.Default, "3D Model Viewer")
        {
            start = DateTime.Now;

            light0Position = new Vector4(200.0f, 150f, 500.0f, 0.0f);
            light0Ambient = new Color4(0.2f, 0.2f, 0.2f, 1.0f);
            light0Diffuse = new Color4(0.7f, 0.7f, 0.7f, 1.0f);
            light0Specular = new Color4(1.0f, 1.0f, 1.0f, 1.0f);

            light1Position = new Vector4(200.0f, -150f, -500.0f, 0.0f);
            light1Ambient = new Color4(0.2f, 0.2f, 0.2f, 1.0f);
            light1Diffuse = new Color4(0.7f, 0.7f, 0.7f, 1.0f);
            light1Specular = new Color4(1.0f, 1.0f, 1.0f, 1.0f);

            materialAmbient = new Color4(0.2f, 0.2f, 0.2f, 1.0f);
            materialDiffuse = new Color4(0.7f, 0.7f, 0.7f, 1.0f);
            materialSpecular = new Color4(0.6f, 0.6f, 0.6f, 1.0f);
            materialShininess = 51.4f;

            vertices = new VertexTK[0];
            indices = new int[0];

            vbo = 0;
            ibo = 0;

            #region Camera__Initialize

            isCameraRotating = false;
            mouseMoving = Vector2.Zero;
            current = Vector2.Zero;
            previous = Vector2.Zero;
            rotate = Matrix4.Identity;
            rotateSpeed = 10.0f;
            zoom = 1.0f;
            wheelPrevious = 0.0f;

            #endregion

            #region Camera__Event

            //マウスボタンが押されると発生するイベント
            this.MouseDown += (sender, e) =>
            {
                //右ボタンが押された場合
                if (e.Button == MouseButton.Left)
                {
                    isCameraRotating = true;
                    current = new Vector2(e.Mouse.X, e.Mouse.Y);
                }
            };

            //マウスボタンが離されると発生するイベント
            this.MouseUp += (sender, e) =>
            {
                //右ボタンが押された場合
                if (e.Button == MouseButton.Left)
                {
                    isCameraRotating = false;
                    previous = Vector2.Zero;
                }
            };

            //マウスが動くと発生するイベント
            this.MouseMove += (sender, e) =>
            {
                ////カメラが回転状態の場合
                if (isCameraRotating)
                {
                    previous = current;
                    current = new Vector2(e.Mouse.X, e.Mouse.Y);
                    mouseMoving += current - previous;
                    
                    if(mouseMoving.Y >= 89.9f)
                    {
                        mouseMoving.Y = 89.9f;
                    }
                    else if(mouseMoving.Y <= -89.9f)
                    {
                        mouseMoving.Y = -89.9f;
                    }

                    float radY = mouseMoving.X / 180 * (float)Math.PI;
                    float radX = mouseMoving.Y / 180 * (float)Math.PI;

                    Matrix4 rotateY;
                    Matrix4 rotateX;

                    Matrix4.CreateRotationY(radY, out rotateY);
                    Matrix4.CreateRotationX(radX, out rotateX);

                    rotate = rotateY * rotateX;
                }
            };

            //マウスホイールが回転すると発生するイベント
            this.MouseWheel += (sender, e) =>
            {
                float delta = (float)e.Mouse.Wheel - (float)wheelPrevious;

                zoom *= (float)Math.Pow(1.2, delta);

                //拡大、縮小の制限
                if (zoom > 2.0f)
                    zoom = 2.0f;
                if (zoom < 0.5f)
                    zoom = 0.5f;
                wheelPrevious = e.Mouse.Wheel;
            };

            #endregion

            VSync = VSyncMode.On;
        }

        //モデルの初期化込みコンストラクタ
        public ModelViewer(Model model) : this()
        {
            ImportModel(model);
        }

        //ウィンドウの起動時に実行される。
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color4.Black);
            GL.Enable(EnableCap.DepthTest);

            //裏面削除、反時計回りが表でカリング
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);
            GL.FrontFace(FrontFaceDirection.Ccw);

            //ライティングON Light0を有効化
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.Light1);

            //法線の正規化
            GL.Enable(EnableCap.Normalize);

            //色を材質に変換
            GL.Enable(EnableCap.ColorMaterial);
            GL.ColorMaterial(MaterialFace.Front, ColorMaterialParameter.Diffuse);

            //各Arrayを有効化
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.NormalArray);
            GL.EnableClientState(ArrayCap.ColorArray);

            //バッファを1コ生成
            GL.GenBuffers(1, out vbo);

            //バッファを1コ生成
            GL.GenBuffers(1, out ibo);
        }

        //ウィンドウの終了時に実行される。
        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);

            GL.DeleteBuffers(1, ref vbo);       //バッファを1コ削除
            GL.DeleteBuffers(1, ref ibo);       //バッファを1コ削除

            GL.DisableClientState(ArrayCap.VertexArray);    //VertexArrayを無効化
            GL.DisableClientState(ArrayCap.NormalArray);    //NormalArrayを無効化
            GL.DisableClientState(ArrayCap.ColorArray);     //ColorArrayを無効化
        }

        //ウィンドウのサイズが変更された場合に実行される。
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(ClientRectangle);
        }

        //画面更新で実行される。
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            KeyboardState keyboardState = Keyboard.GetState();
            //Escapeキーで終了
            if (keyboardState[Key.Escape])
            {
                this.Exit();
            }

            #region Camera__Keyboard

            //F1キーで回転をリセット
            if (keyboardState[Key.F1])
            {
                rotate = Matrix4.Identity;
            }

            //F2キーでY軸90度回転
            if (keyboardState[Key.F2])
            {
                rotate = Matrix4.CreateRotationY(MathHelper.PiOver2);
            }

            //F3キーでY軸180度回転
            if (keyboardState[Key.F3])
            {
                rotate = Matrix4.CreateRotationY(MathHelper.Pi);
            }

            //F4キーでY軸270度回転
            if (keyboardState[Key.F4])
            {
                rotate = Matrix4.CreateRotationY(MathHelper.ThreePiOver2);
            }

            //F5キーで拡大をリセット
            if (keyboardState[Key.F5])
            {
                zoom = 1.0f;
            }

            #endregion
        }

        //画面描画で実行される。
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            #region TransFormationMatrix

            Matrix4 identity = Matrix4.Identity;
            //GL.MultMatrix(ref identity);

            Matrix4 modelView = Matrix4.LookAt(Vector3.UnitZ * 10 / zoom, Vector3.Zero, Vector3.UnitY);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelView);
            GL.MultMatrix(ref rotate);

            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4 / zoom, (float)this.Width / (float)this.Height, 1.0f, 64.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);

            #endregion

            //ライトの指定
            GL.Light(LightName.Light0, LightParameter.Position, light0Position);
            GL.Light(LightName.Light0, LightParameter.Ambient, light0Ambient);
            GL.Light(LightName.Light0, LightParameter.Diffuse, light0Diffuse);
            GL.Light(LightName.Light0, LightParameter.Specular, light0Specular);

            GL.Light(LightName.Light1, LightParameter.Position, light1Position);
            GL.Light(LightName.Light1, LightParameter.Ambient, light1Ambient);
            GL.Light(LightName.Light1, LightParameter.Diffuse, light1Diffuse);
            GL.Light(LightName.Light1, LightParameter.Specular, light1Specular);

            //材質の指定
            GL.Material(MaterialFace.Front, MaterialParameter.Ambient, materialAmbient);
            GL.Material(MaterialFace.Front, MaterialParameter.Diffuse, materialDiffuse);
            GL.Material(MaterialFace.Front, MaterialParameter.Specular, materialSpecular);
            GL.Material(MaterialFace.Front, MaterialParameter.Shininess, materialShininess);

            //波を毎回生成
            //CreateWave(100, 100, 1.0f, 0.5f, 1.0f, 1.0f);

            //毎回描画前にデータを送り込む
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            int vertexArraySize = vertices.Length * VertexTK.Size;
            GL.BufferData<VertexTK>(BufferTarget.ArrayBuffer, new IntPtr(vertexArraySize), vertices, BufferUsageHint.DynamicDraw);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ibo);
            int indexArraySize = indices.Length * sizeof(int);
            GL.BufferData(BufferTarget.ElementArrayBuffer, new IntPtr(indexArraySize), indices, BufferUsageHint.DynamicDraw);


            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);

            //頂点の位置情報の場所を指定
            GL.VertexPointer(3, VertexPointerType.Float, VertexTK.Size, 0);

            //頂点の法線情報の場所を指定
            GL.NormalPointer(NormalPointerType.Float, VertexTK.Size, Vector3.SizeInBytes);

            //頂点の色情報の場所を指定
            GL.ColorPointer(4, ColorPointerType.Float, VertexTK.Size, Vector3.SizeInBytes * 2);

            //IBOを使って描画
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ibo);
            GL.DrawElements(BeginMode.Quads, indices.Length, DrawElementsType.UnsignedInt, 0);

            DrawAxis();

            //バッファのひも付けを解除
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

            SwapBuffers();
        }

        //モデルを読み込む．四角面のみサポート
        void ImportModel(Model model)
        {
            if(model == null)
            {
                Console.WriteLine("ModelViewer : Model Import Error (Model is null.)");
                return;
            }

            vertices = new VertexTK[model.Verticies.Length];
            indices = new int[model.Faces.Length * 4];

            //頂点情報の読み出し
            for (int i = 0; i < model.Verticies.Length; i++)
            {
                Vector3 pos = new Vector3(
                    (float)model.Verticies[i].x,
                    (float)model.Verticies[i].y,
                    (float)model.Verticies[i].z
                    );
                Vector3 nor = Model.RecalcNormal(model, i);
                Color4 col = modelColor;
                vertices[i] = new VertexTK(pos, nor, col);
            }

            //面情報の格納
            for (int i = 0; i < model.Faces.Length; i++)
            {
                for(int j = 0; j < model.Faces[i].face.Length;j++)
                {
                    indices[i * 4 + j] = model.Faces[i].face[j].v_num - 1;
                }
            }

            //モデル情報を表示
            Console.WriteLine("Imported model have {0} vertices.", vertices.Length);
        }

        //波を作成（サンプルプログラム）
        void CreateWave(int row, int column, float size, float height, float frequency, float velocity)
        {
            if (row < 1 || column < 1)
            {
                return;
            }

            vertices = new VertexTK[(row + 1) * (column + 1)];
            indices = new int[row * column * 4];

            //プログラムの経過時間を取得
            TimeSpan ts = DateTime.Now - start;

            float omega = MathHelper.TwoPi * frequency;
            float t = (float)ts.TotalSeconds;
            float v = velocity;
            float h = (float)height / 2.0f;

            //位置、法線、色
            for (int i = 0; i <= row; i++)
            {
                for (int j = 0; j <= column; j++)
                {
                    float x = (2.0f * (float)i / (float)(row + 1) - 1.0f) * size;
                    float y = (2.0f * (float)j / (float)(column + 1) - 1.0f) * size;
                    float r = x * x + y * y;

                    //z = h * sin(omega(t-r/v))
                    //z'= h * (-omega/v)cos(2pif(t-r/v)) 接線
                    float z = h * (float)Math.Sin(omega * (t - r / v));
                    Vector3 position = new Vector3(x, y, z);
                    Vector3 n = new Vector3(h * (float)Math.Cos(omega * (t - r / v)) * omega / v, 0.0f, 1.0f);  //xz平面での法線
                    Matrix4 m = Matrix4.CreateRotationZ((float)Math.Atan2(y, x));
                    Matrix3 m3 = new Matrix3(m.M11, m.M12, m.M13, m.M21, m.M22, m.M23, m.M31, m.M32, m.M33);
                    Vector3 normal = Vector3.Transform(n, m3);
                    normal.Normalize();
                    Color4 color = new Color4((float)i / (float)(row + 1), (float)j / (float)(column + 1), (z + 1.0f) * 0.5f, 1.0f);
                    vertices[i * (column + 1) + j] = new VertexTK(position, normal, color);
                }
            }

            //インデックス
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    indices[(i * column + j) * 4] = i * (column + 1) + j;
                    indices[(i * column + j) * 4 + 1] = (i + 1) * (column + 1) + j;
                    indices[(i * column + j) * 4 + 2] = (i + 1) * (column + 1) + j + 1;
                    indices[(i * column + j) * 4 + 3] = i * (column + 1) + j + 1;
                }
            }
        }

        //原点を描画
        protected void DrawAxis()
        {
            GL.Begin(BeginMode.Lines);
            {
                //X
                GL.Color3(1.0f, 0.0f, 0.0f);
                GL.Vertex3(0.0f, 0.0f, 0.0f);
                GL.Vertex3(1.0f, 0.0f, 0.0f);

                //Y
                GL.Color3(0.0f, 1.0f, 0.0f);
                GL.Vertex3(0.0f, 0.0f, 0.0f);
                GL.Vertex3(0.0f, 1.0f, 0.0f);

                //Z
                GL.Color3(0.0f, 0.0f, 1.0f);
                GL.Vertex3(0.0f, 0.0f, 0.0f);
                GL.Vertex3(0.0f, 0.0f, 1.0f);
            }
            GL.End();
        }
    }
}
