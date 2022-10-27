using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTestMovementScript : MonoBehaviour
{
    public float move_speed;
    public float turn_speed;
    public float elevation_speed;

    public Vector3 last_cp_location;
    public float last_cp_rotation;

    private float startup_timer = 0;
    public float max_startup_timer;

    public bool finished_race = false;
    public enum MoveType { GROUNDED, AIRBOURNE};
    public MoveType cur_move_type = MoveType.GROUNDED;

    void Start()
    {
        last_cp_location = transform.position;
        last_cp_rotation = transform.eulerAngles.y;
    }

    void Update()
    {
        if (max_startup_timer >= startup_timer)
        {
            startup_timer += Time.deltaTime;
        }
        else
        {
            if (!finished_race)
            {
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
            else
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SceneManager.LoadScene(0, LoadSceneMode.Single);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DeathBox")
        {
            Respawn();
        }
    }
    void Respawn()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.eulerAngles = new Vector3(0, last_cp_rotation, 0);
        transform.position = last_cp_location;
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
        if (Input.GetKey(KeyCode.Space))
        {
            transform.position = transform.position + new Vector3(0, elevation_speed * Time.deltaTime, 0);
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
