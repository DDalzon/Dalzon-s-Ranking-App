using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	[Header("Canvas")]
	[SerializeField] Canvas newPlayerCanvas;
	[SerializeField] Canvas mainCanvas;
	
	[Header("New Player UI")]
	[SerializeField] GameObject addPlayerButton;
	[SerializeField] GameObject nameInputField;
	[SerializeField] GameObject ratingInputField;
	
	
	
	private void Awake() 
	{
		HideNewPlayerCanvas();	
	}
	
	void Update()
	{
		VerifyInputs();
	}
	
	public void ShowNewPlayerCanvas()
	{
		newPlayerCanvas.enabled = true;
		mainCanvas.enabled = false;
	}
	
	public void HideNewPlayerCanvas()
	{
		newPlayerCanvas.enabled = false;
		mainCanvas.enabled = true;
	}
	
	void VerifyInputs()
	{
		if(nameInputField.GetComponent<Text>().text != "" &&
			int.TryParse(ratingInputField.GetComponent<Text>().text, out int numValue))
			{
				addPlayerButton.SetActive(true);
			}
			else
			{
				addPlayerButton.SetActive(false);
			}
	}
}
