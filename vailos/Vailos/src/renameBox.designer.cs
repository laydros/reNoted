namespace Vailos
{
    partial class renameBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(renameBox));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.renameFileTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.renameBoxCancelButton = new System.Windows.Forms.Button();
            this.renameBoxRenameButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tableLayoutPanel1.Controls.Add(this.renameFileTextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.renameBoxCancelButton, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.renameBoxRenameButton, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60.06601F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 39.93399F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(305, 102);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // renameFileTextBox
            // 
            this.renameFileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.renameFileTextBox, 2);
            this.renameFileTextBox.Location = new System.Drawing.Point(115, 20);
            this.renameFileTextBox.Margin = new System.Windows.Forms.Padding(10);
            this.renameFileTextBox.Name = "renameFileTextBox";
            this.renameFileTextBox.Size = new System.Drawing.Size(180, 20);
            this.renameFileTextBox.TabIndex = 3;
            this.renameFileTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.renameFileTextBox_KeyPress);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "New File Name:";
            // 
            // renameBoxCancelButton
            // 
            this.renameBoxCancelButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.renameBoxCancelButton.Location = new System.Drawing.Point(215, 70);
            this.renameBoxCancelButton.MaximumSize = new System.Drawing.Size(75, 23);
            this.renameBoxCancelButton.MinimumSize = new System.Drawing.Size(75, 23);
            this.renameBoxCancelButton.Name = "renameBoxCancelButton";
            this.renameBoxCancelButton.Size = new System.Drawing.Size(75, 23);
            this.renameBoxCancelButton.TabIndex = 6;
            this.renameBoxCancelButton.Text = "Cancel";
            this.renameBoxCancelButton.UseVisualStyleBackColor = true;
            this.renameBoxCancelButton.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // renameBoxRenameButton
            // 
            this.renameBoxRenameButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.renameBoxRenameButton.Location = new System.Drawing.Point(15, 70);
            this.renameBoxRenameButton.MaximumSize = new System.Drawing.Size(75, 23);
            this.renameBoxRenameButton.MinimumSize = new System.Drawing.Size(75, 23);
            this.renameBoxRenameButton.Name = "renameBoxRenameButton";
            this.renameBoxRenameButton.Size = new System.Drawing.Size(75, 23);
            this.renameBoxRenameButton.TabIndex = 5;
            this.renameBoxRenameButton.Text = "Rename";
            this.renameBoxRenameButton.UseVisualStyleBackColor = true;
            this.renameBoxRenameButton.Click += new System.EventHandler(this.renameBoxRenameButton_Click);
            // 
            // renameBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 102);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "renameBox";
            this.Text = "Rename File";
            this.Load += new System.EventHandler(this.renameBox_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox renameFileTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button renameBoxRenameButton;
        private System.Windows.Forms.Button renameBoxCancelButton;
    }
}