using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchRaycast : MonoBehaviour
{
    private GameObject target;
    public GameObject tel;
    public GameObject p;
    public Text TText;
    void Update()
    {
        target = GetClikedObject();
        if (Vector3.Distance(p.transform.position, transform.position) <= 5f)
        {
            TText.gameObject.SetActive(true);
        }
        else
        {
            TText.gameObject.SetActive(false);
        }
        if (Input.GetMouseButtonDown(0))
        {

            if (target.Equals(gameObject) && Vector3.Distance(p.transform.position, transform.position) <= 5f)
            {
                
                
                p.transform.position = tel.transform.position;
                

            }
        }
    }
    private GameObject GetClikedObject()
    {
        RaycastHit hit;
        GameObject target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (true == (Physics.Raycast(ray.origin, ray.direction * 10, out hit)))
        {
            target = hit.collider.gameObject;
        }
        return target;
    }
}
