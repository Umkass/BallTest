using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] ParticleSystem psCoinPrefab;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            UIManager.Instance.AddCoin();
            Instantiate(psCoinPrefab, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
