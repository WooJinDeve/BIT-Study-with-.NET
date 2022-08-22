
namespace _0412_실습
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.파일FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.저장SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.불러오기LToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.프로그램종료XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.도형선택ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.사각형RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.타원EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.삼각형TToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 426);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.파일FToolStripMenuItem,
            this.도형선택ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 파일FToolStripMenuItem
            // 
            this.파일FToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.저장SToolStripMenuItem,
            this.불러오기LToolStripMenuItem,
            this.toolStripMenuItem1,
            this.프로그램종료XToolStripMenuItem});
            this.파일FToolStripMenuItem.Name = "파일FToolStripMenuItem";
            this.파일FToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.파일FToolStripMenuItem.Text = "파일(&F)";
            // 
            // 저장SToolStripMenuItem
            // 
            this.저장SToolStripMenuItem.Name = "저장SToolStripMenuItem";
            this.저장SToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.저장SToolStripMenuItem.Text = "저장(&S)";
            this.저장SToolStripMenuItem.Click += new System.EventHandler(this.저장SToolStripMenuItem_Click);
            // 
            // 불러오기LToolStripMenuItem
            // 
            this.불러오기LToolStripMenuItem.Name = "불러오기LToolStripMenuItem";
            this.불러오기LToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.불러오기LToolStripMenuItem.Text = "불러오기(&L)";
            this.불러오기LToolStripMenuItem.Click += new System.EventHandler(this.불러오기LToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
            // 
            // 프로그램종료XToolStripMenuItem
            // 
            this.프로그램종료XToolStripMenuItem.Name = "프로그램종료XToolStripMenuItem";
            this.프로그램종료XToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.프로그램종료XToolStripMenuItem.Text = "프로그램종료(&X)";
            this.프로그램종료XToolStripMenuItem.Click += new System.EventHandler(this.프로그램종료XToolStripMenuItem_Click);
            // 
            // 도형선택ToolStripMenuItem
            // 
            this.도형선택ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.사각형RToolStripMenuItem,
            this.타원EToolStripMenuItem,
            this.삼각형TToolStripMenuItem});
            this.도형선택ToolStripMenuItem.Name = "도형선택ToolStripMenuItem";
            this.도형선택ToolStripMenuItem.Size = new System.Drawing.Size(82, 20);
            this.도형선택ToolStripMenuItem.Text = "도형선택(&S)";
            // 
            // 사각형RToolStripMenuItem
            // 
            this.사각형RToolStripMenuItem.Name = "사각형RToolStripMenuItem";
            this.사각형RToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.사각형RToolStripMenuItem.Text = "사각형(&R)";
            this.사각형RToolStripMenuItem.Click += new System.EventHandler(this.ShapeMenuItem_Click);
            // 
            // 타원EToolStripMenuItem
            // 
            this.타원EToolStripMenuItem.Name = "타원EToolStripMenuItem";
            this.타원EToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.타원EToolStripMenuItem.Text = "타원(&E)";
            this.타원EToolStripMenuItem.Click += new System.EventHandler(this.ShapeMenuItem_Click);
            // 
            // 삼각형TToolStripMenuItem
            // 
            this.삼각형TToolStripMenuItem.Name = "삼각형TToolStripMenuItem";
            this.삼각형TToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.삼각형TToolStripMenuItem.Text = "삼각형(&T)";
            this.삼각형TToolStripMenuItem.Click += new System.EventHandler(this.ShapeMenuItem_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(200, 24);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(600, 426);
            this.panel2.TabIndex = 2;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            this.panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseDown);
            this.panel2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseMove);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripMenuItem 파일FToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 저장SToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 불러오기LToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 프로그램종료XToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 도형선택ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 사각형RToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 타원EToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 삼각형TToolStripMenuItem;
    }
}

