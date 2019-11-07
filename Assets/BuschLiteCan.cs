using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class BuschLiteCan : NetworkBehaviour
{
    private int cardsUnderTab = 0;
    public int minCardsUnderTab = 15;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void placeCard()
    {
        cardsUnderTab++;
        Debug.Log("Cards under tab: "+cardsUnderTab);
        
        if (IsCanCracked())
        {
            Debug.Log("Can cracked, bitch. Let's get a new can.");
            NewCan();
        }
    }

    public void NewCan()
    {
        cardsUnderTab = 0;
    }

    private bool IsCanCracked()
    {
        if (cardsUnderTab < minCardsUnderTab)
        {
            return false;
        }

        double chanceOfCracking = 95 * (1 - System.Math.Exp(-0.05 * (cardsUnderTab - minCardsUnderTab))) + 5;
        double rand = Random.Range(0, 100);

        // Debug.Log("Chance (" + chanceOfCracking + ") > Random (" + rand + ")?");

        // If the chance of cracking the can beats the random number (0-100), the can cracks
        return (chanceOfCracking > rand);
    }
}
