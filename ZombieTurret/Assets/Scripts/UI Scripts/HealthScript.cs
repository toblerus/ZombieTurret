using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthScript : MonoBehaviour {
    public GameManager GameManager;
    public TextMeshProUGUI HealthText;

	// Use this for initialization
	void Start () {
        GameManager = FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        HealthText.text = "Health:" + GameManager.GetHealth().ToString();
	}
}
