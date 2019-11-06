using System.Collections;
using UnityEngine;

public class BuschLiteCan : MonoBehaviour
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
        
        if (canCracked())
        {
            Debug.Log("Can Cracked Bitch");
        }
    }

    private bool canCracked()
    {
        if (cardsUnderTab < minCardsUnderTab)
        {
            return false;
        }

        double chanceOfCracking = 95 * (1 - System.Math.Exp(-0.05 * (cardsUnderTab - minCardsUnderTab))) + 5;
        double rand = Random.Range(0, 100);

        Debug.Log("Chance (" + chanceOfCracking + ") > Random (" + rand + ")?");

        // If the chance of cracking the can beats the random number (0-100), the can cracks
        return (chanceOfCracking > rand);
    }
}
