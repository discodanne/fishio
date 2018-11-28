namespace Tracking
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
            this.redLower = new System.Windows.Forms.NumericUpDown();
            this.redUpper = new System.Windows.Forms.NumericUpDown();
            this.greenUpper = new System.Windows.Forms.NumericUpDown();
            this.greenLower = new System.Windows.Forms.NumericUpDown();
            this.blueUpper = new System.Windows.Forms.NumericUpDown();
            this.blueLower = new System.Windows.Forms.NumericUpDown();
            this.debugTrackingCB = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.redLower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.redUpper)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.greenUpper)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.greenLower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blueUpper)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blueLower)).BeginInit();
            this.SuspendLayout();
            // 
            // redLower
            // 
            this.redLower.Location = new System.Drawing.Point(0, 0);
            this.redLower.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.redLower.Name = "redLower";
            this.redLower.Size = new System.Drawing.Size(50, 20);
            this.redLower.TabIndex = 0;
            this.redLower.ValueChanged += new System.EventHandler(this.redLower_ValueChanged);
            // 
            // redUpper
            // 
            this.redUpper.Location = new System.Drawing.Point(51, 0);
            this.redUpper.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.redUpper.Name = "redUpper";
            this.redUpper.Size = new System.Drawing.Size(50, 20);
            this.redUpper.TabIndex = 1;
            this.redUpper.ValueChanged += new System.EventHandler(this.redUpper_ValueChanged);
            // 
            // greenUpper
            // 
            this.greenUpper.Location = new System.Drawing.Point(171, 0);
            this.greenUpper.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.greenUpper.Name = "greenUpper";
            this.greenUpper.Size = new System.Drawing.Size(50, 20);
            this.greenUpper.TabIndex = 3;
            this.greenUpper.ValueChanged += new System.EventHandler(this.greenUpper_ValueChanged);
            // 
            // greenLower
            // 
            this.greenLower.Location = new System.Drawing.Point(120, 0);
            this.greenLower.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.greenLower.Name = "greenLower";
            this.greenLower.Size = new System.Drawing.Size(50, 20);
            this.greenLower.TabIndex = 2;
            this.greenLower.ValueChanged += new System.EventHandler(this.greenLower_ValueChanged);
            // 
            // blueUpper
            // 
            this.blueUpper.Location = new System.Drawing.Point(291, 0);
            this.blueUpper.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.blueUpper.Name = "blueUpper";
            this.blueUpper.Size = new System.Drawing.Size(50, 20);
            this.blueUpper.TabIndex = 5;
            this.blueUpper.ValueChanged += new System.EventHandler(this.blueUpper_ValueChanged);
            // 
            // blueLower
            // 
            this.blueLower.Location = new System.Drawing.Point(240, 0);
            this.blueLower.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.blueLower.Name = "blueLower";
            this.blueLower.Size = new System.Drawing.Size(50, 20);
            this.blueLower.TabIndex = 4;
            this.blueLower.ValueChanged += new System.EventHandler(this.blueLower_ValueChanged);
            // 
            // debugTrackingCB
            // 
            this.debugTrackingCB.AutoSize = true;
            this.debugTrackingCB.Location = new System.Drawing.Point(348, 2);
            this.debugTrackingCB.Name = "debugTrackingCB";
            this.debugTrackingCB.Size = new System.Drawing.Size(64, 17);
            this.debugTrackingCB.TabIndex = 6;
            this.debugTrackingCB.Text = "Debug?";
            this.debugTrackingCB.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 743);
            this.Controls.Add(this.debugTrackingCB);
            this.Controls.Add(this.blueUpper);
            this.Controls.Add(this.blueLower);
            this.Controls.Add(this.greenUpper);
            this.Controls.Add(this.greenLower);
            this.Controls.Add(this.redUpper);
            this.Controls.Add(this.redLower);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.redLower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.redUpper)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.greenUpper)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.greenLower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blueUpper)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blueLower)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown redLower;
        private System.Windows.Forms.NumericUpDown redUpper;
        private System.Windows.Forms.NumericUpDown greenUpper;
        private System.Windows.Forms.NumericUpDown greenLower;
        private System.Windows.Forms.NumericUpDown blueUpper;
        private System.Windows.Forms.NumericUpDown blueLower;
        private System.Windows.Forms.CheckBox debugTrackingCB;
    }
}

