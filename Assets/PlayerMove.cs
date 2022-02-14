using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gun;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = gun.transform.position+new Vector3(0,-1f,0);
    }
}
