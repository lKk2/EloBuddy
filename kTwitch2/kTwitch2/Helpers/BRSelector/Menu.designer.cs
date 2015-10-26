using System.ComponentModel;
using System.Windows.Forms;

namespace BRSelector.External
{
    partial class Menu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.selectorType = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.champion1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.champion5 = new System.Windows.Forms.Label();
            this.trackBar5 = new System.Windows.Forms.TrackBar();
            this.champion4 = new System.Windows.Forms.Label();
            this.trackBar4 = new System.Windows.Forms.TrackBar();
            this.champion3 = new System.Windows.Forms.Label();
            this.trackBar3 = new System.Windows.Forms.TrackBar();
            this.champion2 = new System.Windows.Forms.Label();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.showTarget = new System.Windows.Forms.CheckBox();
            this.drawForcedTarget = new System.Windows.Forms.CheckBox();
            this.forceSelectedTarget = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "BRSelector";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(269, 488);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "By KK2 & Vector";
            // 
            // selectorType
            // 
            this.selectorType.FormattingEnabled = true;
            this.selectorType.Items.AddRange(new object[] {
            "AutoPriority",
            "LessAttack",
            "MostAP ",
            "MostAD",
            "Closest",
            "NearMouse",
            "LessCast",
            "LessHealth",
            "Priority",
            "Points"});
            this.selectorType.Location = new System.Drawing.Point(12, 103);
            this.selectorType.Name = "selectorType";
            this.selectorType.Size = new System.Drawing.Size(332, 69);
            this.selectorType.TabIndex = 4;
            this.selectorType.SelectedIndexChanged += new System.EventHandler(this.selectorType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Selector Type";
            // 
            // champion1
            // 
            this.champion1.AutoSize = true;
            this.champion1.Location = new System.Drawing.Point(16, 196);
            this.champion1.Name = "champion1";
            this.champion1.Size = new System.Drawing.Size(63, 13);
            this.champion1.TabIndex = 7;
            this.champion1.Text = "Champion 1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.champion5);
            this.panel1.Controls.Add(this.trackBar5);
            this.panel1.Controls.Add(this.champion4);
            this.panel1.Controls.Add(this.trackBar4);
            this.panel1.Controls.Add(this.champion3);
            this.panel1.Controls.Add(this.trackBar3);
            this.panel1.Controls.Add(this.champion2);
            this.panel1.Controls.Add(this.trackBar2);
            this.panel1.Controls.Add(this.trackBar1);
            this.panel1.Location = new System.Drawing.Point(12, 178);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(330, 295);
            this.panel1.TabIndex = 14;
            // 
            // champion5
            // 
            this.champion5.AutoSize = true;
            this.champion5.Location = new System.Drawing.Point(4, 237);
            this.champion5.Name = "champion5";
            this.champion5.Size = new System.Drawing.Size(63, 13);
            this.champion5.TabIndex = 15;
            this.champion5.Text = "Champion 5";
            // 
            // trackBar5
            // 
            this.trackBar5.AutoSize = false;
            this.trackBar5.Location = new System.Drawing.Point(2, 255);
            this.trackBar5.Maximum = 3;
            this.trackBar5.Name = "trackBar5";
            this.trackBar5.Size = new System.Drawing.Size(326, 35);
            this.trackBar5.TabIndex = 14;
            this.trackBar5.Scroll += new System.EventHandler(this.trackBar5_Scroll);
            // 
            // champion4
            // 
            this.champion4.AutoSize = true;
            this.champion4.Location = new System.Drawing.Point(4, 181);
            this.champion4.Name = "champion4";
            this.champion4.Size = new System.Drawing.Size(63, 13);
            this.champion4.TabIndex = 13;
            this.champion4.Text = "Champion 4";
            // 
            // trackBar4
            // 
            this.trackBar4.AutoSize = false;
            this.trackBar4.Location = new System.Drawing.Point(1, 199);
            this.trackBar4.Maximum = 3;
            this.trackBar4.Name = "trackBar4";
            this.trackBar4.Size = new System.Drawing.Size(326, 35);
            this.trackBar4.TabIndex = 12;
            this.trackBar4.Scroll += new System.EventHandler(this.trackBar4_Scroll);
            // 
            // champion3
            // 
            this.champion3.AutoSize = true;
            this.champion3.Location = new System.Drawing.Point(4, 124);
            this.champion3.Name = "champion3";
            this.champion3.Size = new System.Drawing.Size(63, 13);
            this.champion3.TabIndex = 11;
            this.champion3.Text = "Champion 3";
            // 
            // trackBar3
            // 
            this.trackBar3.AutoSize = false;
            this.trackBar3.Location = new System.Drawing.Point(2, 143);
            this.trackBar3.Maximum = 3;
            this.trackBar3.Name = "trackBar3";
            this.trackBar3.Size = new System.Drawing.Size(326, 35);
            this.trackBar3.TabIndex = 10;
            this.trackBar3.Scroll += new System.EventHandler(this.trackBar3_Scroll);
            // 
            // champion2
            // 
            this.champion2.AutoSize = true;
            this.champion2.Location = new System.Drawing.Point(4, 68);
            this.champion2.Name = "champion2";
            this.champion2.Size = new System.Drawing.Size(63, 13);
            this.champion2.TabIndex = 9;
            this.champion2.Text = "Champion 2";
            // 
            // trackBar2
            // 
            this.trackBar2.AutoSize = false;
            this.trackBar2.Location = new System.Drawing.Point(2, 86);
            this.trackBar2.Maximum = 3;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(326, 35);
            this.trackBar2.TabIndex = 8;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // trackBar1
            // 
            this.trackBar1.AutoSize = false;
            this.trackBar1.Location = new System.Drawing.Point(2, 35);
            this.trackBar1.Maximum = 3;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(326, 35);
            this.trackBar1.TabIndex = 0;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // showTarget
            // 
            this.showTarget.AutoSize = true;
            this.showTarget.Location = new System.Drawing.Point(16, 30);
            this.showTarget.Name = "showTarget";
            this.showTarget.Size = new System.Drawing.Size(87, 17);
            this.showTarget.TabIndex = 15;
            this.showTarget.Text = "Show Target";
            this.showTarget.UseVisualStyleBackColor = true;
            this.showTarget.CheckedChanged += new System.EventHandler(this.showTarget_CheckedChanged);
            // 
            // drawForcedTarget
            // 
            this.drawForcedTarget.AutoSize = true;
            this.drawForcedTarget.Location = new System.Drawing.Point(109, 30);
            this.drawForcedTarget.Name = "drawForcedTarget";
            this.drawForcedTarget.Size = new System.Drawing.Size(121, 17);
            this.drawForcedTarget.TabIndex = 16;
            this.drawForcedTarget.Text = "Draw Forced Target";
            this.drawForcedTarget.UseVisualStyleBackColor = true;
            this.drawForcedTarget.CheckedChanged += new System.EventHandler(this.drawForcedTarget_CheckedChanged);
            // 
            // forceSelectedTarget
            // 
            this.forceSelectedTarget.AutoSize = true;
            this.forceSelectedTarget.Location = new System.Drawing.Point(16, 53);
            this.forceSelectedTarget.Name = "forceSelectedTarget";
            this.forceSelectedTarget.Size = new System.Drawing.Size(132, 17);
            this.forceSelectedTarget.TabIndex = 17;
            this.forceSelectedTarget.Text = "Force Selected Target";
            this.forceSelectedTarget.UseVisualStyleBackColor = true;
            this.forceSelectedTarget.CheckedChanged += new System.EventHandler(this.forceSelectedTarget_CheckedChanged);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 510);
            this.Controls.Add(this.forceSelectedTarget);
            this.Controls.Add(this.drawForcedTarget);
            this.Controls.Add(this.showTarget);
            this.Controls.Add(this.champion1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.selectorType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Name = "Menu";
            this.Text = "Menu";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public Label label1;
        public Label label2;
        public ListBox selectorType;
        public Label label3;
        public Label champion1;
        public Panel panel1;
        public Label champion5;
        public TrackBar trackBar5;
        public Label champion4;
        public TrackBar trackBar4;
        public Label champion3;
        public TrackBar trackBar3;
        public Label champion2;
        public TrackBar trackBar2;
        public TrackBar trackBar1;
        public CheckBox showTarget;
        public CheckBox drawForcedTarget;
        public CheckBox forceSelectedTarget;
    }
}