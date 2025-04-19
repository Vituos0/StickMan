using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class PlayerCollectibles : MonoBehaviour
{
    public Text textComponent;
    public int Money;

    // Start is called before the first frame update
    void Start()
    {

        Money = PlayerPrefs.GetInt("Money", 0);
        textComponent = GameObject.FindGameObjectWithTag("MoneyUI").GetComponentInChildren<Text>();
        UpdateText();

    }

    private void UpdateText()
    {
        textComponent.text = Money.ToString();
    }

    public void MoneyCollecting()
    {
        Money=Money + 20;
        UpdateText();
    }
    
}
