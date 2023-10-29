using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qingrui.WeiboRobot
{
	public class ResultDto
	{
		public DateTime expires_at { get; set; }

		public string access_token { get; set; }
		public long expires_in { get; set; }
	}
}
