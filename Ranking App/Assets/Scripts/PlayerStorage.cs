using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStorage : MonoBehaviour
{
	[SerializeField] List<Player> players;
	[SerializeField] GameObject nameInputField;
	[SerializeField] GameObject ratingInputField;
	[SerializeField] GameObject playerPrefab;


	public void AddPlayer()
	{
		GameObject playerObject = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
		playerObject.name = "player_" +
			nameInputField.GetComponent<Text>().text;
		Player newPlayer = playerObject.GetComponent<Player>();

		//Setting values
		newPlayer.SetName(nameInputField.GetComponent<Text>().text);
		if (int.TryParse(ratingInputField.GetComponent<Text>().text, out int numValue))
		{
			newPlayer.SetRating(numValue);
		}
		else
		{
			newPlayer.SetRating(400);
		}

		//Adding Player
		players.Add(newPlayer);
		FindObjectOfType<UIManager>().ShowMainCanvas();
	}

	public void LoadPlayers()
	{
		foreach(var item in players)
		{
			players.Remove(item);
			Destroy(item.gameObject);
		}
		
		AppData data = SaveSystem.LoadPlayers();
		foreach (var item in data.players)
		{
			GameObject playerObject = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
			playerObject.name = "player_" + item.name;
			Player newPlayer = playerObject.GetComponent<Player>();

			//Setting values
			newPlayer.SetName(item.name);
			newPlayer.SetRating(item.rating);
			
			//Adding Player
			players.Add(newPlayer);
		}
	}
	
	public void SavePlayers()
	{
		SaveSystem.SavePlayers(players);
	}

	public List<Player> Players()
	{
		return players;
	}
}
