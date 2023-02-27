using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] string playerName;
    [SerializeField] int rating;
    Text mainUIText;
    public bool showingOnMain;


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

    public string Plate()
    {
        return playerName + "\n|| " + rating + " ||";
    }

    public void SetMyText(Text text)
    {
        mainUIText = text;
    }

    public void UpdateMainUIText()
    {
        if (mainUIText != null)
        {
            mainUIText.text = playerName + "\n|| " + rating + " ||";
            FindObjectOfType<UIManager>().SetTextColor(this, mainUIText);
        }
    }
}
