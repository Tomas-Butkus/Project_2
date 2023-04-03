using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera cam;
    public Transform target;
    [Header("Manually set camera offset")]
    public Vector3 offsetPos;
    [Header("Camera variables")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float zoomMin;
    [SerializeField] private float zoomMax;
    //[Header("Camera follow bounds")]
    //[SerializeField] private float xMin;
    //[SerializeField] private float xMax;
    //[SerializeField] private float zMin;
    //[SerializeField] private float zMax;


    Vector3 targetPos;


    private void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        MoveWithTarget();
        Zoom();
    }

    void MoveWithTarget()
    {
        targetPos = target.position + offsetPos;
        cam.transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
        //cam.transform.position = new Vector3(Mathf.Clamp(target.position.x, xMin, xMax), (Mathf.Clamp(target.position.y, zMin, zMax)), offsetPos.y);
    }

    void Zoom()

    {

        // Local variable to temporarily store our camera's position

        Vector3 camPos = cam.transform.position;

        // Local variable to store the distance of the camera from the camera_target

        float distance = Vector3.Distance(transform.position, cam.transform.position);


        // When we scroll our mouse wheel up, zoom in if the camera is not within the minimum distance (set by our zoomMin variable)

        if (Input.GetAxis("Mouse ScrollWheel") > 0f && transform.position.y > zoomMin)

        {
            camPos += cam.transform.forward * zoomSpeed * Time.deltaTime;
            offsetPos.y = camPos.y;
        }


        // When we scroll our mouse wheel down, zoom out if the camera is not outside of the maximum distance (set by our zoomMax variable)

        if (Input.GetAxis("Mouse ScrollWheel") < 0f && transform.position.y < zoomMax)

        {
            camPos -= cam.transform.forward * zoomSpeed * Time.deltaTime;
            offsetPos.y = camPos.y;
        }


        // Set the camera's position to the position of the temporary variable
        camPos.y = Mathf.Clamp(camPos.y, zoomMin, zoomMax);
        cam.transform.position = camPos;

    }
}
