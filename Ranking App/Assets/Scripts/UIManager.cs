using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

	[Header("Canvas")]
	[SerializeField] Canvas newPlayerCanvas;
	[SerializeField] Canvas mainCanvas;
	[SerializeField] Canvas battleCanvas;


	[Header("New Player UI")]
	[SerializeField] GameObject addPlayerButton;
	[SerializeField] GameObject nameInputField;
	[SerializeField] GameObject ratingInputField;

	[Header("Battle UI")]
	[SerializeField] GameObject pOneInputField;
	[SerializeField] Text pOneInputFieldText;
	[SerializeField] Text pOneInfoText;
	[SerializeField] GameObject setOneButton;
	[SerializeField] GameObject[] pOneInputSet;
	[SerializeField] GameObject pTwoInputField;
	[SerializeField] Text pTwoInputFieldText;
	[SerializeField] Text pTwoInfoText;
	[SerializeField] GameObject setTwoButton;
	[SerializeField] GameObject[] pTwoInputSet;
	[SerializeField] GameObject[] winnerMenu;
	[SerializeField] Text winnerInputFieldText;
	[SerializeField] GameObject confirmWinButton;
	[SerializeField] Text playerOneDiff;
	[SerializeField] Text playerTwoDiff;
	[SerializeField] GameObject cancelButton;
	Player winner;

	[Header("Main UI")]
	[SerializeField] Text[] playersTexts;
	[SerializeField] GameObject addPlayerMainButton;




	private void Awake()
	{
		ShowMainCanvas();
	}

	void Update()
	{
		VerifyPlayerCount();
		VerifyInputs();
		VerifyNameOne();
		VerifyNameTwo();
		VerifyWinnerName();
		SetMainCanvasTexts();
	}

	public void ShowNewPlayerCanvas()
	{
		battleCanvas.enabled = false;
		newPlayerCanvas.enabled = true;
		mainCanvas.enabled = false;

		ClearInputTexts();
	}

	public void ShowMainCanvas()
	{
		battleCanvas.enabled = false;
		newPlayerCanvas.enabled = false;
		mainCanvas.enabled = true;
	}

	public void ShowBattleCanvas()
	{
		ResetBattleCanvas();
		battleCanvas.enabled = true;
		newPlayerCanvas.enabled = false;
		mainCanvas.enabled = false;
	}


	// Add Player Code:
	void ClearInputTexts()
	{
		nameInputField.GetComponentInParent<InputField>().SetTextWithoutNotify("");
		ratingInputField.GetComponentInParent<InputField>().SetTextWithoutNotify("");
	}
	
	void VerifyPlayerCount()
	{
		Player[] players = FindObjectsOfType<Player>();
		if(players.Length < 10)
		{
			addPlayerMainButton.SetActive(true);
		}else
		{
			addPlayerMainButton.SetActive(false);
		}
	}

	void VerifyInputs()
	{
		bool PlayerWithSameName()
		{
			List<Player> players = FindObjectOfType<PlayerStorage>().Players();
			foreach (var item in players)
			{
				if (item.Name() == nameInputField.GetComponent<Text>().text)
				{
					return true;
				}
			}
			return false;
		}

		if (nameInputField.GetComponent<Text>().text != "" &&
			int.TryParse(ratingInputField.GetComponent<Text>().text, out int numValue) &&
				!PlayerWithSameName())
		{
			addPlayerButton.SetActive(true);
		}
		else
		{
			addPlayerButton.SetActive(false);
		}
	}



	//Battle Session Code:
	void VerifyNameOne()
	{
		List<Player> players = FindObjectOfType<PlayerStorage>().Players();
		foreach (var item in players)
		{
			if (item.Name() == pOneInputFieldText.text)
			{
				setOneButton.SetActive(true);
				break;
			}
			else
			{
				setOneButton.SetActive(false);
			}
		}
	}

	void VerifyNameTwo()
	{
		List<Player> players = FindObjectOfType<PlayerStorage>().Players();
		foreach (var item in players)
		{
			if (item.Name() == pTwoInputFieldText.text &&
			item.Name() != FindObjectOfType<BattleSession>().PlayerOne().Name())
			{
				setTwoButton.SetActive(true);
				break;
			}
			else
			{
				setTwoButton.SetActive(false);
			}
		}
	}

	public void SetPlayerOne()
	{
		setOneButton.GetComponent<Text>().enabled = false;
		//Set Player
		Player playerOne = new Player();
		List<Player> players = FindObjectOfType<PlayerStorage>().Players();
		foreach (var item in players)
		{
			if (item.Name() == pOneInputFieldText.text)
			{
				playerOne = item;
				FindObjectOfType<BattleSession>().SetPlayerOne(playerOne);
			}
		}
		

		//Activate P2 input field
		pTwoInputField.SetActive(true);

		//Set P1 Text

		pOneInfoText.enabled = true;
		foreach (var item in pOneInputSet)
		{
			item.SetActive(false);
		}
		pOneInfoText.text = playerOne.Name() + "\n|| " + playerOne.Rating() + " ||";
		SetTextColor(playerOne, pOneInfoText);
	}

	public void SetPlayerTwo()
	{
		setTwoButton.GetComponent<Text>().enabled = false;
		Player playerTwo = new Player();
		List<Player> players = FindObjectOfType<PlayerStorage>().Players();
		foreach (var item in players)
		{
			if (item.Name() == pTwoInputFieldText.text)
			{
				playerTwo = item;
			}
		}
		FindObjectOfType<BattleSession>().SetPlayerTwo(playerTwo);

		setTwoButton.SetActive(false);
		pTwoInfoText.enabled = true;
		pTwoInfoText.text = playerTwo.Name() + "\n|| " + playerTwo.Rating() + " ||";
		SetTextColor(playerTwo, pTwoInfoText);
		foreach (var item in pTwoInputSet)
		{
			item.SetActive(false);
		}

		EnableWinnerMenu();
	}

	void VerifyWinnerName()
	{
		Player[] currentPlayers = FindObjectOfType<BattleSession>().CurrentPlayers();
		if (currentPlayers[0] != null && currentPlayers[1] != null)
		{
			foreach (var item in currentPlayers)
			{
				if (winnerInputFieldText.text == item.Name())
				{
					confirmWinButton.SetActive(true);
					winner = item;
					break;
				}
				else
				{
					confirmWinButton.SetActive(false);
					winner = null; //not really needed but, just in case...
				}
			}
		}
	}

	public void CalculateVictoryPoints()
	{
		Player[] currentPlayers = FindObjectOfType<BattleSession>().CurrentPlayers();
		if (winnerInputFieldText.text == currentPlayers[0].Name())
		{
			FindObjectOfType<BattleSession>().PlayerOneWins();
		}
		else if (winnerInputFieldText.text == currentPlayers[1].Name())
		{
			FindObjectOfType<BattleSession>().PlayerTwoWins();
		}

		ProcessCalculation();
	}

	public void CalculateDrawPoints()
	{
		FindObjectOfType<BattleSession>().Draw();
		ProcessCalculation();
	}

	void UpdatePlayerTextData()
	{
		Player playerOne = FindObjectOfType<BattleSession>().PlayerOne();
		pOneInfoText.text = playerOne.Name() + "\n|| " + playerOne.Rating() + " ||";
		SetTextColor(playerOne, pOneInfoText);

		Player playerTwo = FindObjectOfType<BattleSession>().PlayerTwo();
		pTwoInfoText.text = playerTwo.Name() + "\n|| " + playerTwo.Rating() + " ||";
		SetTextColor(playerTwo, pTwoInfoText);
	}

	void DisableWinnerMenu()
	{
		foreach (var item in winnerMenu)
		{
			item.SetActive(false);
		}
	}

	void EnableWinnerMenu()
	{
		foreach (var item in winnerMenu)
		{
			item.SetActive(true);
		}
	}

	void SetPlayerDiffs()
	{
		BattleSession battleSession = FindObjectOfType<BattleSession>();

		if (battleSession.PlayerOneDiff() >= 0)
		{
			playerOneDiff.text = "+" + battleSession.PlayerOneDiff();
		}
		else
		{
			playerOneDiff.text = battleSession.PlayerOneDiff().ToString();
		}

		if (battleSession.PlayerTwoDiff() >= 0)
		{
			playerTwoDiff.text = "+" + battleSession.PlayerTwoDiff();
		}
		else
		{
			playerTwoDiff.text = battleSession.PlayerTwoDiff().ToString();
		}
	}

	IEnumerator RestartWinnerMenu()
	{
		yield return new WaitForSeconds(1.5f);
		EnableWinnerMenu();
		cancelButton.SetActive(true);
		playerOneDiff.enabled = false;
		playerTwoDiff.enabled = false;
		confirmWinButton.GetComponent<Button>().enabled = true;
		confirmWinButton.GetComponent<Image>().enabled = true;
	}

	void ProcessCalculation()
	{
		confirmWinButton.GetComponent<Button>().enabled = false;
		confirmWinButton.GetComponent<Image>().enabled = false;
		UpdatePlayerTextData();
		DisableWinnerMenu();
		cancelButton.SetActive(false);
		playerOneDiff.enabled = true;
		playerTwoDiff.enabled = true;
		SetPlayerDiffs();
		StartCoroutine(RestartWinnerMenu());
	}

	void ResetBattleCanvas()
	{
		pOneInputField.GetComponent<InputField>().SetTextWithoutNotify("");
		pTwoInputField.GetComponent<InputField>().SetTextWithoutNotify("");
		pOneInfoText.enabled = false;
		pTwoInfoText.enabled = false;
		setOneButton.GetComponent<Text>().enabled = true;
		setTwoButton.GetComponent<Text>().enabled = true;
		foreach (var item in pOneInputSet)
		{
			item.SetActive(true);
		}
		pTwoInputSet[0].SetActive(true);
		winnerMenu[1].GetComponent<InputField>().SetTextWithoutNotify("");
		DisableWinnerMenu();
	}

	void SetTextColor(Player player, Text theText)
	{
		Belt[] belts = FindObjectsOfType<Belt>();
		foreach (var item in belts)
		{
			item.ApplyBeltColor(player, theText);
		}
	}



	//MainCanvas UI Code:
	public void SetMainCanvasTexts()
	{
		Player[] players = FindObjectsOfType<Player>();
		if (players != null)
		{
			foreach (Player player in players)
			{
				foreach (Text text in playersTexts)
				{
					if (!player.showingOnMain && text.text == "")
					{
						text.text = player.Name() + "\n|| " + player.Rating() + " ||";
						SetTextColor(player, text);
						player.showingOnMain = true;
						break;
					}
				}
			}
		}
	}
	
	//Tens que criar uma variavel na classe Player pra guardar o texto determinado acima
	//e depois criar uma função (aqui ou la) pra atualizar as cores e os textos.
}
