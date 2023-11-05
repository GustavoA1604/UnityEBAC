using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHelper : MonoBehaviour
{
    public List<Transform> positions;
    public float timeBetweenPositions = 1f;
    private int _curPositionIndex = 0;

    void Start()
    {
        transform.position = positions[0].transform.position;
        _curPositionIndex = (_curPositionIndex + 1) % positions.Count;
        StartCoroutine(StartMovement());
    }

    IEnumerator StartMovement()
    {
        float time = 0;

        while (true)
        {
            var currentPosition = transform.position;

            while (time < timeBetweenPositions)
            {
                transform.position = Vector3.Lerp(currentPosition, positions[_curPositionIndex].transform.position, time / timeBetweenPositions);
                time += Time.deltaTime;
                yield return null;
            }

            _curPositionIndex = (_curPositionIndex + 1) % positions.Count;
            time = 0;

            yield return null;
        }
    }
}
