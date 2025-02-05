﻿using System;
using System.Collections.Generic;
using System.IO;
using TShockAPI;
using Newtonsoft.Json;

namespace AutoRank
{
	public class Config
	{
		private static string savepath = Path.Combine(TShock.SavePath, "AutoRank");
		private static string filepath = Path.Combine(savepath, "AutoRank.json");
		public bool AutoRank = true;
		public bool RankUpCostMoney = true;
		public string RankUpMessage = "[AutoRank] You've been auto-ranked to %GROUP%!";
		public string RankCmdAlias = "rank";
		public string RankUpCmd = "rankup";
		public string RankCmdMsg = "[$rankindex/$rankcount] Current Rank: $rankname. Next rank in $remainder.";
		public string MaxRankMsg = "[$rankindex/$rankcount] Current Rank: $rankname.";
		public List<Rank> Ranks = new List<Rank>()
		{
			new Rank("TestRank")
			{
				parentgroup = "Parent",
				group = "Group",
				cost = "100"
			}
		};

		private static void Write(Config file)
		{
			try
			{
				File.WriteAllText(filepath, JsonConvert.SerializeObject(file, Formatting.Indented));
			}
			catch (Exception ex)
			{
				TShock.Log.ConsoleError("[AutoRank] Exception at 'Config.Write': {0}\nCheck logs for details.",
						ex.Message);
				TShock.Log.Error(ex.ToString());
			}
		}

		public static Config Read()
		{
			Directory.CreateDirectory(savepath);

			Config file = new Config();
			try
			{
				if (!File.Exists(filepath))
				{
					Write(file);
				}
				else
				{
					file = JsonConvert.DeserializeObject<Config>(File.ReadAllText(filepath));
				}
			}
			catch (Exception ex)
			{
				TShock.Log.ConsoleError("[AutoRank] Exception at 'Config.Read': {0}\nCheck logs for details.",
						ex.Message);
				TShock.Log.Error(ex.ToString());
			}
			return file;
		}
	}
}
