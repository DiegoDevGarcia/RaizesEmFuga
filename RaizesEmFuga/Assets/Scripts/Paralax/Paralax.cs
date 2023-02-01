using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    public GameObject cameraPlayer;
    private float length, startPos;
    public float speedParalax;

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = (cameraPlayer.transform.position.x * (1 - speedParalax));
        float dist = (cameraPlayer.transform.position.x * speedParalax);

        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        if (temp > startPos + length / 2 )
        {
            startPos += length;
        }
        else if (temp < startPos - length / 2 )
        {
            startPos -= length;
        }
    }
}
