using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
//using System.Numerics;
using OpenTK;

namespace Random3DModelGenerator
{
    class Random3DModel
    {
        public Model beforeModel;
        public Model afterModel;   //生成モデル
        public Bitmap heightmap;   //ハイトマップ

        public Random3DModel()
        {
            beforeModel = null;
            afterModel = null;
            heightmap = null;
        }

        //各ファイルを読み込む
        public void Import(string primitive_fp, string heightmap_fp)
        {
            if (!File.Exists(primitive_fp))
            {
                Console.WriteLine("Random3DModel : Import Error ... File is not exist : {0}", primitive_fp);
                return;
            }
            if (!File.Exists(heightmap_fp))
            {
                Console.WriteLine("Random3DModel : Import Error ... File is not exist : {0}", heightmap_fp);
                return;
            }

            beforeModel = Model.Import(primitive_fp);

            //画像の読み込み
            heightmap = new Bitmap(heightmap_fp);
        }

        //filepathへモデルをobj形式で出力する．
        public void Export(string filepath)
        {
            Model.Export(afterModel, filepath);
        }

        public void Apply(double height)
        {
            afterModel = ApplyHeightMap(beforeModel, heightmap, height);
        }

        //渡された3Dモデルをハイトマップに従って，変化させる
        public static Model ApplyHeightMap(Model mdl, Bitmap map, double height)
        {
            Model resultModel;
            Vector3 normal;
            Color pixel;

            resultModel = new Model(mdl);   //モデルのクローンを生成

            for (int i = 0; i < mdl.Verticies.Length; i++)
            {
                //頂点iが対応するテクスチャピクセル色を取得(面を経由)
                Point faceindex = mdl.Verticies[i].faces[0];
                int texcoordNumber = mdl.Faces[faceindex.x].face[faceindex.y].t_num - 1;
                pixel = map.GetPixel(
                    (int)(mdl.Texcoords[texcoordNumber].v * map.Width % map.Width),
                    map.Height - (int)(mdl.Texcoords[texcoordNumber].u * (map.Height - 1) % map.Height) - 1
                    );

                //法線の再計算
                //normal = RecalcNormal(mdl, i);
                normal = Model.CalcAllVertexNormal(mdl, i);

                //頂点を法線方向に，最大heightで，ピクセル色分移動させる．
                resultModel.Verticies[i].x = mdl.Verticies[i].x + normal.X * pixel.B / 255.0 * height;
                resultModel.Verticies[i].y = mdl.Verticies[i].y + normal.Y * pixel.B / 255.0 * height;
                resultModel.Verticies[i].z = mdl.Verticies[i].z + normal.Z * pixel.B / 255.0 * height;
            }

            return resultModel;
        }

        /*
        //頂点の法線ベクトルを計算して返す
        public static Vector3 CalcNormal(Model mdl, int vertex_number)
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
                *

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
        */
    }
}
