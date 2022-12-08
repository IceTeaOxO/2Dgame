using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeThrow : MonoBehaviour
{
    public GameObject axePrefab;

    public GameObject character;

    Vector2 direction;

    public float power;

    void Start()
    {

    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            Throw(character);
        }    
    }

    void Throw(GameObject character)
    {
        direction = character.GetComponent<PlayerController>().GetFacingDirection().normalized;
        GameObject axe = MonoBehaviour.Instantiate(axePrefab);
        axe.transform.position = character.transform.position;
        axe.GetComponent<Rigidbody2D>().isKinematic = false;
        axe.GetComponent<Rigidbody2D>().AddForce(direction * power);
        axe.GetComponent<Axe>().canRotate = true;
    }
}
