using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Belt : MonoBehaviour
{
	Color myColor;
	[SerializeField] int minPoints, maxPoints;
	
	private void Start() 
	{
		myColor = GetComponent<Text>().color;
	}
	
	public void ApplyBeltColor(Player player, Text text)
	{
		if(player.Rating() <= maxPoints && player.Rating() >= minPoints)
		{
			text.color = myColor;
		}
	}
}
