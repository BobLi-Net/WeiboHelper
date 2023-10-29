using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Qingrui.WeiboRobot
{
	public partial class Form1 : Form
	{
		private readonly string authorizeUrl; 
		private ResultDto accessToken = null;
		private  bool enableAuth = true;
		public AppSettings appSettings;
		//// 浏览器对象
		//public ChromiumWebBrowser chromeBrowser;
		public Form1()
		{
			InitializeComponent();
			appSettings= Utils.LoadSettings<AppSettings>(Application.StartupPath+"\\config\\settings.json");
			authorizeUrl = $"https://api.weibo.com/oauth2/authorize?response_type=code&client_id={appSettings.ClientId}&redirect_uri={appSettings.Redirect_url}";
			this.Width = 1200;
			this.Height= 700;
			this.StartPosition= FormStartPosition.CenterScreen;
			timer1.Interval = 1000 * appSettings.TimeSpan;
			AddLog( "启动程序", Color.LightGreen);
			
			//InitCef();
			CallAuth();
		}
	 
		public void CallAuth()
		{
			AddLog( "调起新浪微博Auth2.0认证", Color.LightGreen);
			this.WindowState= FormWindowState.Normal;
			timer1.Enabled = false; 
			webView21.Source=new Uri(authorizeUrl);
		}

		private WeiboContent GetContent()
		{
			try
			{
				AddLog( "请求远程内容", Color.LightGreen);

				var con = "";
				if (appSettings.ContentFrom.ToLower().StartsWith("http"))
				{
					using (HttpClient client = new HttpClient()) { 
						con=client.GetAsync(appSettings.ContentFrom).Result.Content.ReadAsStringAsync().Result;
					}
				}
				else {
					con=File.ReadAllText(appSettings.ContentFrom);
				}
				var conRes= ParseText(con);
				AddLog($"发现内容：{con.Split('\n')[0]} 请求远程内容", Color.LightGreen);
				return conRes;
			}
			catch (Exception ex)
			{
				AddLog( $"请求内容异常：{ex.Message}", Color.Red);
				return null; 
			}
		
		}
		private void SendContent(WeiboContent con)
		{
			try
			{
				AddLog( "开始发送内容", Color.LightGreen);
				string status = con.Content;
				
				status += $" {appSettings.SourceUrl}";
				IPHostEntry ipEntry = Dns.GetHostEntry(Dns.GetHostName());
				var rip = appSettings.DefaultUrl; 
				string url = "https://api.weibo.com/2/statuses/share.json"; 
				var formData = new List<KeyValuePair<string, string>>
				{
					new KeyValuePair<string, string>("access_token", accessToken.access_token),
					new KeyValuePair<string, string>("status", status),
					new KeyValuePair<string, string>("rip", rip)
				};
				var postContent = new FormUrlEncodedContent(formData);

				var request = new HttpRequestMessage(HttpMethod.Post, url)
				{
					Content = postContent
				}; 
				bool isSuc = true;
				using (HttpClient client = new HttpClient()) {
					var response = client.SendAsync(request).Result;
				
					if (!response.IsSuccessStatusCode)
					{
						isSuc = false;
						AddLog($"错误代码：{response.StatusCode}", Color.Red);
					}
					var str = response.Content.ReadAsStringAsync().Result;
					Console.WriteLine(str);
					if (str.IndexOf("error") > -1)
					{
						isSuc = false;
						AddLog($"===>\r\n{DateTime.Now.ToString("[dd HH:mm:ss]")} 发送请求返回值：{str}", Color.Red);
					}
					else {
						AddLog("发送结束", Color.LightGreen);
					}
					//无论成败都不重发
					File.WriteAllText("rec.dat", con.Title);
				}

				if (isSuc) {
					if (webView21.Source.ToString().ToLower() == appSettings.HomePage.ToLower())
						webView21.Reload();
					else
						webView21.Source = new Uri(appSettings.HomePage);
				}
				

			 
			}
			catch (Exception ex)
			{
				AddLog( $"发送内容异常：{ex.Message}", Color.Red);
			}
			

		}

 
 


		private ResultDto GetRequestToken(string code)
		{
			AddLog( "重新请求Token", Color.LightGreen);
			using (HttpClient client = new HttpClient()) {

				//client.BaseAddress = new Uri(url);
				var formValues = new Dictionary<string, string>
				{
					{"client_id", "957231558"},
					{"client_secret", "ff45000c663c3d159a71f5b21faf4868"},
					{"grant_type", "authorization_code"},
					{"code", code},
					{"redirect_uri", "http://wxzibo.zbszyqxt.com/"}
				};
				var postContent = new MultipartFormDataContent();
				//string boundary = string.Format("--{0}", DateTime.Now.Ticks.ToString("x"));
				//postContent.Headers.Add("ContentType", $"multipart/form-data, boundary={boundary}");
				var durl = "https://api.weibo.com/oauth2/access_token?";

				foreach (var kv in formValues) {
					durl += $"&{kv.Key}={kv.Value}";
				}
				durl = durl.Replace("?&", "?");
					//postContent.Add(new StringContent(kv.Value), kv.Key);

				var response = client.PostAsync(durl, null).Result;
				
				var str = response.Content.ReadAsStringAsync().Result;
				
				//this.WindowState = FormWindowState.Minimized;
				return JsonConvert.DeserializeObject<ResultDto>(str);

			}
			throw new Exception("获取token异常");
			
		   
		}
		private WeiboContent ParseText(string con) {
			var title = con.Split('\n')[0]; //去重复用
			var datetime = DateTime.Parse(con.Split('\n')[1]); //判断是否已过期用

			return new WeiboContent
			{
				Title = title,
				Content = con.Split('\n')[2],
				ReleaseTime = datetime
			};
		}
		private void timer1_Tick(object sender, EventArgs e)
		{
			try
			{
				 
				AddLog( "询检内容", Color.LightGreen);
				var con = GetContent();
				if (con==null)
				{
					AddLog( $"没有获取到内容", Color.LightGreen);
					return;
				}
				
				bool isSend = true;
				if (File.Exists("rec.dat"))
				{
					var rec = File.ReadAllText("rec.dat");

					if (rec.StartsWith(con.Title))
					{ 
							AddLog( $"重复的内容[{con.Title}]不发送", Color.LightGreen);
						isSend = false;
					}

				}
				if (con.ReleaseTime.AddHours(12) < DateTime.Now)
				{ 
						AddLog( $"超时的内容[{con.ReleaseTime}]不发送", Color.LightGreen);
					isSend = false;
				}
				if (isSend)
				{
					if (accessToken == null || accessToken.expires_at.AddSeconds(-30) < DateTime.Now)
					{
						AddLog( $"发现Token过期", Color.Orange);
						if (enableAuth)
						{

							CallAuth();
							timer1.Enabled = false;

						}

					}
					else
					{
						//this.WindowState = FormWindowState.Minimized;
						SendContent(con);

					}

				}
			}
			catch (Exception ex)
			{
				AddLog( $"timer1_Tick:{ex.Message}", Color.LightGreen); 
			}
			
		}

		private void webView21_SourceChanged(object sender, Microsoft.Web.WebView2.Core.CoreWebView2SourceChangedEventArgs e)
		{
			try
			{
				var url = webView21.Source.ToString();
				lblUrl.Text = url;
				if (url.IndexOf("code=") > -1)
				{
					AddLog( $"发现认证code", Color.LightGreen);
					var code = url.Replace("code=", "|").Split('|')[1].Split('&')[0];
					var token = GetRequestToken(code);
					token.expires_at = DateTime.Now.AddSeconds(token.expires_in);
					accessToken = token;
					using (System.IO.StreamWriter sw = new StreamWriter("token.dat"))
					{

						sw.Write(JsonConvert.SerializeObject(token));
					}
					webView21.Source = new Uri(appSettings.HomePage);
					timer1.Enabled = true;
				}
			}
			catch (Exception ex)
			{
				AddLog(  $"SourceChanged严重异常:{ex.Message} {webView21?.Source?.ToString()}", Color.LightGreen); 
			}
			
		}

		private void Form1_SizeChanged(object sender, EventArgs e)
		{
			
		}

		private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			this.Show();
			this.WindowState= FormWindowState.Maximized;
			this.ShowInTaskbar = true;
		}

		private void Form1_Resize(object sender, EventArgs e)
		{
			if (this.WindowState == FormWindowState.Minimized)
			{
				this.Hide();
				this.ShowInTaskbar = false;
			}
		}
		private int _lines = 0;
		/// <summary>
		/// 输出
		/// </summary>
		/// <param name="content"></param>
		/// <param name="color"></param>
		private void AddLog(string content, Color color)
		{
		 
				//超出一万行，清空
				this._lines++;
				if (this._lines > 10000)
				{
					this.rtLogs.Text = string.Empty;
					this._lines = 1;
				}

				content += Environment.NewLine;
				this.rtLogs.SelectionColor = color;//设置文本颜色
				this.rtLogs.AppendText(DateTime.Now.ToString("[dd HH:mm:ss]") + content);//输出文本，换行

				this.rtLogs.SelectionStart = this.rtLogs.Text.Length;//设置插入符位置为文本框末
				this.rtLogs.ScrollToCaret();//滚动条滚到到最新插入行
		 
		}
	}
}
