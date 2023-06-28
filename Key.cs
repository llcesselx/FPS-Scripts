using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Key : MonoBehaviour
{
    public scr_KeyInventory keyInventory;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            keyInventory.AddKey();
        }
    }
}
