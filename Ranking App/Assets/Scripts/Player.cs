using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] string playerName;
	[SerializeField] int rating;
	
	public float Rating()
	{
		return rating;
	}
	
	public void UpdateRating(int newRating)
	{
		rating = newRating;
	}
}
