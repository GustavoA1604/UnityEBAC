using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    public Vector2 pastPosition;
    public float speed = .3f;

    void Start()
    {
        pastPosition = Input.mousePosition;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MoveX(Input.mousePosition.x - pastPosition.x);
        }

        pastPosition = Input.mousePosition;
    }

    public void MoveX(float xDiff)
    {
        transform.position += Vector3.right * Time.deltaTime * speed * xDiff;
    }
}
