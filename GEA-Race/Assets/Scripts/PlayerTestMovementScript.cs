using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestMovementScript : MonoBehaviour
{
    public float move_speed;
    public float turn_speed;
    public float elevation_speed;
    public enum MoveType { GROUNDED, AIRBOURNE};
    public MoveType cur_move_type = MoveType.GROUNDED;

    void Start()
    {
        
    }

    void Update()
    {
        Debug.DrawLine(transform.position, transform.position + transform.forward * 20, Color.red);
        if (cur_move_type == MoveType.GROUNDED)
        {
            MoveGrounded();
        }
        else if (cur_move_type == MoveType.AIRBOURNE)
        {
            MoveAirbourne();
        }
        else
        {
            Debug.Log("MOVE TYPE ERROR");
        }
    }

    void HandleRotation()
    {
        transform.eulerAngles = transform.eulerAngles + new Vector3(0, turn_speed * Time.deltaTime * Input.GetAxis("Horizontal") * Input.GetAxis("Vertical"), 0);
    }

    void HandleForwardMovement()
    {
        transform.position = transform.position + transform.forward * move_speed * Input.GetAxis("Vertical");
    }

    void HandleUpDown()
    {
        if (Input.GetButton("Space"))
        {
            transform.position = transform.position + new Vector3(0, elevation_speed, 0);
        }
    }

    void MoveGrounded()
    {
        HandleRotation();
        HandleForwardMovement();
    }

    void MoveAirbourne()
    {
        HandleRotation();
        HandleForwardMovement();
        HandleUpDown();
    }
}
