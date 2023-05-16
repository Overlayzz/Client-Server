
namespace mBank.Forms
{
    partial class FormDepos
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
            this.btnGl = new System.Windows.Forms.Button();
            this.btnOper = new System.Windows.Forms.Button();
            this.btnHist = new System.Windows.Forms.Button();
            this.panelDesktopPaneDep = new System.Windows.Forms.Panel();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.btnAnalis = new System.Windows.Forms.Button();
            this.panelDesktopPaneDep.SuspendLayout();
            this.panelMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGl
            // 
            this.btnGl.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnGl.FlatAppearance.BorderSize = 0;
            this.btnGl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGl.Font = new System.Drawing.Font("Bell MT", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGl.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnGl.Location = new System.Drawing.Point(0, 0);
            this.btnGl.Name = "btnGl";
            this.btnGl.Size = new System.Drawing.Size(217, 34);
            this.btnGl.TabIndex = 16;
            this.btnGl.Text = "Главная";
            this.btnGl.UseVisualStyleBackColor = true;
            this.btnGl.Click += new System.EventHandler(this.btnGl_Click);
            // 
            // btnOper
            // 
            this.btnOper.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnOper.FlatAppearance.BorderSize = 0;
            this.btnOper.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOper.Font = new System.Drawing.Font("Bell MT", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOper.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnOper.Location = new System.Drawing.Point(217, 0);
            this.btnOper.Name = "btnOper";
            this.btnOper.Size = new System.Drawing.Size(217, 34);
            this.btnOper.TabIndex = 15;
            this.btnOper.Text = "Операции";
            this.btnOper.UseVisualStyleBackColor = true;
            this.btnOper.Click += new System.EventHandler(this.btnOper_Click);
            // 
            // btnHist
            // 
            this.btnHist.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnHist.FlatAppearance.BorderSize = 0;
            this.btnHist.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHist.Font = new System.Drawing.Font("Bell MT", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHist.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnHist.Location = new System.Drawing.Point(434, 0);
            this.btnHist.Name = "btnHist";
            this.btnHist.Size = new System.Drawing.Size(217, 34);
            this.btnHist.TabIndex = 14;
            this.btnHist.Text = "История";
            this.btnHist.UseVisualStyleBackColor = true;
            this.btnHist.Click += new System.EventHandler(this.btnHist_Click);
            // 
            // panelDesktopPaneDep
            // 
            this.panelDesktopPaneDep.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.panelDesktopPaneDep.Controls.Add(this.panelMenu);
            this.panelDesktopPaneDep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDesktopPaneDep.Location = new System.Drawing.Point(0, 0);
            this.panelDesktopPaneDep.Name = "panelDesktopPaneDep";
            this.panelDesktopPaneDep.Size = new System.Drawing.Size(867, 450);
            this.panelDesktopPaneDep.TabIndex = 17;
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.panelMenu.Controls.Add(this.btnAnalis);
            this.panelMenu.Controls.Add(this.btnHist);
            this.panelMenu.Controls.Add(this.btnOper);
            this.panelMenu.Controls.Add(this.btnGl);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(867, 34);
            this.panelMenu.TabIndex = 18;
            // 
            // btnAnalis
            // 
            this.btnAnalis.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnAnalis.FlatAppearance.BorderSize = 0;
            this.btnAnalis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnalis.Font = new System.Drawing.Font("Bell MT", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnalis.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAnalis.Location = new System.Drawing.Point(651, 0);
            this.btnAnalis.Name = "btnAnalis";
            this.btnAnalis.Size = new System.Drawing.Size(217, 34);
            this.btnAnalis.TabIndex = 17;
            this.btnAnalis.Text = "Анализ";
            this.btnAnalis.UseVisualStyleBackColor = true;
            this.btnAnalis.Click += new System.EventHandler(this.btnAnalis_Click);
            // 
            // FormDepos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(867, 450);
            this.Controls.Add(this.panelDesktopPaneDep);
            this.Name = "FormDepos";
            this.Text = "ДЕПОЗИТ";
            this.Load += new System.EventHandler(this.FormDepos_Load);
            this.panelDesktopPaneDep.ResumeLayout(false);
            this.panelMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnGl;
        private System.Windows.Forms.Button btnOper;
        private System.Windows.Forms.Button btnHist;
        private System.Windows.Forms.Panel panelDesktopPaneDep;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Button btnAnalis;
    }
}