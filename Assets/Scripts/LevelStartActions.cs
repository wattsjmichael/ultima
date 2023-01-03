using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStartActions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerController.instance.DoAtLevelStart();

        SaveManager.instance.activeSave.currScene = SceneManager.GetActiveScene().name;
        SaveManager.instance.activeSave.sceneStartPos = PlayerController.instance.transform.position;

        SaveManager.instance.SaveInfo();
    }


}
