using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public int coinCount;
    public Text coinText;
    void Start()
    {
        
    }

    void Update()
    {
        coinText.text = "Coins: " + coinCount.ToString();
    }
}
//onColissionTrigger trzeba dodac do gracza którego jeszcze nie ma
//większość kodu bedzie w graczu , wiec jest tu troche pusto