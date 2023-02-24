using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
	public string name;
	public int rating;
	
	public PlayerData(Player player)
	{
		name = player.Name();
		rating = (int)player.Rating();
	}
}
