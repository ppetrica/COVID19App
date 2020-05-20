namespace COVID19App
{
    partial class MainForm
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
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.helpProvider = new System.Windows.Forms.HelpProvider();
            this.SuspendLayout();
            // 
            // mainTabControl
            // 
            this.mainTabControl.Location = new System.Drawing.Point(1, -2);
            this.mainTabControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(1467, 838);
            this.mainTabControl.TabIndex = 0;
            // 
            // helpProvider
            // 
            this.helpProvider.HelpNamespace = "help/COVID19AppHelp.chm";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1471, 838);
            this.Controls.Add(this.mainTabControl);
            this.helpProvider.SetHelpKeyword(this, "F1");
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.helpProvider.SetShowHelp(this, true);
            this.Text = "COVID-19 App";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.HelpProvider helpProvider;
    }
}

