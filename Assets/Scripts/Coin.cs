using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int serialID;
    public GameObject pickupEffect;
    public int amount = 1;

    private void Start()
    {
        gameObject.SetActive(DataManager.Instance.GetLevelItemActive(serialID));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup();
        }
    }

    void Pickup()
    {
        Instantiate(pickupEffect, transform.position, transform.rotation);
        int hp = DataManager.Instance.playerData.hp;
        DataManager.Instance.playerData.SetHP(hp + amount);
        DataManager.Instance.SetLevelItemStatus(serialID, false);
        Destroy(gameObject);
    }
}
