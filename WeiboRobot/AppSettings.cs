using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qingrui.WeiboRobot
{
	public class AppSettings
	{
		public string ClientId { get;  set; }
		public string AppSecret { get;  set; }
		public string Redirect_url { get;  set; }
		public string SourceUrl { get;  set; } = "http://www.qingrui.vip";
		public string DefaultUrl { get;  set; } = "60.214.245.91";
		public string ContentFrom { get;   set; }
		public string HomePage { get; set; } = "https://www.weibo.com/";
		public int  TimeSpan { get; set; }
	}
}
