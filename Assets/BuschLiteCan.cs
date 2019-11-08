using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class BuschLiteCan : NetworkBehaviour
{
    private int cardsUnderTab = 0;
    public int minCardsUnderTab = 15;
    public GameObject _explosionPrefab;

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
            ExplosionEffect explosionEffect = _explosionPrefab.GetComponent<ExplosionEffect>();
            explosionEffect.Explode();
            //Destroy(_explosionPrefab);
            NewCan();
        }
    }

    public void NewCan()
    {
        cardsUnderTab = 0;
        Instantiate(_explosionPrefab, this.transform.position, this.transform.rotation);
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
