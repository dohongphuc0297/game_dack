using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lyn_sta: MonoBehaviour
{
	public GameObject Lyn = null;
    private Animator _activeLyn = null;
    bool Lyn_active = false;

    // Start is called before the first frame update
    void Start()
    {
        _activeLyn = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    	Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D coll = Lyn.GetComponent<Collider2D>();

        if(coll.OverlapPoint(mouseWorldPos)) {
            if(!Lyn_active) {
                Lyn_active = true;
            }
        }
        else {
            Lyn_active = false;
        }
        _activeLyn.SetBool("active", Lyn_active);
    }
}
