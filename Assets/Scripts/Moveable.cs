using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Moveable : MonoBehaviour
{
    public float pushSpeed = 2f;
    public float canPushDistance = 5f;
    public float rotateSpeed = 2f;
    public float canRotateDistance = 5f;
    public GameObject player;

    private bool isPushing;
    private bool isRotating;

    void Start()
    {

    }

    void Update()
    {
        if (isPushing && 
            Vector3.Distance(player.transform.position, transform.position) <= canPushDistance)
        {
            Vector3 pushDirection = player.transform.forward;
            transform.Translate(pushDirection * pushSpeed * Time.deltaTime, Space.World);
        }

        if (isRotating && 
            Vector3.Distance(player.transform.position, transform.position) <= canRotateDistance)
        {
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
        }
    }

    public void StartPushing()
    {
        isPushing = true;
    }

    public void StopPushing()
    {
        isPushing = false;
    }

    public void StartRotating()
    {
        isRotating = true;
    }

    public void StopRotating()
    {
        isRotating = false;
    }
}
