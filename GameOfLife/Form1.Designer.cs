namespace GameOfLife
{
	partial class Form1
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
			this.GraphicsBox = new System.Windows.Forms.PictureBox();
			this.NextButton = new System.Windows.Forms.Button();
			this.ClearButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.GraphicsBox)).BeginInit();
			this.SuspendLayout();
			// 
			// GraphicsBox
			// 
			this.GraphicsBox.Location = new System.Drawing.Point(12, 12);
			this.GraphicsBox.Name = "GraphicsBox";
			this.GraphicsBox.Size = new System.Drawing.Size(623, 375);
			this.GraphicsBox.TabIndex = 0;
			this.GraphicsBox.TabStop = false;
			this.GraphicsBox.Click += new System.EventHandler(this.GraphicsBox_Click);
			this.GraphicsBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GraphicsBox_MouseDown);
			this.GraphicsBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GraphicsBox_MouseHover);
			// 
			// NextButton
			// 
			this.NextButton.Location = new System.Drawing.Point(641, 364);
			this.NextButton.Name = "NextButton";
			this.NextButton.Size = new System.Drawing.Size(75, 23);
			this.NextButton.TabIndex = 1;
			this.NextButton.Text = "Next (X)";
			this.NextButton.UseVisualStyleBackColor = true;
			this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
			// 
			// ClearButton
			// 
			this.ClearButton.Location = new System.Drawing.Point(641, 12);
			this.ClearButton.Name = "ClearButton";
			this.ClearButton.Size = new System.Drawing.Size(75, 23);
			this.ClearButton.TabIndex = 2;
			this.ClearButton.Text = "Clear";
			this.ClearButton.UseVisualStyleBackColor = true;
			this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(720, 412);
			this.Controls.Add(this.ClearButton);
			this.Controls.Add(this.NextButton);
			this.Controls.Add(this.GraphicsBox);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
			((System.ComponentModel.ISupportInitialize)(this.GraphicsBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox GraphicsBox;
		private System.Windows.Forms.Button NextButton;
		private System.Windows.Forms.Button ClearButton;
	}
}

