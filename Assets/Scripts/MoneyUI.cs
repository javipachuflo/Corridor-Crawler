using UnityEngine;
using TMPro; // Required for TextMeshPro

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private string prefix = "Money: ";

    private void OnEnable()
    {
        // Subscribe to the event: "When money changes, run UpdateUI"
        if (MoneyManager.Instance != null)
        {
            MoneyManager.Instance.OnMoneyChanged += UpdateUI;
        }
    }

    private void Start()
    {
        // Failsafe: Ensure UI is correct on the very first frame
        if (MoneyManager.Instance != null)
        {
            UpdateUI(MoneyManager.Instance.GetMoney());

            // Subscribe here as well in case Start happens before OnEnable finishes setting up the Singleton
            MoneyManager.Instance.OnMoneyChanged += UpdateUI;
        }
    }

    private void OnDisable()
    {
        // Always unsubscribe to prevent memory leaks and missing reference errors
        if (MoneyManager.Instance != null)
        {
            MoneyManager.Instance.OnMoneyChanged -= UpdateUI;
        }
    }

    private void UpdateUI(int newAmount)
    {
        if (moneyText != null)
        {
            moneyText.text = prefix + newAmount.ToString();
        }
    }
}