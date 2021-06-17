namespace Random3DModelGenerator
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.Button_generate = new System.Windows.Forms.Button();
            this.TextBox_save = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Button_save = new System.Windows.Forms.Button();
            this.GroupBox_input = new System.Windows.Forms.GroupBox();
            this.Button_input = new System.Windows.Forms.Button();
            this.Button_inputviewer = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.TextBox_input = new System.Windows.Forms.TextBox();
            this.Button_outputviewer = new System.Windows.Forms.Button();
            this.GroupBox_output = new System.Windows.Forms.GroupBox();
            this.Button_output = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.TextBox_output = new System.Windows.Forms.TextBox();
            this.TextBox_workspace = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Button_workspace = new System.Windows.Forms.Button();
            this.Button_import = new System.Windows.Forms.Button();
            this.Button_heightmap = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.TextBox_heightmap = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TextBox_height = new System.Windows.Forms.TextBox();
            this.GroupBox_input.SuspendLayout();
            this.GroupBox_output.SuspendLayout();
            this.SuspendLayout();
            // 
            // Button_generate
            // 
            this.Button_generate.Font = new System.Drawing.Font("MS UI Gothic", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Button_generate.Location = new System.Drawing.Point(4, 95);
            this.Button_generate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Button_generate.Name = "Button_generate";
            this.Button_generate.Size = new System.Drawing.Size(306, 57);
            this.Button_generate.TabIndex = 0;
            this.Button_generate.Text = "generate";
            this.Button_generate.UseVisualStyleBackColor = true;
            this.Button_generate.Click += new System.EventHandler(this.Button_generate_Click);
            // 
            // TextBox_save
            // 
            this.TextBox_save.Location = new System.Drawing.Point(58, 359);
            this.TextBox_save.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TextBox_save.Name = "TextBox_save";
            this.TextBox_save.Size = new System.Drawing.Size(520, 19);
            this.TextBox_save.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 362);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "保存先：";
            // 
            // Button_save
            // 
            this.Button_save.Location = new System.Drawing.Point(582, 358);
            this.Button_save.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Button_save.Name = "Button_save";
            this.Button_save.Size = new System.Drawing.Size(63, 18);
            this.Button_save.TabIndex = 3;
            this.Button_save.Text = "Browsing";
            this.Button_save.UseVisualStyleBackColor = true;
            this.Button_save.Click += new System.EventHandler(this.Button_save_Click);
            // 
            // GroupBox_input
            // 
            this.GroupBox_input.Controls.Add(this.label6);
            this.GroupBox_input.Controls.Add(this.TextBox_height);
            this.GroupBox_input.Controls.Add(this.Button_heightmap);
            this.GroupBox_input.Controls.Add(this.label5);
            this.GroupBox_input.Controls.Add(this.TextBox_heightmap);
            this.GroupBox_input.Controls.Add(this.Button_import);
            this.GroupBox_input.Controls.Add(this.Button_input);
            this.GroupBox_input.Controls.Add(this.Button_inputviewer);
            this.GroupBox_input.Controls.Add(this.label3);
            this.GroupBox_input.Controls.Add(this.TextBox_input);
            this.GroupBox_input.Location = new System.Drawing.Point(9, 45);
            this.GroupBox_input.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.GroupBox_input.Name = "GroupBox_input";
            this.GroupBox_input.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.GroupBox_input.Size = new System.Drawing.Size(318, 309);
            this.GroupBox_input.TabIndex = 4;
            this.GroupBox_input.TabStop = false;
            this.GroupBox_input.Text = "入力";
            // 
            // Button_input
            // 
            this.Button_input.Location = new System.Drawing.Point(250, 21);
            this.Button_input.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Button_input.Name = "Button_input";
            this.Button_input.Size = new System.Drawing.Size(63, 18);
            this.Button_input.TabIndex = 11;
            this.Button_input.Text = "Browsing";
            this.Button_input.UseVisualStyleBackColor = true;
            this.Button_input.Click += new System.EventHandler(this.Button_input_Click);
            // 
            // Button_inputviewer
            // 
            this.Button_inputviewer.Font = new System.Drawing.Font("MS UI Gothic", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Button_inputviewer.Location = new System.Drawing.Point(4, 246);
            this.Button_inputviewer.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Button_inputviewer.Name = "Button_inputviewer";
            this.Button_inputviewer.Size = new System.Drawing.Size(309, 58);
            this.Button_inputviewer.TabIndex = 0;
            this.Button_inputviewer.Text = "View";
            this.Button_inputviewer.UseVisualStyleBackColor = true;
            this.Button_inputviewer.Click += new System.EventHandler(this.Button_viewer_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 23);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "OBJファイル：";
            // 
            // TextBox_input
            // 
            this.TextBox_input.Location = new System.Drawing.Point(76, 21);
            this.TextBox_input.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TextBox_input.Name = "TextBox_input";
            this.TextBox_input.Size = new System.Drawing.Size(171, 19);
            this.TextBox_input.TabIndex = 9;
            this.TextBox_input.TextChanged += new System.EventHandler(this.TextBox_input_TextChanged);
            // 
            // Button_outputviewer
            // 
            this.Button_outputviewer.Font = new System.Drawing.Font("MS UI Gothic", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Button_outputviewer.Location = new System.Drawing.Point(4, 246);
            this.Button_outputviewer.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Button_outputviewer.Name = "Button_outputviewer";
            this.Button_outputviewer.Size = new System.Drawing.Size(308, 58);
            this.Button_outputviewer.TabIndex = 0;
            this.Button_outputviewer.Text = "View";
            this.Button_outputviewer.UseVisualStyleBackColor = true;
            this.Button_outputviewer.Click += new System.EventHandler(this.Button_outputviewer_Click);
            // 
            // GroupBox_output
            // 
            this.GroupBox_output.Controls.Add(this.Button_output);
            this.GroupBox_output.Controls.Add(this.Button_outputviewer);
            this.GroupBox_output.Controls.Add(this.label4);
            this.GroupBox_output.Controls.Add(this.TextBox_output);
            this.GroupBox_output.Controls.Add(this.Button_generate);
            this.GroupBox_output.Location = new System.Drawing.Point(332, 45);
            this.GroupBox_output.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.GroupBox_output.Name = "GroupBox_output";
            this.GroupBox_output.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.GroupBox_output.Size = new System.Drawing.Size(316, 310);
            this.GroupBox_output.TabIndex = 5;
            this.GroupBox_output.TabStop = false;
            this.GroupBox_output.Text = "出力";
            // 
            // Button_output
            // 
            this.Button_output.Location = new System.Drawing.Point(250, 22);
            this.Button_output.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Button_output.Name = "Button_output";
            this.Button_output.Size = new System.Drawing.Size(63, 18);
            this.Button_output.TabIndex = 14;
            this.Button_output.Text = "Browsing";
            this.Button_output.UseVisualStyleBackColor = true;
            this.Button_output.Click += new System.EventHandler(this.Button_output_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 24);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "出力ファイル：";
            // 
            // TextBox_output
            // 
            this.TextBox_output.Location = new System.Drawing.Point(74, 22);
            this.TextBox_output.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TextBox_output.Name = "TextBox_output";
            this.TextBox_output.Size = new System.Drawing.Size(173, 19);
            this.TextBox_output.TabIndex = 12;
            // 
            // TextBox_workspace
            // 
            this.TextBox_workspace.Location = new System.Drawing.Point(98, 22);
            this.TextBox_workspace.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TextBox_workspace.Name = "TextBox_workspace";
            this.TextBox_workspace.Size = new System.Drawing.Size(483, 19);
            this.TextBox_workspace.TabIndex = 6;
            this.TextBox_workspace.TextChanged += new System.EventHandler(this.TextBox_workspace_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "ワークスペース：";
            // 
            // Button_workspace
            // 
            this.Button_workspace.Location = new System.Drawing.Point(585, 22);
            this.Button_workspace.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Button_workspace.Name = "Button_workspace";
            this.Button_workspace.Size = new System.Drawing.Size(63, 18);
            this.Button_workspace.TabIndex = 8;
            this.Button_workspace.Text = "Browsing";
            this.Button_workspace.UseVisualStyleBackColor = true;
            this.Button_workspace.Click += new System.EventHandler(this.Button_workspace_Click);
            // 
            // Button_import
            // 
            this.Button_import.Font = new System.Drawing.Font("MS UI Gothic", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Button_import.Location = new System.Drawing.Point(6, 95);
            this.Button_import.Margin = new System.Windows.Forms.Padding(2);
            this.Button_import.Name = "Button_import";
            this.Button_import.Size = new System.Drawing.Size(309, 58);
            this.Button_import.TabIndex = 12;
            this.Button_import.Text = "Import";
            this.Button_import.UseVisualStyleBackColor = true;
            this.Button_import.Click += new System.EventHandler(this.Button_import_Click);
            // 
            // Button_heightmap
            // 
            this.Button_heightmap.Location = new System.Drawing.Point(250, 45);
            this.Button_heightmap.Margin = new System.Windows.Forms.Padding(2);
            this.Button_heightmap.Name = "Button_heightmap";
            this.Button_heightmap.Size = new System.Drawing.Size(63, 18);
            this.Button_heightmap.TabIndex = 15;
            this.Button_heightmap.Text = "Browsing";
            this.Button_heightmap.UseVisualStyleBackColor = true;
            this.Button_heightmap.Click += new System.EventHandler(this.Button_heightmap_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 47);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "ハイトマップ：";
            // 
            // TextBox_heightmap
            // 
            this.TextBox_heightmap.Location = new System.Drawing.Point(76, 45);
            this.TextBox_heightmap.Margin = new System.Windows.Forms.Padding(2);
            this.TextBox_heightmap.Name = "TextBox_heightmap";
            this.TextBox_heightmap.Size = new System.Drawing.Size(171, 19);
            this.TextBox_heightmap.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 70);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 12);
            this.label6.TabIndex = 17;
            this.label6.Text = "最大高さ：";
            // 
            // TextBox_height
            // 
            this.TextBox_height.Location = new System.Drawing.Point(76, 68);
            this.TextBox_height.Margin = new System.Windows.Forms.Padding(2);
            this.TextBox_height.Name = "TextBox_height";
            this.TextBox_height.Size = new System.Drawing.Size(171, 19);
            this.TextBox_height.TabIndex = 16;
            this.TextBox_height.Text = "0.5";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 383);
            this.Controls.Add(this.Button_workspace);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TextBox_workspace);
            this.Controls.Add(this.GroupBox_output);
            this.Controls.Add(this.GroupBox_input);
            this.Controls.Add(this.Button_save);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TextBox_save);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximumSize = new System.Drawing.Size(674, 422);
            this.MinimumSize = new System.Drawing.Size(674, 422);
            this.Name = "Form1";
            this.Text = "Random3DModelGenerator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.GroupBox_input.ResumeLayout(false);
            this.GroupBox_input.PerformLayout();
            this.GroupBox_output.ResumeLayout(false);
            this.GroupBox_output.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Button_generate;
        private System.Windows.Forms.TextBox TextBox_save;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Button_save;
        private System.Windows.Forms.GroupBox GroupBox_input;
        private System.Windows.Forms.Button Button_inputviewer;
        private System.Windows.Forms.Button Button_outputviewer;
        private System.Windows.Forms.GroupBox GroupBox_output;
        private System.Windows.Forms.TextBox TextBox_workspace;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Button_workspace;
        private System.Windows.Forms.Button Button_input;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TextBox_input;
        private System.Windows.Forms.Button Button_output;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TextBox_output;
        private System.Windows.Forms.Button Button_import;
        private System.Windows.Forms.Button Button_heightmap;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TextBox_heightmap;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TextBox_height;
    }
}

