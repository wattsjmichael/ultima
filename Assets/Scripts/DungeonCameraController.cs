using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCameraController : MonoBehaviour
{

    public static DungeonCameraController instance;
    public float camSpeed;

    public Vector3 targetPoint;

    public bool inBossRoom;
    private Vector3 limitUpper, limitLower;

   
    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        targetPoint.z = transform.position.z;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (inBossRoom)
        {
            targetPoint.y = Mathf.Clamp(PlayerController.instance.transform.position.y, limitLower.y, limitUpper.y);
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, camSpeed * Time.deltaTime);
    }

    public void ActivateBossRoom(Vector3 up, Vector3 low)
    {
        inBossRoom = true;
        limitUpper = up;
        limitLower = low;

    }
}
