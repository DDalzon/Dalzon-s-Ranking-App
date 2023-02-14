using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSession : MonoBehaviour
{
	[SerializeField] Player playerOne, playerTwo;
	float pOneExpected, pTwoExpected;
	int pOneDiff, pTwoDiff;
	
	
	public void SetPlayerOne(Player player)
	{
		playerOne = player;
	}
	
	public void SetPlayerTwo(Player player)
	{
		playerTwo = player;
	}
	
	public Player PlayerOne()
	{
		return playerOne;
	}
	
	public Player PlayerTwo()
	{
		return playerTwo;
	}
	
	public Player[] CurrentPlayers()
	{
		Player[] currentPlayers = new Player[2];
		currentPlayers[0] = playerOne;
		currentPlayers[1] = playerTwo;
		return currentPlayers;
	}
	
	
	void CalculateExpectedValues()
	{
		pOneExpected = 1/
			(1 + (Mathf.Pow(10, (playerTwo.Rating() - playerOne.Rating())/400)));
		
		pTwoExpected = 1/
			(1 + (Mathf.Pow(10, (playerOne.Rating() - playerTwo.Rating())/400)));
	}

	
	public void PlayerOneWins()
	{
		CalculateExpectedValues();
		
		int oldPoneRating = (int)Mathf.Round(playerOne.Rating());
		float newPOneRating = playerOne.Rating() + 32*(1 - pOneExpected);
		playerOne.UpdateRating((int)Mathf.Round(newPOneRating));
		pOneDiff = (int)Mathf.Round(newPOneRating - oldPoneRating);
		
		int oldPTwoRating = (int)Mathf.Round(playerTwo.Rating());
		float newPTwoRating = playerTwo.Rating() + 32*(0 - pTwoExpected);
		playerTwo.UpdateRating((int)Mathf.Round(newPTwoRating));
		pTwoDiff = (int)Mathf.Round(newPTwoRating - oldPTwoRating);
	}
	
	public void PlayerTwoWins()
	{
		CalculateExpectedValues();
		
		int oldPoneRating = (int)Mathf.Round(playerOne.Rating());
		float newPOneRating = playerOne.Rating() + 32*(0 - pOneExpected);
		playerOne.UpdateRating((int)Mathf.Round(newPOneRating));
		pOneDiff = (int)Mathf.Round(newPOneRating - oldPoneRating);
		
		int oldPTwoRating = (int)Mathf.Round(playerTwo.Rating());
		float newPTwoRating = playerTwo.Rating() + 32*(1 - pTwoExpected);
		playerTwo.UpdateRating((int)Mathf.Round(newPTwoRating));
		pTwoDiff = (int)Mathf.Round(newPTwoRating - oldPTwoRating);
	}
	
	public void Draw()
	{
		CalculateExpectedValues();
		
		int oldPoneRating = (int)Mathf.Round(playerOne.Rating());
		float newPOneRating = playerOne.Rating() + 32*(0.5f - pOneExpected);
		playerOne.UpdateRating((int)Mathf.Round(newPOneRating));
		pOneDiff = (int)Mathf.Round(newPOneRating - oldPoneRating);
		
		int oldPTwoRating = (int)Mathf.Round(playerTwo.Rating());
		float newPTwoRating = playerTwo.Rating() + 32*(0.5f - pTwoExpected);
		playerTwo.UpdateRating((int)Mathf.Round(newPTwoRating));
		pTwoDiff = (int)Mathf.Round(newPTwoRating - oldPTwoRating);
	}
	
	public int PlayerOneDiff()
	{
		return pOneDiff;
	}
	
	public int PlayerTwoDiff()
	{
		return pTwoDiff;
	}
}
