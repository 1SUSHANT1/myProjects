using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    float length;
    float StartPos;
    [SerializeField] GameObject cam;
    [SerializeField] float parallaxeffect;
    // Start is called before the first frame update
    void Start()
    {
        StartPos = transform.position.x;
        length = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = cam.transform.position.x * parallaxeffect;
        transform.position = new Vector3(StartPos+distance,transform.position.y,transform.position.z);
        if(distance>StartPos+length)
        {
            StartPos += length;
        }
        else  if (distance < StartPos -length)
        {
            StartPos -= length;

        }
    }
}
