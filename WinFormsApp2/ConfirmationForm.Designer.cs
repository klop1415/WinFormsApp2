namespace WinFormsApp2
{
    partial class ConfirmationForm
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
            button1 = new Button();
            button2 = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.PaleGreen;
            button1.Cursor = Cursors.Hand;
            button1.DialogResult = DialogResult.Cancel;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Location = new Point(67, 78);
            button1.Margin = new Padding(4);
            button1.Name = "button1";
            button1.Size = new Size(113, 40);
            button1.TabIndex = 0;
            button1.Text = "отставить";
            button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            button2.BackColor = Color.Coral;
            button2.Cursor = Cursors.Hand;
            button2.DialogResult = DialogResult.OK;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Location = new Point(202, 78);
            button2.Margin = new Padding(4);
            button2.Name = "button2";
            button2.Size = new Size(122, 40);
            button2.TabIndex = 1;
            button2.Text = "давай уже";
            button2.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F);
            label1.Location = new Point(56, 26);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(297, 25);
            label1.TabIndex = 2;
            label1.Text = "Удалится безвозвратно! Уверен?";
            // 
            // ConfirmationForm
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.MistyRose;
            ClientSize = new Size(392, 165);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(button1);
            Font = new Font("Segoe UI", 12F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4);
            Name = "ConfirmationForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ConfirmationForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button2;
        private Label label1;
    }
}