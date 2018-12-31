using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    public GameObject _Archer;
    public Animator animator;
    bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    	Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D coll = _Archer.GetComponent<Collider2D>();

        //RaycastHit hit;
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(coll.OverlapPoint(mouseWorldPos)) {
            if(!isActive) {
                isActive = true;
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log(gameObject.name);
            }
        }
        else {
            isActive = false;
        }
    
        animator.SetBool("isActive", isActive);
    }
}
