using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TurnUI : MonoBehaviour
{
    public Text turnText;
    public int maxTurn;
    // Start is called before the first frame update
    public void Setup(int maxTurn)
    {
        this.maxTurn = maxTurn;
    }

    // Update is called once per frame
    public void UpdateTurn(int turnNumber)
    {
        turnText.text = $"{turnNumber}/{maxTurn}";
    }
}
