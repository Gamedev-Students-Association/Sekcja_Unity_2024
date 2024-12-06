using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CoinManager : MonoBehaviour
{
    public int coinCount;
    public Text coinText;
    void Start()
    {
        
    }

    void Update()
    {
        coinText.text = coinCount.ToString();
    }
}
