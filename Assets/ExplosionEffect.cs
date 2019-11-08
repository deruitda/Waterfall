using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    public GameObject _explisionPrefab;

    public void Explode()
    {
        Instantiate(_explisionPrefab, this.transform.position, this.transform.rotation);
    }
}
