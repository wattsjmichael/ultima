using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartActions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerController.instance.DoAtLevelStart();
    }


}
