using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class AppData
{
	public List<PlayerData> players;
	
	public AppData(List<Player> playerList)
	{
		players = new List<PlayerData>();
		foreach(var item in playerList)
		{
			PlayerData playerData = new PlayerData(item);		
			players.Add(playerData);
		}
	}
}
