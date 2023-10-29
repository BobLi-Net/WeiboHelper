using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Reflection;

namespace Qingrui.WeiboRobot
{
	public class Utils
	{
		public static T LoadSettings<T>(string filePath) {
			var con = File.ReadAllText(filePath);
			 
			return JsonConvert.DeserializeObject<T>(con);
		}
		 

		public static bool RunningModeIsDebug
		{
			get
			{

				var assebly = Assembly.GetEntryAssembly();
				if (assebly == null)
				{
					assebly = new StackTrace().GetFrames().Last().GetMethod().Module.Assembly;
				}

				var debugableAttribute = assebly.GetCustomAttribute<DebuggableAttribute>();
				var isdebug = debugableAttribute.DebuggingFlags.HasFlag(DebuggableAttribute.DebuggingModes.EnableEditAndContinue);

				return isdebug;
			}
		}


	}
}
