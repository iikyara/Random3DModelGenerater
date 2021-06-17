using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using OpenTK;

namespace Random3DModelGenerator
{
    //2つの整数を格納する構造体
    public struct Point
    {
        public int x;
        public int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    //頂点構造体
    public struct Vertex
    {
        //頂点座標
        public double x;
        public double y;
        public double z;

        //頂点色
        public Color c;

        //参照している面のインデックス
        public Point[] faces;
    }

    //テクスチャ座標構造体
    public struct Texcoord
    {
        //テクスチャ座標
        public double u;
        public double v;
    }

    //ノーマル座標構造体
    public struct Normal
    {
        //ノーマル座標
        public double x;
        public double y;
        public double z;
    }

    //1頂点構造体
    public struct VertexNumber
    {
        public int v_num;    //頂点座標番号
        public int t_num;    //テクスチャ座標番号
        public int n_num;    //ノーマル座標番号
    }

    //面構造体
    public struct Face
    {   
        public VertexNumber[] face;    //面を構成する頂点の集合
    }

    public class Model
    {
        public Vertex[] Verticies;
        public Texcoord[] Texcoords;
        public Normal[] Normals;
        public Face[] Faces;

        public Model(Vertex[] vs, Texcoord[] ts, Normal[] ns, Face[] fs)
        {
            Verticies = vs;
            Texcoords = ts;
            Normals = ns;
            Faces = fs;
        }

        public Model(double[,] v, VertexNumber[,] f)
        {
            //頂点情報の格納
            SetVerticies(v);

            //面情報格納
            SetFaces(f);
        }

        public Model(double[,] v, double[,] vn, VertexNumber[,] f) : this(v, f)
        {
            //ノーマル情報の格納
            SetNormal(vn);
        }

        public Model(double[,] v, Color[] vc, VertexNumber[,] f) : this(v, f)
        {
            //頂点カラー情報の格納
            SetColor(vc);
        }

        //複製を返す
        public Model(Model model)
        {
            Vertex[] vs = new Vertex[model.Verticies.Length];
            Texcoord[] ts = new Texcoord[model.Texcoords.Length];
            Normal[] ns = new Normal[model.Normals.Length];
            Face[] fs = new Face[model.Faces.Length];

            Array.Copy(model.Verticies, vs, model.Verticies.Length);
            Array.Copy(model.Texcoords, ts, model.Texcoords.Length);
            Array.Copy(model.Normals, ns, model.Normals.Length);

            //vertexとfaceは構造体の中に配列を持つため，１つずつコピー
            for (int i = 0; i < model.Verticies.Length; i++)
            {
                Point[] faces = new Point[model.Verticies[i].faces.Length];
                for (int j = 0; j < model.Verticies[i].faces.Length; j++)
                {
                    faces[j] = model.Verticies[i].faces[j];
                }
                vs[i].faces = faces;
            }
            for (int i = 0; i < model.Faces.Length; i++)
            {
                VertexNumber[] face = new VertexNumber[model.Faces[i].face.Length];
                for(int j = 0;j<model.Faces[i].face.Length;j++)
                {
                    face[j] = model.Faces[i].face[j];
                }
                fs[i].face = face;
            }

            Verticies = vs;
            Texcoords = ts;
            Normals = ns;
            Faces = fs;
        }

        public void SetVerticies(double[,] v)
        {
            Verticies = new Vertex[v.GetLength(0)];
            for (int i = 0; i < Verticies.Length; i++)
            {
                Verticies[i].x = v[i, 0];
                Verticies[i].y = v[i, 1];
                Verticies[i].z = v[i, 2];
            }
        }
        public void SetNormal(double[,] vn)
        {
            Normals = new Normal[vn.GetLength(0)];
            for (int i = 0; i < Normals.Length; i++)
            {
                Normals[i].x = vn[i, 0];
                Normals[i].y = vn[i, 1];
                Normals[i].z = vn[i, 2];
            }
        }
        public void SetColor(Color[] vc)
        {
            //頂点カラー情報の格納
            for (int i = 0; i < Verticies.Length; i++)
            {
                Verticies[i].c = vc[i];
            }
        }
        public void SetFaces(VertexNumber[,] f)
        {
            Faces = new Face[f.GetLength(0)];
            for (int i = 0; i < Faces.Length; i++)
            {
                Faces[i].face = new VertexNumber[f.GetLength(1)];
                for (int j = 0; j < Faces[i].face.Length; j++)
                {
                    Faces[i].face[j] = f[i, j];
                }
            }
        }

        //指定されたパスのOBJモデルをインポートする．
        public static Model Import(string primitive_fp)
        {
            if (!File.Exists(primitive_fp))
            {
                Console.WriteLine("Random3DModel : Import Error ... File is not exist : {0}", primitive_fp);
                return null;
            }

            StreamReader sr_p = new StreamReader(primitive_fp, Encoding.GetEncoding("shift_jis"));
            List<Vertex> vs = new List<Vertex>();
            List<Texcoord> vts = new List<Texcoord>();
            List<Normal> ns = new List<Normal>();
            List<VertexNumber> vns = new List<VertexNumber>();
            List<Face> fs = new List<Face>();
            int count = 0;  //行数

            while (sr_p.Peek() != -1)
            {
                string[] line;  //1行を空白で分割した文字列
                List<string> tmp;   //分割後に，空文字列を削除するためのリスト
                bool tryResult; //パースが成功したかどうか

                count++;    //行数をカウント

                line = sr_p.ReadLine().Split(' ');
                tmp = new List<string>(line);
                tmp.RemoveAll(item => item == null);
                tmp.RemoveAll(item => item == "");
                line = tmp.ToArray();

                if (line.Length == 0)
                {
                    continue;
                }

                if (line[0] == "v")
                {
                    tryResult = true;
                    Vertex vtemp = new Vertex();
                    vtemp.faces = new Point[0];   //面インデックスの初期化
                    tryResult &= double.TryParse(line[1], out vtemp.x);
                    tryResult &= double.TryParse(line[2], out vtemp.y);
                    tryResult &= double.TryParse(line[3], out vtemp.z);
                    if (tryResult)
                    {
                        vs.Add(vtemp);
                    }
                    else
                    {
                        Console.WriteLine("{0} : Vertex Import Error.", count);
                    }
                }
                else if (line[0] == "vt")
                {
                    tryResult = true;
                    Texcoord vtemp = new Texcoord();
                    tryResult &= double.TryParse(line[1], out vtemp.u);
                    tryResult &= double.TryParse(line[2], out vtemp.v);
                    if (tryResult)
                    {
                        vts.Add(vtemp);
                    }
                    else
                    {
                        Console.WriteLine("{0} : Texcoord Import Error.", count);
                    }
                }
                else if (line[0] == "vn")
                {
                    tryResult = true;
                    Normal ntemp = new Normal();
                    tryResult &= double.TryParse(line[1], out ntemp.x);
                    tryResult &= double.TryParse(line[2], out ntemp.y);
                    tryResult &= double.TryParse(line[3], out ntemp.z);
                    if (tryResult)
                    {
                        ns.Add(ntemp);
                    }
                    else
                    {
                        Console.WriteLine("{0} : Normal Import Error.", count);
                    }
                }
                else if (line[0] == "f")
                {
                    tryResult = true;
                    Face ftemp = new Face();
                    vns.Clear();
                    for (int i = 1; i < line.Length; i++)
                    {
                        String[] vn_strs = line[i].Split('/');
                        VertexNumber vntemp = new VertexNumber();
                        tryResult &= int.TryParse(vn_strs[0], out vntemp.v_num);
                        int.TryParse(vn_strs[1], out vntemp.t_num);
                        tryResult &= int.TryParse(vn_strs[2], out vntemp.n_num);
                        vns.Add(vntemp);

                        //参照した頂点から面に対して逆引きできるように，頂点へ面インデックスを格納．
                        Vertex temp = vs[vntemp.v_num - 1];
                        Point[] additional = new Point[] { new Point(fs.Count, i - 1) };
                        temp.faces = vs[vntemp.v_num - 1].faces.Concat(additional).ToArray();
                        vs[vntemp.v_num - 1] = temp;
                    }
                    if (tryResult)
                    {
                        ftemp.face = vns.ToArray();
                        fs.Add(ftemp);
                    }
                    else
                    {
                        Console.WriteLine("{0} : Face Import Error.", count);
                    }
                }
            }

            //modelへ格納
            Vertex[] Verticies = vs.ToArray();
            Texcoord[] Texcoords = vts.ToArray();
            Normal[] Normals = ns.ToArray();
            Face[] Faces = fs.ToArray();

            sr_p.Close();

            Console.WriteLine("Importing is success.");

            return new Model(Verticies, Texcoords, Normals, Faces);
        }

        //指定されたパスにOBJ形式でエクスポートする．
        public static void Export(Model model, string filepath)
        {
            StreamWriter w = new StreamWriter(filepath, false, Encoding.GetEncoding("shift_jis"));
            //頂点情報を記述
            for (int i = 0; i < model.Verticies.Length; i++)
            {
                w.WriteLine("v {0} {1} {2}", model.Verticies[i].x, model.Verticies[i].y, model.Verticies[i].z);
            }

            //テクスチャ情報を記述
            for (int i = 0; i < model.Texcoords.Length; i++)
            {
                w.WriteLine("vt {0} {1}", model.Texcoords[i].u, model.Texcoords[i].v);
            }

            //法線情報を記述
            for (int i = 0; i < model.Normals.Length; i++)
            {
                w.WriteLine("vn {0} {1} {2}", model.Normals[i].x, model.Normals[i].y, model.Normals[i].z);
            }

            //面情報を記述
            string wstring;
            for (int i = 0; i < model.Faces.Length; i++)
            {
                wstring = @"f";
                for (int j = 0; j < model.Faces[i].face.Length; j++)
                {
                    wstring += @" ";
                    if (model.Faces[i].face[j].v_num != 0)
                    {
                        wstring += model.Faces[i].face[j].v_num.ToString();
                    }
                    wstring += @"/";
                    if (model.Faces[i].face[j].t_num != 0)
                    {
                        wstring += model.Faces[i].face[j].t_num.ToString();
                    }
                    wstring += @"/";
                    if (model.Faces[i].face[j].n_num != 0)
                    {
                        wstring += model.Faces[i].face[j].n_num.ToString();
                    }
                }
                w.WriteLine(wstring);
            }
            w.Close();

            Console.WriteLine("Export is success.");
        }
        
        //頂点の法線ベクトルを全て計算して返す
        public static Vector3 CalcAllVertexNormal(Model mdl, int vertex_number)
        {
            Vector3 normal = new Vector3((float)0.0, (float)0.0, (float)0.0);
            for (int j = 0; j < mdl.Verticies[vertex_number].faces.Length; j++)
            {
                Face f = mdl.Faces[mdl.Verticies[vertex_number].faces[j].x];    //参照する面
                int index = mdl.Verticies[vertex_number].faces[j].y;            //面の中での頂点番号
                Normal n = mdl.Normals[f.face[index].n_num - 1];
                normal = normal + new Vector3((float)n.x, (float)n.y, (float)n.z);
            }
            normal = Vector3.Normalize(normal); //正規化
            return normal;
        }

        //頂点の法線ベクトルを計算し直して返す(四角面を想定)
        public static Vector3 RecalcNormal(Model mdl, int vertex_number)
        {
            Vector3 normal = new Vector3((float)0.0, (float)0.0, (float)0.0);

            for (int j = 0; j < mdl.Verticies[vertex_number].faces.Length; j++)
            {
                /*
                Face f = mdl.Faces[mdl.Verticies[vertex_number].faces[j].x];    //参照する面
                int index = mdl.Verticies[vertex_number].faces[j].y;            //面の中での頂点番号

                //頂点を選択
                Vector3 vv1, vv2, n;
                Vertex v;
                v = mdl.Verticies[f.face[(index - 1 + f.face.Length * 10) % f.face.Length].v_num - 1];
                Vector3 v0 = new Vector3((float)v.x, (float)v.y, (float)v.z);
                v = mdl.Verticies[f.face[(index + f.face.Length * 10) % f.face.Length].v_num - 1];
                Vector3 v1 = new Vector3((float)v.x, (float)v.y, (float)v.z);
                v = mdl.Verticies[f.face[(index + 1 + f.face.Length * 10) % f.face.Length].v_num - 1];
                Vector3 v2 = new Vector3((float)v.x, (float)v.y, (float)v.z);

                //面法線の計算
                vv1 = v1 - v0;
                vv2 = v2 - v1;
                n = Vector3.Cross(vv1, vv2);
                n = Vector3.Normalize(n);
                */

                int findex = mdl.Verticies[vertex_number].faces[j].x;
                Vector3 n = RecalcSurfaceNormal(mdl, findex);

                normal = normal + n;
            }

            normal = Vector3.Normalize(normal);
            return normal;
        }

        //面の法線ベクトルを計算し直して返す．（四角面を想定））
        public static Vector3 RecalcSurfaceNormal(Model mdl, int face_number)
        {
            Vector3 normal = new Vector3((float)0.0, (float)0.0, (float)0.0);
            Vector3[] v = new Vector3[4];

            for (int i = 0; i < v.Length; i++)
            {
                int vindex = mdl.Faces[face_number].face[i].v_num - 1;
                v[i] = new Vector3(
                    (float)mdl.Verticies[vindex].x,
                    (float)mdl.Verticies[vindex].y,
                    (float)mdl.Verticies[vindex].z
                    );
            }

            normal = Vector3.Cross(v[0] - v[2], v[1] - v[3]);
            normal = Vector3.Normalize(normal);

            return normal;
        }

        //面の法線ベクトルを計算して返す．（三角面を想定）
        public static Vector3 CalcTriangleFaceNormal(Model mdl, int face_number)
        {
            Vector3 normal = new Vector3((float)0.0, (float)0.0, (float)0.0);
            Vector3[] v = new Vector3[3];

            for (int i = 0; i < v.Length; i++)
            {
                int vindex = mdl.Faces[face_number].face[i].v_num - 1;
                v[i] = new Vector3(
                    (float)mdl.Verticies[vindex].x,
                    (float)mdl.Verticies[vindex].y,
                    (float)mdl.Verticies[vindex].z
                    );
            }

            normal = Vector3.Cross(v[2] - v[0], v[1] - v[0]);
            normal = Vector3.Normalize(normal);

            return normal;
        }
    }
}
