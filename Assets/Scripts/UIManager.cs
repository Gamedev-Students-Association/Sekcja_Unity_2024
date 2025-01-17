using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    
    public void UpdateCoinDisplay(int coins)
    {
        coinText.text = $"Coins: {coins}";
    }
}