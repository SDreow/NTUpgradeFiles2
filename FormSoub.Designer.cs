
namespace NTUpgradeFiles2
{
    partial class FormSoub
    {
        /// <summary>
        /// Vyžaduje se proměnná návrháře.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Uvolněte všechny používané prostředky.
        /// </summary>
        /// <param name="disposing">hodnota true, když by se měl spravovaný prostředek odstranit; jinak false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kód generovaný Návrhářem Windows Form

        /// <summary>
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listViewSoub = new System.Windows.Forms.ListView();
            this.listView2Zmeny = new System.Windows.Forms.ListView();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.soubory = new System.Windows.Forms.TabPage();
            this.buttonUpgr = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.soubory.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.listViewSoub);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listView2Zmeny);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Size = new System.Drawing.Size(948, 508);
            this.splitContainer1.SplitterDistance = 269;
            this.splitContainer1.TabIndex = 0;
            // 
            // listViewSoub
            // 
            this.listViewSoub.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewSoub.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewSoub.FullRowSelect = true;
            this.listViewSoub.GridLines = true;
            this.listViewSoub.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewSoub.HideSelection = false;
            this.listViewSoub.Location = new System.Drawing.Point(0, 0);
            this.listViewSoub.Name = "listViewSoub";
            this.listViewSoub.Size = new System.Drawing.Size(948, 269);
            this.listViewSoub.TabIndex = 0;
            this.listViewSoub.UseCompatibleStateImageBehavior = false;
            this.listViewSoub.View = System.Windows.Forms.View.Details;
            // 
            // listView2Zmeny
            // 
            this.listView2Zmeny.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView2Zmeny.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView2Zmeny.FullRowSelect = true;
            this.listView2Zmeny.GridLines = true;
            this.listView2Zmeny.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView2Zmeny.HideSelection = false;
            this.listView2Zmeny.Location = new System.Drawing.Point(0, 0);
            this.listView2Zmeny.Name = "listView2Zmeny";
            this.listView2Zmeny.Size = new System.Drawing.Size(948, 235);
            this.listView2Zmeny.TabIndex = 0;
            this.listView2Zmeny.UseCompatibleStateImageBehavior = false;
            this.listView2Zmeny.View = System.Windows.Forms.View.Details;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.soubory);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(962, 540);
            this.tabControl.TabIndex = 0;
            // 
            // soubory
            // 
            this.soubory.Controls.Add(this.splitContainer1);
            this.soubory.Location = new System.Drawing.Point(4, 22);
            this.soubory.Name = "soubory";
            this.soubory.Padding = new System.Windows.Forms.Padding(3);
            this.soubory.Size = new System.Drawing.Size(954, 514);
            this.soubory.TabIndex = 0;
            this.soubory.Text = "Soubory";
            this.soubory.UseVisualStyleBackColor = true;
            // 
            // buttonUpgr
            // 
            this.buttonUpgr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUpgr.Location = new System.Drawing.Point(413, 546);
            this.buttonUpgr.Name = "buttonUpgr";
            this.buttonUpgr.Size = new System.Drawing.Size(97, 28);
            this.buttonUpgr.TabIndex = 26;
            this.buttonUpgr.Text = "&Proved upgrade";
            this.buttonUpgr.UseVisualStyleBackColor = true;
            this.buttonUpgr.Click += new System.EventHandler(this.buttonUpgr_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(4, 546);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(954, 28);
            this.progressBar1.TabIndex = 25;
            this.progressBar1.Visible = false;
            // 
            // FormSoub
            // 
            this.AcceptButton = this.buttonUpgr;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 586);
            this.Controls.Add(this.buttonUpgr);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.tabControl);
            this.Name = "FormSoub";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Upgrade";
            this.Load += new System.EventHandler(this.CheckBoxVse_CheckedChanged);
            this.Shown += new System.EventHandler(this.FormSoub_Shown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.soubory.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage soubory;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button buttonUpgr;
        public System.Windows.Forms.ProgressBar progressBar1;
        public System.Windows.Forms.ListView listViewSoub;
        public System.Windows.Forms.ListView listView2Zmeny;
    }
}

