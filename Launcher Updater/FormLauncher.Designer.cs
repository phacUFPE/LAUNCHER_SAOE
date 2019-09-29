namespace Launcher_Updater
{
    partial class Launcher
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Launcher));
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblSize = new System.Windows.Forms.Label();
            this.lblCurrent = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.pctFileBorder = new System.Windows.Forms.PictureBox();
            this.pctFileProgress = new System.Windows.Forms.PictureBox();
            this.pctTotalBorder = new System.Windows.Forms.PictureBox();
            this.pctTotalProgress = new System.Windows.Forms.PictureBox();
            this.lblClientVer = new System.Windows.Forms.Label();
            this.lblLauncherVer = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.lblKBs = new System.Windows.Forms.Label();
            this.cboLanguages = new System.Windows.Forms.ComboBox();
            this.lblSite = new System.Windows.Forms.Label();
            this.lblRegister = new System.Windows.Forms.Label();
            this.lblDonate = new System.Windows.Forms.Label();
            this.lblFacebook = new System.Windows.Forms.Label();
            this.btnRepair = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.btnMain = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.cboEngine = new System.Windows.Forms.ComboBox();
            this.backgroundWorker_connection = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.pctFileBorder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctFileProgress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctTotalBorder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctTotalProgress)).BeginInit();
            this.SuspendLayout();
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_RunWorkerCompleted);
            // 
            // lblInfo
            // 
            this.lblInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblInfo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.ForeColor = System.Drawing.Color.White;
            this.lblInfo.Location = new System.Drawing.Point(67, 274);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(244, 14);
            this.lblInfo.TabIndex = 3;
            // 
            // lblSize
            // 
            this.lblSize.BackColor = System.Drawing.Color.Transparent;
            this.lblSize.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSize.ForeColor = System.Drawing.Color.White;
            this.lblSize.Location = new System.Drawing.Point(67, 289);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(319, 13);
            this.lblSize.TabIndex = 6;
            this.lblSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCurrent
            // 
            this.lblCurrent.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrent.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrent.ForeColor = System.Drawing.Color.White;
            this.lblCurrent.Location = new System.Drawing.Point(3, 237);
            this.lblCurrent.Name = "lblCurrent";
            this.lblCurrent.Size = new System.Drawing.Size(64, 13);
            this.lblCurrent.TabIndex = 15;
            this.lblCurrent.Text = "File";
            this.lblCurrent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotal
            // 
            this.lblTotal.BackColor = System.Drawing.Color.Transparent;
            this.lblTotal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.White;
            this.lblTotal.Location = new System.Drawing.Point(3, 257);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(64, 13);
            this.lblTotal.TabIndex = 16;
            this.lblTotal.Text = "Total";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pctFileBorder
            // 
            this.pctFileBorder.BackColor = System.Drawing.Color.Transparent;
            this.pctFileBorder.BackgroundImage = global::Launcher_Updater.Properties.Resources.healthbar_empty;
            this.pctFileBorder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pctFileBorder.ErrorImage = null;
            this.pctFileBorder.Location = new System.Drawing.Point(68, 237);
            this.pctFileBorder.Name = "pctFileBorder";
            this.pctFileBorder.Size = new System.Drawing.Size(402, 14);
            this.pctFileBorder.TabIndex = 20;
            this.pctFileBorder.TabStop = false;
            // 
            // pctFileProgress
            // 
            this.pctFileProgress.BackColor = System.Drawing.Color.Transparent;
            this.pctFileProgress.BackgroundImage = global::Launcher_Updater.Properties.Resources.healthbar_empty;
            this.pctFileProgress.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pctFileProgress.ErrorImage = null;
            this.pctFileProgress.Image = global::Launcher_Updater.Properties.Resources.healthbar_full;
            this.pctFileProgress.Location = new System.Drawing.Point(68, 237);
            this.pctFileProgress.Name = "pctFileProgress";
            this.pctFileProgress.Size = new System.Drawing.Size(402, 14);
            this.pctFileProgress.TabIndex = 21;
            this.pctFileProgress.TabStop = false;
            // 
            // pctTotalBorder
            // 
            this.pctTotalBorder.BackColor = System.Drawing.Color.Transparent;
            this.pctTotalBorder.BackgroundImage = global::Launcher_Updater.Properties.Resources.healthbar_empty;
            this.pctTotalBorder.Location = new System.Drawing.Point(68, 257);
            this.pctTotalBorder.Name = "pctTotalBorder";
            this.pctTotalBorder.Size = new System.Drawing.Size(402, 14);
            this.pctTotalBorder.TabIndex = 22;
            this.pctTotalBorder.TabStop = false;
            // 
            // pctTotalProgress
            // 
            this.pctTotalProgress.BackColor = System.Drawing.Color.Transparent;
            this.pctTotalProgress.BackgroundImage = global::Launcher_Updater.Properties.Resources.healthbar_empty;
            this.pctTotalProgress.Image = global::Launcher_Updater.Properties.Resources.healthbar_full;
            this.pctTotalProgress.Location = new System.Drawing.Point(68, 257);
            this.pctTotalProgress.Name = "pctTotalProgress";
            this.pctTotalProgress.Size = new System.Drawing.Size(402, 14);
            this.pctTotalProgress.TabIndex = 23;
            this.pctTotalProgress.TabStop = false;
            // 
            // lblClientVer
            // 
            this.lblClientVer.BackColor = System.Drawing.Color.Transparent;
            this.lblClientVer.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClientVer.ForeColor = System.Drawing.Color.White;
            this.lblClientVer.Location = new System.Drawing.Point(501, 333);
            this.lblClientVer.Name = "lblClientVer";
            this.lblClientVer.Size = new System.Drawing.Size(109, 13);
            this.lblClientVer.TabIndex = 25;
            this.lblClientVer.Text = "Client Ver:";
            this.lblClientVer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLauncherVer
            // 
            this.lblLauncherVer.BackColor = System.Drawing.Color.Transparent;
            this.lblLauncherVer.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLauncherVer.ForeColor = System.Drawing.Color.White;
            this.lblLauncherVer.Location = new System.Drawing.Point(504, 347);
            this.lblLauncherVer.Name = "lblLauncherVer";
            this.lblLauncherVer.Size = new System.Drawing.Size(106, 13);
            this.lblLauncherVer.TabIndex = 26;
            this.lblLauncherVer.Text = "Launcher Ver:";
            this.lblLauncherVer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCount
            // 
            this.lblCount.BackColor = System.Drawing.Color.Transparent;
            this.lblCount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.ForeColor = System.Drawing.Color.White;
            this.lblCount.Location = new System.Drawing.Point(317, 275);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(78, 12);
            this.lblCount.TabIndex = 27;
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblKBs
            // 
            this.lblKBs.BackColor = System.Drawing.Color.Transparent;
            this.lblKBs.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKBs.ForeColor = System.Drawing.Color.White;
            this.lblKBs.Location = new System.Drawing.Point(67, 302);
            this.lblKBs.Name = "lblKBs";
            this.lblKBs.Size = new System.Drawing.Size(114, 13);
            this.lblKBs.TabIndex = 28;
            this.lblKBs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboLanguages
            // 
            this.cboLanguages.BackColor = System.Drawing.Color.White;
            this.cboLanguages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLanguages.FormattingEnabled = true;
            this.cboLanguages.Location = new System.Drawing.Point(404, 4);
            this.cboLanguages.Name = "cboLanguages";
            this.cboLanguages.Size = new System.Drawing.Size(137, 21);
            this.cboLanguages.TabIndex = 29;
            this.cboLanguages.SelectionChangeCommitted += new System.EventHandler(this.CboLanguages_SelectionChangeCommitted);
            // 
            // lblSite
            // 
            this.lblSite.BackColor = System.Drawing.Color.Transparent;
            this.lblSite.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblSite.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSite.ForeColor = System.Drawing.Color.White;
            this.lblSite.Location = new System.Drawing.Point(44, 336);
            this.lblSite.Name = "lblSite";
            this.lblSite.Size = new System.Drawing.Size(87, 17);
            this.lblSite.TabIndex = 32;
            this.lblSite.Text = "Site";
            this.lblSite.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSite.Click += new System.EventHandler(this.RedirectLabels_Click);
            this.lblSite.MouseEnter += new System.EventHandler(this.OnMouseEnter);
            this.lblSite.MouseLeave += new System.EventHandler(this.OnMouseLeave);
            // 
            // lblRegister
            // 
            this.lblRegister.BackColor = System.Drawing.Color.Transparent;
            this.lblRegister.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblRegister.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegister.ForeColor = System.Drawing.Color.White;
            this.lblRegister.Location = new System.Drawing.Point(162, 336);
            this.lblRegister.Name = "lblRegister";
            this.lblRegister.Size = new System.Drawing.Size(87, 17);
            this.lblRegister.TabIndex = 33;
            this.lblRegister.Text = "Register";
            this.lblRegister.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRegister.Click += new System.EventHandler(this.RedirectLabels_Click);
            this.lblRegister.MouseEnter += new System.EventHandler(this.OnMouseEnter);
            this.lblRegister.MouseLeave += new System.EventHandler(this.OnMouseLeave);
            // 
            // lblDonate
            // 
            this.lblDonate.BackColor = System.Drawing.Color.Transparent;
            this.lblDonate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblDonate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDonate.ForeColor = System.Drawing.Color.White;
            this.lblDonate.Location = new System.Drawing.Point(278, 336);
            this.lblDonate.Name = "lblDonate";
            this.lblDonate.Size = new System.Drawing.Size(87, 17);
            this.lblDonate.TabIndex = 34;
            this.lblDonate.Text = "Donate";
            this.lblDonate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDonate.Click += new System.EventHandler(this.RedirectLabels_Click);
            this.lblDonate.MouseEnter += new System.EventHandler(this.OnMouseEnter);
            this.lblDonate.MouseLeave += new System.EventHandler(this.OnMouseLeave);
            // 
            // lblFacebook
            // 
            this.lblFacebook.BackColor = System.Drawing.Color.Transparent;
            this.lblFacebook.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblFacebook.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFacebook.ForeColor = System.Drawing.Color.White;
            this.lblFacebook.Location = new System.Drawing.Point(383, 336);
            this.lblFacebook.Name = "lblFacebook";
            this.lblFacebook.Size = new System.Drawing.Size(87, 17);
            this.lblFacebook.TabIndex = 35;
            this.lblFacebook.Text = "Facebook";
            this.lblFacebook.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblFacebook.Click += new System.EventHandler(this.RedirectLabels_Click);
            this.lblFacebook.MouseEnter += new System.EventHandler(this.OnMouseEnter);
            this.lblFacebook.MouseLeave += new System.EventHandler(this.OnMouseLeave);
            // 
            // btnRepair
            // 
            this.btnRepair.BackColor = System.Drawing.Color.Transparent;
            this.btnRepair.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRepair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRepair.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnRepair.FlatAppearance.BorderSize = 0;
            this.btnRepair.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnRepair.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnRepair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRepair.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRepair.ForeColor = System.Drawing.Color.White;
            this.btnRepair.Image = global::Launcher_Updater.Properties.Resources.new_wrench;
            this.btnRepair.Location = new System.Drawing.Point(1, 1);
            this.btnRepair.Name = "btnRepair";
            this.btnRepair.Size = new System.Drawing.Size(88, 35);
            this.btnRepair.TabIndex = 36;
            this.btnRepair.Text = "Reparar Arquivos";
            this.btnRepair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRepair.UseVisualStyleBackColor = false;
            this.btnRepair.Click += new System.EventHandler(this.BtnRepair_Click);
            this.btnRepair.MouseHover += new System.EventHandler(this.BtnRepair_MouseHover);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImage = global::Launcher_Updater.Properties.Resources.close_white;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(582, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(25, 25);
            this.btnClose.TabIndex = 37;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            this.btnClose.MouseEnter += new System.EventHandler(this.BtnClose_MouseEnter);
            this.btnClose.MouseLeave += new System.EventHandler(this.BtnClose_MouseLeave);
            // 
            // btnMinimize
            // 
            this.btnMinimize.BackColor = System.Drawing.Color.Transparent;
            this.btnMinimize.BackgroundImage = global::Launcher_Updater.Properties.Resources.minimize_white;
            this.btnMinimize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMinimize.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            this.btnMinimize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnMinimize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMinimize.ForeColor = System.Drawing.Color.Black;
            this.btnMinimize.Location = new System.Drawing.Point(553, 2);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(25, 25);
            this.btnMinimize.TabIndex = 38;
            this.btnMinimize.UseVisualStyleBackColor = false;
            this.btnMinimize.Click += new System.EventHandler(this.BtnMinimize_Click);
            this.btnMinimize.MouseEnter += new System.EventHandler(this.BtnMinimize_MouseEnter);
            this.btnMinimize.MouseLeave += new System.EventHandler(this.BtnMinimize_MouseLeave);
            // 
            // btnMain
            // 
            this.btnMain.BackColor = System.Drawing.Color.Transparent;
            this.btnMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMain.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnMain.FlatAppearance.BorderSize = 0;
            this.btnMain.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnMain.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnMain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMain.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMain.ForeColor = System.Drawing.Color.White;
            this.btnMain.Image = ((System.Drawing.Image)(resources.GetObject("btnMain.Image")));
            this.btnMain.Location = new System.Drawing.Point(490, 229);
            this.btnMain.Name = "btnMain";
            this.btnMain.Size = new System.Drawing.Size(112, 50);
            this.btnMain.TabIndex = 0;
            this.btnMain.Text = "Link Start";
            this.btnMain.UseVisualStyleBackColor = false;
            this.btnMain.Click += new System.EventHandler(this.BtnMain_Click);
            // 
            // cboEngine
            // 
            this.cboEngine.BackColor = System.Drawing.Color.White;
            this.cboEngine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEngine.FormattingEnabled = true;
            this.cboEngine.Location = new System.Drawing.Point(304, 4);
            this.cboEngine.Name = "cboEngine";
            this.cboEngine.Size = new System.Drawing.Size(94, 21);
            this.cboEngine.TabIndex = 39;
            this.cboEngine.SelectedIndexChanged += new System.EventHandler(this.CboEngine_SelectedIndexChanged);
            // 
            // Launcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.BackgroundImage = global::Launcher_Updater.Properties.Resources.bg_saoe;
            this.ClientSize = new System.Drawing.Size(610, 361);
            this.Controls.Add(this.cboEngine);
            this.Controls.Add(this.btnMinimize);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRepair);
            this.Controls.Add(this.lblFacebook);
            this.Controls.Add(this.lblDonate);
            this.Controls.Add(this.lblRegister);
            this.Controls.Add(this.lblSite);
            this.Controls.Add(this.cboLanguages);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.lblKBs);
            this.Controls.Add(this.lblClientVer);
            this.Controls.Add(this.pctTotalProgress);
            this.Controls.Add(this.pctTotalBorder);
            this.Controls.Add(this.pctFileProgress);
            this.Controls.Add(this.pctFileBorder);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblCurrent);
            this.Controls.Add(this.lblSize);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.btnMain);
            this.Controls.Add(this.lblLauncherVer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Launcher";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Launcher";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Launcher_Shown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.pctFileBorder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctFileProgress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctTotalBorder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctTotalProgress)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label lblCurrent;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.PictureBox pctFileBorder;
        private System.Windows.Forms.PictureBox pctFileProgress;
        private System.Windows.Forms.PictureBox pctTotalBorder;
        private System.Windows.Forms.PictureBox pctTotalProgress;
        private System.Windows.Forms.Label lblClientVer;
        private System.Windows.Forms.Label lblLauncherVer;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label lblKBs;
        private System.Windows.Forms.ComboBox cboLanguages;
        private System.Windows.Forms.Label lblSite;
        private System.Windows.Forms.Label lblRegister;
        private System.Windows.Forms.Label lblDonate;
        private System.Windows.Forms.Label lblFacebook;
        private System.Windows.Forms.Button btnRepair;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.Button btnMain;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ComboBox cboEngine;
        private System.ComponentModel.BackgroundWorker backgroundWorker_connection;
    }
}

