using System.Runtime.InteropServices;
using UnityEngine;

public class DiamondInteractable : MonoBehaviour, IInteractable
{

    [SerializeField] private string promptMessage = "Pick up Diamond";

    [SerializeField] private int moneyToAdd = 100;

    [SerializeField] private AudioSource diamondPickUpSoundEffect;

    public void Interact()
    {
        Debug.Log("Interacted with " + promptMessage);
        diamondPickUpSoundEffect.Play();
        MoneyManager.Instance.AddMoney(moneyToAdd);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject, diamondPickUpSoundEffect.clip.length);
    }
    public string GetPromptText()
    {
        return promptMessage;
    }

    public Transform GetTransform()
    {
        // Returning this object's transform allows the UI to track it
        return transform;
    }
}
