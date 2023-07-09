using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseDetector : MonoBehaviour
{
    public Action DetectedObstacle;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(GameConstants.OBSTACLE_BOX))
        {
            DetectedObstacle?.Invoke();
        }
    }
}
