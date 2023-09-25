
namespace NTUpgradeFiles2
{
    partial class FormPaths
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.labelRunExe = new System.Windows.Forms.Label();
            this.buttonListRunExe = new System.Windows.Forms.Button();
            this.listViewUpgrPaths = new System.Windows.Forms.ListView();
            this.buttonUpgr = new System.Windows.Forms.Button();
            this.buttonDel = new System.Windows.Forms.Button();
            this.buttonExeName = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(214, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Po upgrade spust tento exe (relativni cesta):";
            // 
            // labelRunExe
            // 
            this.labelRunExe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelRunExe.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelRunExe.Location = new System.Drawing.Point(16, 30);
            this.labelRunExe.Name = "labelRunExe";
            this.labelRunExe.Size = new System.Drawing.Size(880, 24);
            this.labelRunExe.TabIndex = 25;
            // 
            // buttonListRunExe
            // 
            this.buttonListRunExe.Location = new System.Drawing.Point(902, 30);
            this.buttonListRunExe.Name = "buttonListRunExe";
            this.buttonListRunExe.Size = new System.Drawing.Size(48, 23);
            this.buttonListRunExe.TabIndex = 26;
            this.buttonListRunExe.Text = ". . .";
            this.buttonListRunExe.UseVisualStyleBackColor = true;
            this.buttonListRunExe.Click += new System.EventHandler(this.buttonListRunExe_Click);
            // 
            // listViewUpgrPaths
            // 
            this.listViewUpgrPaths.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.listViewUpgrPaths.AllowColumnReorder = true;
            this.listViewUpgrPaths.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewUpgrPaths.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listViewUpgrPaths.CheckBoxes = true;
            this.listViewUpgrPaths.FullRowSelect = true;
            this.listViewUpgrPaths.GridLines = true;
            this.listViewUpgrPaths.HideSelection = false;
            this.listViewUpgrPaths.LabelEdit = true;
            this.listViewUpgrPaths.Location = new System.Drawing.Point(16, 63);
            this.listViewUpgrPaths.Name = "listViewUpgrPaths";
            this.listViewUpgrPaths.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.listViewUpgrPaths.Size = new System.Drawing.Size(934, 476);
            this.listViewUpgrPaths.TabIndex = 23;
            this.listViewUpgrPaths.UseCompatibleStateImageBehavior = false;
            this.listViewUpgrPaths.View = System.Windows.Forms.View.Details;
            // 
            // buttonUpgr
            // 
            this.buttonUpgr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUpgr.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonUpgr.Location = new System.Drawing.Point(829, 545);
            this.buttonUpgr.Name = "buttonUpgr";
            this.buttonUpgr.Size = new System.Drawing.Size(121, 34);
            this.buttonUpgr.TabIndex = 29;
            this.buttonUpgr.Text = "&Proved upgrade";
            this.buttonUpgr.UseVisualStyleBackColor = true;
            this.buttonUpgr.Click += new System.EventHandler(this.buttonUpgr_Click);
            // 
            // buttonDel
            // 
            this.buttonDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDel.Location = new System.Drawing.Point(16, 545);
            this.buttonDel.Name = "buttonDel";
            this.buttonDel.Size = new System.Drawing.Size(121, 34);
            this.buttonDel.TabIndex = 31;
            this.buttonDel.Text = "&Smaž označené";
            this.buttonDel.UseVisualStyleBackColor = true;
            this.buttonDel.Click += new System.EventHandler(this.buttonDel_Click);
            // 
            // buttonExeName
            // 
            this.buttonExeName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonExeName.Location = new System.Drawing.Point(143, 545);
            this.buttonExeName.Name = "buttonExeName";
            this.buttonExeName.Size = new System.Drawing.Size(121, 34);
            this.buttonExeName.TabIndex = 32;
            this.buttonExeName.Text = "New &upgrade cesta ";
            this.buttonExeName.UseVisualStyleBackColor = true;
            this.buttonExeName.Click += new System.EventHandler(this.buttonExeName_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "\\\\DataServer\\vyroba\\NetVyroba\\NetVyroba.exe";
            this.openFileDialog1.InitialDirectory = "\\\\DataServer\\vyroba\\NetVyroba";
            // 
            // FormPaths
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 586);
            this.Controls.Add(this.buttonExeName);
            this.Controls.Add(this.buttonDel);
            this.Controls.Add(this.buttonUpgr);
            this.Controls.Add(this.listViewUpgrPaths);
            this.Controls.Add(this.buttonListRunExe);
            this.Controls.Add(this.labelRunExe);
            this.Controls.Add(this.label1);
            this.Name = "FormPaths";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Upgrade";
            this.Load += new System.EventHandler(this.FormPaths_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelRunExe;
        private System.Windows.Forms.Button buttonListRunExe;
        public System.Windows.Forms.ListView listViewUpgrPaths;
        private System.Windows.Forms.Button buttonUpgr;
        private System.Windows.Forms.Button buttonDel;
        private System.Windows.Forms.Button buttonExeName;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}