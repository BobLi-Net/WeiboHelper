namespace Qingrui.WeiboRobot
{
	partial class Form1
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.label1 = new System.Windows.Forms.Label();
			this.lblUrl = new System.Windows.Forms.Label();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
			this.panel1 = new System.Windows.Forms.Panel();
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.rtLogs = new System.Windows.Forms.RichTextBox();
			((System.ComponentModel.ISupportInitialize)(this.webView21)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(82, 15);
			this.label1.TabIndex = 4;
			this.label1.Text = "当前地址：";
			// 
			// lblUrl
			// 
			this.lblUrl.AutoSize = true;
			this.lblUrl.Location = new System.Drawing.Point(100, 18);
			this.lblUrl.Name = "lblUrl";
			this.lblUrl.Size = new System.Drawing.Size(31, 15);
			this.lblUrl.TabIndex = 7;
			this.lblUrl.Text = "...";
			// 
			// timer1
			// 
			this.timer1.Interval = 30000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// webView21
			// 
			this.webView21.AllowExternalDrop = true;
			this.webView21.CreationProperties = null;
			this.webView21.DefaultBackgroundColor = System.Drawing.Color.White;
			this.webView21.Dock = System.Windows.Forms.DockStyle.Fill;
			this.webView21.Location = new System.Drawing.Point(0, 0);
			this.webView21.Name = "webView21";
			this.webView21.Size = new System.Drawing.Size(889, 666);
			this.webView21.TabIndex = 0;
			this.webView21.ZoomFactor = 1D;
			this.webView21.SourceChanged += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2SourceChangedEventArgs>(this.webView21_SourceChanged);
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.Controls.Add(this.webView21);
			this.panel1.Location = new System.Drawing.Point(15, 53);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(889, 666);
			this.panel1.TabIndex = 8;
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
			this.notifyIcon1.Text = "微博发布助手";
			this.notifyIcon1.Visible = true;
			this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
			// 
			// rtLogs
			// 
			this.rtLogs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.rtLogs.BackColor = System.Drawing.Color.Black;
			this.rtLogs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.rtLogs.Location = new System.Drawing.Point(910, 53);
			this.rtLogs.Name = "rtLogs";
			this.rtLogs.Size = new System.Drawing.Size(425, 666);
			this.rtLogs.TabIndex = 10;
			this.rtLogs.Text = "";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1347, 746);
			this.Controls.Add(this.rtLogs);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.lblUrl);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Form1";
			this.Text = "微博发布助手";
			this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
			this.Resize += new System.EventHandler(this.Form1_Resize);
			((System.ComponentModel.ISupportInitialize)(this.webView21)).EndInit();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblUrl;
		private System.Windows.Forms.Timer timer1;
		private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.NotifyIcon notifyIcon1;
		private System.Windows.Forms.RichTextBox rtLogs;
	}
}

