using System;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    // The Singleton instance allows other scripts to access this easily
    public static MoneyManager Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] private int startingMoney = 0;

    // The actual money value. Private so it can't be accidentally altered.
    private int currentMoney;

    // The Event that UI and other systems will listen to
    public event Action<int> OnMoneyChanged;

    private void Awake()
    {
        // Standard Singleton setup
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Optional: Keep this object alive when loading new scenes
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Initialize the money and notify any listeners
        currentMoney = startingMoney;
        OnMoneyChanged?.Invoke(currentMoney);
    }

    /// <summary>
    /// Returns the current amount of money.
    /// </summary>
    public int GetMoney()
    {
        return currentMoney;
    }

    /// <summary>
    /// Adds money and updates the UI automatically.
    /// </summary>
    public void AddMoney(int amount)
    {
        if (amount <= 0) return; // Prevent adding negative numbers

        currentMoney += amount;
        OnMoneyChanged?.Invoke(currentMoney); // Announce the change
    }

    /// <summary>
    /// Checks if the player has enough money without spending it.
    /// </summary>
    public bool HasEnoughMoney(int amount)
    {
        return currentMoney >= amount;
    }

    /// <summary>
    /// Attempts to spend money. Returns true if successful.
    /// </summary>
    public bool SpendMoney(int amount)
    {
        if (amount <= 0) return false;

        if (HasEnoughMoney(amount))
        {
            currentMoney -= amount;
            OnMoneyChanged?.Invoke(currentMoney); // Announce the change
            return true;
        }

        return false; // Not enough money
    }
}