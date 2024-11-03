namespace HwMonitorTask
{
    partial class Setting
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Setting));
            checkBox_startUp = new CheckBox();
            SuspendLayout();
            // 
            // checkBox_startUp
            // 
            checkBox_startUp.AutoSize = true;
            checkBox_startUp.Location = new Point(12, 12);
            checkBox_startUp.Name = "checkBox_startUp";
            checkBox_startUp.Size = new Size(176, 24);
            checkBox_startUp.TabIndex = 0;
            checkBox_startUp.Text = "With system startup";
            checkBox_startUp.UseVisualStyleBackColor = true;
            // 
            // Setting
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(216, 48);
            Controls.Add(checkBox_startUp);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Setting";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox checkBox_startUp;
    }
}
