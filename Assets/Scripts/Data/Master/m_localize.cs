using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class m_localize 
{
	static Dictionary<string, m_localize> master = new Dictionary<string, m_localize>();

	public static void Load (Master.Type masterType) 
	{
		master.Clear();

		switch (masterType) {
		case Master.Type.LoadfromFile :
			List<List<string>> csv = Master.GetCSV("m_localize");
			for (int i = 0; i < csv.Count; i++) {
				master.Add(csv[i][0], new m_localize(csv[i]));
			}
			break;
		case Master.Type.LoadFromServer : 
			break;
		}
	}

	public static string Get (string key)
	{
		m_localize data = master[key];
		switch (ProjectSetting.language) {
		case E.Languages.English : 
			return data.value_1;
		case E.Languages.Korean : 
			return data.value_2;
		case E.Languages.Japanese : 
			return data.value_3;
		default :
			Debug.Log("Cannot Find Localize Value");
			return null;
		}
	}

	public static string GetWithParams (string key, params string[] param)
	{
		return string.Format(Get(key), param);
	}

	string key;
	string value_1;
	string value_2;
	string value_3;

	public m_localize (List<string> data) 
	{
		this.key = data[0];
		this.value_1 = data[1];
		this.value_2 = data[2];
		this.value_3 = data[3];
	}
}
