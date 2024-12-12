using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopUIController : MonoBehaviour
{
    public TextMeshProUGUI speedPriceText;

    public float speedPrice;
    public float attackPrice;

    public TextMeshProUGUI attackPriceText;
    // Start is called before the first frame update
    void Start()
    {
        speedPriceText.text = speedPrice.ToString();
        attackPriceText.text = attackPrice.ToString();
    }

    public void UpdateSpeedPriceCost(int cost)
    {
        speedPrice += cost;
        speedPriceText.text = speedPrice.ToString();
    }

    public void UpdateAttackPriceCost(int cost)
    {
        attackPrice += cost;
        attackPriceText.text = attackPrice.ToString();
    }
}
