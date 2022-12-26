using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  public static CameraController instance;

    private Camera theCam;
  private Transform target;

  public BoxCollider2D areaBox;

  private float halfWidth, halfHeight; 

  private void Awake()
  {
      instance = this;
  }
    // Start is called before the first frame update
    void Start()
    {
        target = PlayerController.instance.transform;
        theCam = GetComponent<Camera>();

        halfHeight = theCam.orthographicSize;
        halfWidth = theCam.orthographicSize * theCam.aspect;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, areaBox.bounds.min.x + halfWidth, areaBox.bounds.max.x - halfWidth),
        Mathf.Clamp(transform.position.y, areaBox.bounds.min.y + halfHeight, areaBox.bounds.max.y - halfHeight),
        transform.position.z);
    }
}
