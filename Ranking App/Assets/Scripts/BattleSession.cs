using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSession : MonoBehaviour
{
	[SerializeField] Player playerOne, playerTwo;
	float pOneExpected, pTwoExpected;
	
	
	
	private void Update() 
	{		
		if(Input.GetKeyDown(KeyCode.A))
		{
			PlayerOneWins();
		}
	}
	
	
	void CalculateExpectedValues()
	{
		pOneExpected = 1/
			(1 + (Mathf.Pow(10, (playerTwo.Rating() - playerOne.Rating())/400)));
		
		pTwoExpected = 1/
			(1 + (Mathf.Pow(10, (playerOne.Rating() - playerTwo.Rating())/400)));
	}

	
	void PlayerOneWins()
	{
		CalculateExpectedValues();
		
		float newPOneRating = playerOne.Rating() + 32*(1 - pOneExpected);
		playerOne.UpdateRating((int)Mathf.Round(newPOneRating));
		
		float newPTwoRating = playerTwo.Rating() + 32*(0 - pTwoExpected);
		playerTwo.UpdateRating((int)Mathf.Round(newPTwoRating));
	}
	
	void PlayerTwoWins()
	{
		CalculateExpectedValues();
		
		float newPOneRating = playerOne.Rating() + 32*(0 - pOneExpected);
		playerOne.UpdateRating((int)Mathf.Round(newPOneRating));
		
		float newPTwoRating = playerTwo.Rating() + 32*(1 - pTwoExpected);
		playerTwo.UpdateRating((int)Mathf.Round(newPTwoRating));
	}
	
	void Draw()
	{
		CalculateExpectedValues();
		
		float newPOneRating = playerOne.Rating() + 32*(0.5f - pOneExpected);
		playerOne.UpdateRating((int)Mathf.Round(newPOneRating));
		
		float newPTwoRating = playerTwo.Rating() + 32*(0.5f - pTwoExpected);
		playerTwo.UpdateRating((int)Mathf.Round(newPTwoRating));
	}
}
