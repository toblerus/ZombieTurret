using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CashScript : MonoBehaviour
{
    public GameManager GameManager;
    public TextMeshProUGUI CashText;

    // Use this for initialization
    void Start()
    {
        CashText = GetComponent<TextMeshProUGUI>();
        GameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CashText.text = "Cash:" + GameManager.GetCash().ToString();
    }
}
