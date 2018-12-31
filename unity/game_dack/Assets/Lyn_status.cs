using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lyn_status : MonoBehaviour
{
   	public GameObject Lyn;
    public Animator animator;
    bool Lyn_active = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    	Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D coll = Lyn.GetComponent<Collider2D>();

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(coll.OverlapPoint(mouseWorldPos)) {
            if(!Lyn_active) {
                Lyn_active = true;
            }
        }
        else {
            Lyn_active = false;
        }
        if (Input.GetMouseButtonDown(0))
        {	
        
        	if(coll.OverlapPoint(mouseWorldPos)) {
	            Debug.Log("123");
        	}
        	
        }
        animator.SetBool("change", Lyn_active);
    }
}
