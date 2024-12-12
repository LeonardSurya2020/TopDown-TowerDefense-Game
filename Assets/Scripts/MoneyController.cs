using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class MoneyController : MonoBehaviour
{
    public int totalMoney;
    public TextMeshProUGUI text;
    public void AddMoney(int money)
    {
        this.totalMoney += money;
        text.text = "$" + totalMoney.ToString();


    }

    public void DecreaseMoney(int moneySpend)
    {
        this.totalMoney -= moneySpend;
        text.text = "$" +  totalMoney.ToString();
    }

}
