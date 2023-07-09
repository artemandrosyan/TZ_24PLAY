using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBlock : MonoBehaviour, IPoolElement
{
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

}
