using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Random3DModelGenerator
{
    public partial class Form1 : Form
    {
        Random3DModel controller;   //コントローラー

        public Form1()
        {
            InitializeComponent();

            controller = new Random3DModel();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //保存先を入力するまで作成できない．
            Button_generate.Enabled = false;
            Button_import.Enabled = false;
            TextBox_save.Enabled = false;
            SetEnabled(false);
        }

        private void Button_save_Click(object sender, EventArgs e)
        {
            if(Browsing(TextBox_save) == DialogResult.OK)
            {
                Button_generate.Enabled = true;
            }
            else
            {
                Button_generate.Enabled = false;
            }
        }

        private void Button_workspace_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            fbd.Description = "ワークスペースを指定してください．";
            fbd.RootFolder = Environment.SpecialFolder.Desktop;
            fbd.SelectedPath = Environment.CurrentDirectory;
            
            if(fbd.ShowDialog(this) == DialogResult.OK)
            {
                TextBox_workspace.Text = fbd.SelectedPath;
            }
        }

        private void Button_generate_Click(object sender, EventArgs e)
        {
            double height;
            if(!double.TryParse(TextBox_height.Text, out height))
            {
                MessageBox.Show("高さには数字を入力してください");
                return;
            }

            controller.Apply(height);
            controller.Export(TextBox_output.Text);
        }

        private void Button_viewer_Click(object sender, EventArgs e)
        {
            using (ModelViewer window = new ModelViewer(controller.beforeModel))
            {
                window.Run(30.0);
            }
        }

        private DialogResult Browsing(TextBox tb, string initialFilename, string initialDirectory, string title, bool overwriteprompt)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.FileName = initialFilename;
            sfd.InitialDirectory = initialDirectory;
            sfd.Filter = "全てのファイル(*.*)|*.*";
            sfd.FilterIndex = 0;
            sfd.Title = title;
            sfd.OverwritePrompt = overwriteprompt;

            DialogResult result = sfd.ShowDialog();

            if (result == DialogResult.OK)
            {
                tb.Text = sfd.FileName;
            }

            return result;
        }

        private DialogResult Browsing(TextBox tb)
        {
            return Browsing(
                tb,
                "output.obj",
                @"C:\",
                "保存先のファイルを選んでください．",
                true
                );
        }

        //ワークスペース以外の部品を使えるか設定する
        private void SetEnabled(bool isEnabled)
        {
            GroupBox_input.Enabled = isEnabled;
            GroupBox_output.Enabled = isEnabled;
            Button_save.Enabled = isEnabled;
        }

        private void Button_input_Click(object sender, EventArgs e)
        {
            DialogResult result = Browsing(
                TextBox_input,
                "input.obj",
                TextBox_workspace.Text,
                "インポートするファイルを選択してください．",
                false
                );
            if (result == DialogResult.OK)
            {
                Button_import.Enabled = true;
            }
            else
            {
                Button_import.Enabled = false;
            }
        }

        private void Button_output_Click(object sender, EventArgs e)
        {
            DialogResult result = Browsing(
                   TextBox_output,
                   "output.obj",
                   TextBox_workspace.Text,
                   "保存するファイルを選択してください．",
                   true
                   );
            if (result == DialogResult.OK)
            {
                Button_generate.Enabled = true;
            }
            else
            {
                Button_generate.Enabled = false;
            }
        }

        private void TextBox_input_TextChanged(object sender, EventArgs e)
        {
            if(File.Exists(((TextBox)sender).Text))
            {
                Button_import.Enabled = true;
            }
            else
            {
                Button_import.Enabled = false;
            }
        }

        private void Button_import_Click(object sender, EventArgs e)
        {
            if(!File.Exists(TextBox_input.Text))
            {
                MessageBox.Show("入力されたOBJファイルが見つかりませんでした．");

                return;
            }
            if(!File.Exists(TextBox_heightmap.Text))
            {
                MessageBox.Show("入力されたハイトマップファイルが見つかりませんでした．");
                return;
            }
            controller.Import(TextBox_input.Text,TextBox_heightmap.Text);
        }

        private void Button_heightmap_Click(object sender, EventArgs e)
        {
            DialogResult result = Browsing(
                TextBox_heightmap,
                "input.obj",
                TextBox_workspace.Text,
                "インポートするファイルを選択してください．",
                false
                );
            if (result == DialogResult.OK)
            {
                Button_import.Enabled = true;
            }
            else
            {
                Button_import.Enabled = false;
            }
        }

        private void Button_outputviewer_Click(object sender, EventArgs e)
        {
            using (ModelViewer window = new ModelViewer(controller.afterModel))
            {
                window.Run(30.0);
            }
        }

        private void TextBox_workspace_TextChanged(object sender, EventArgs e)
        {
            if(Directory.Exists(((TextBox)sender).Text))
            {
                SetEnabled(true);
            }
            else
            {
                SetEnabled(false);
            }
        }
    }
}
