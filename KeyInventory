using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_KeyInventory : MonoBehaviour
{
    public Text keyText;
    int keys = 0;

    void Start()
    {
        keyText.text = "Keys: " + keys.ToString();
    }

    void Update()
    {
        AddKeyToInventory();
    }

    public void AddKey()
    {
        keys += 1;
    }

    public void AddKeyToInventory()
    {
        keyText.text = "Keys: " + keys.ToString();
    }

}
