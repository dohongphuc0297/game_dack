using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bael_Script : MonoBehaviour
{
    public GameObject _Bael;
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
        Collider2D coll = _Bael.GetComponent<Collider2D>();

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (coll.OverlapPoint(mouseWorldPos))
        {
            if (!isActive)
            {
                isActive = true;
            }
        }
        else
        {
            isActive = false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (coll.OverlapPoint(mouseWorldPos))
            {
                Debug.Log("123");
            }

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.gameObject.name);
            }
        }
        animator.SetBool("isActive", isActive);
    }
}
