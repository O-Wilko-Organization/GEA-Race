using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RingScript : MonoBehaviour
{
    public GameObject[] next_rings;
    public enum RingTypes { START,CP,FINISH, START_AND_FINISH };
    public RingTypes ring_type;
    public enum MoveType { GROUNDED, AIRBOURNE};
    public MoveType move_type;
    public bool cur_enabled;

    void Start()
    {
        
    }

    // SHOW PATH IN EDITOR
    void Update()
    {
        if (next_rings != null)
        {
            foreach (GameObject ring in next_rings)
            {
                Debug.DrawLine(this.transform.position, ring.transform.position, Color.red);
            }
        }

        if (cur_enabled)
        {
            this.GetComponent<MeshRenderer>().enabled = true;
            this.GetComponent<MeshCollider>().enabled = true;
        }
        else
        {
            this.GetComponent<MeshRenderer>().enabled = false;
            this.GetComponent<MeshCollider>().enabled = false;
        }
    }

    // WHEN RING IS HIT
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerTestMovementScript>().last_cp_location = transform.position;
            other.GetComponent<PlayerTestMovementScript>().last_cp_rotation = transform.eulerAngles.y - 90;
            if (move_type == MoveType.GROUNDED)
            {
                other.GetComponent<PlayerTestMovementScript>().cur_move_type = PlayerTestMovementScript.MoveType.GROUNDED;
            }
            else if (move_type == MoveType.AIRBOURNE)
            {
                other.GetComponent<PlayerTestMovementScript>().cur_move_type = PlayerTestMovementScript.MoveType.AIRBOURNE;
            }
            else
            {

                //MOVE TYPE ERROR
            }
            RaceManagerScript manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<RaceManagerScript>();
            if (ring_type == RingTypes.FINISH || ring_type == RingTypes.START_AND_FINISH || ring_type == RingTypes.START)
            {
                // CHECK LAP FUNCTION
                manager.LapOrFinish(next_rings);
            }
            else
            {
                // UPDATE RINGS
                manager.UpdateRings(next_rings);
            }
        }
    }
}
