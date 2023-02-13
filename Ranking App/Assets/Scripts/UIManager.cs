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
	[SerializeField] Text pOneInputFieldText;
	[SerializeField] Text pOneInfoText;
	[SerializeField] GameObject setOneButton;
	[SerializeField] GameObject[] pOneInputSet;
	[SerializeField] GameObject pTwoInputField;
	[SerializeField] Text pTwoInputFieldtext;
	[SerializeField] GameObject setTwoButton;




	private void Awake()
	{
		ShowMainCanvas();
	}

	void Update()
	{
		VerifyInputs();
		VerifyNameOne();
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
			if (item.Name() == pTwoInputFieldtext.text &&
				item.Name() != pOneInputFieldText.text)
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
		//Set Player
		Player playerOne = new Player();
		List<Player> players = FindObjectOfType<PlayerStorage>().Players();
		foreach (var item in players)
		{
			if (item.Name() == pOneInputFieldText.text)
			{
				playerOne = item;
			}
		}
		FindObjectOfType<BattleSession>().SetPlayerOne(playerOne);

		//Activate P2 input field
		pTwoInputField.SetActive(true);

		//Set P1 Text
		setOneButton.GetComponent<Text>().enabled = false;
		pOneInfoText.enabled = true;
		foreach (var item in pOneInputSet)
		{
			item.SetActive(false);
		}
		pOneInfoText.text = playerOne.Name() + "\n" + "|| " + playerOne.Rating() + " ||";
	}
}
