namespace Fabricator
{
    partial class FabricatorLoadStringPrompt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FabricatorLoadStringPrompt));
            textBox1 = new TextBox();
            Apply = new Button();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.Location = new Point(12, 16);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(384, 23);
            textBox1.TabIndex = 0;
            // 
            // Apply
            // 
            Apply.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Apply.Location = new Point(12, 45);
            Apply.Name = "Apply";
            Apply.Size = new Size(384, 23);
            Apply.TabIndex = 1;
            Apply.Text = "Apply";
            Apply.UseVisualStyleBackColor = true;
            Apply.Click += Apply_Click;
            // 
            // FabricatorLoadStringPrompt
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(408, 80);
            Controls.Add(Apply);
            Controls.Add(textBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FabricatorLoadStringPrompt";
            Text = "FabricatorLoadStringPrompt";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Button Apply;
    }
}