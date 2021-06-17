using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OpenTK;

namespace Random3DModelGenerator
{
    public class ModelComparator
    {
        //param
        Model model1, model2;
        public double SodaResult;

        //constructor
        public ModelComparator()
        {
            model1 = null;
            model2 = null;
        }
        public ModelComparator(Model m1, Model m2) : this()
        {
            model1 = m1;
            model2 = m2;
        }

        public void setModel1(Model m1)
        {
            model1 = m1;
        }

        public void setModel2(Model m2)
        {
            model1 = m2;
        }

        //io methods
        public void Export(string filename)
        {

        }

        //method Runing Comparating methods
        public void RunComparatingMethods()
        {
            SodaComparatingMethod1();
        }

        //comparating methods
        public void SodaComparatingMethod1()
        {
            double lamda = 10;
            double phi = 10;
            double[][] f1 = SodaFeature(model1, lamda, phi);
            double[][] f2 = SodaFeature(model2, lamda, phi);


        }

        /// <summary>
        /// 惣田メソッドに基づいてモデルの特徴量を計算し，特徴量を返す
        /// </summary>
        /// <param name="mdl">特徴量を計算するモデル</param>
        /// <param name="lamda">角度kをlamda個の領域に区切る</param>
        /// <param name="phi">相対距離aをphi個の領域に区切る</param>
        /// <returns>特徴量（2次元配列）を返す</returns>
        private static double[][] SodaFeature(Model mdl, double lamda, double phi)
        {
            string EPS = "#.###";   //精度
            int[] Q = null; //面対の各面番号を2つ格納
            List<int[]> M = new List<int[]>();
            Dictionary<string, int[]> Ma = null;
            Dictionary<string, Dictionary<string, int[]>> Mka = new Dictionary<string, Dictionary<string, int[]>>();

            //集合Mの作成
            for(int i = 0; i < mdl.Faces.Length; i++)
            {
                for(int j = i + 1; j < mdl.Faces.Length; j++)
                {
                    Q = new int[2];
                    Q[0] = i;
                    Q[1] = j;
                    M.Add(Q);
                }
            }

            //集合Mkの作成
            foreach(int[] Qi in M)
            {
                Vector3 normal1 = Model.CalcTriangleFaceNormal(mdl, Qi[0]);
                Vector3 normal2 = Model.CalcTriangleFaceNormal(mdl, Qi[1]);
            }

            return null;
        }
    }
}
