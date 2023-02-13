using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] string playerName;
	[SerializeField] int rating;
	
	public string Name()
	{
		return playerName;
	}
	
	public float Rating()
	{
		return rating;
	}
	
	public void UpdateRating(int newRating)
	{
		rating = newRating;
	}
	
	public void SetName(string name)
	{
		playerName = name;
	}
	
	public void SetRating(int startingRating)
	{
		rating = startingRating;
	}
}
