using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas : MonoBehaviour
{	
	private int health = 5;
	public Text healthText;
	public GameObject Panel;
	public bool isShow = false;
    // Start is called before the first frame update
    void Start()
    {
        Panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "HEALTH: " + health;
        if(Input.GetKeyDown(KeyCode.Space)){
        	health--;
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
        	isShow = !isShow;
        	Panel.SetActive(isShow);
        }
    }
}
