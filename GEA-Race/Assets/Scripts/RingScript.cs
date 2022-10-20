using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RingScript : MonoBehaviour
{
    public GameObject[] next_rings;
    public enum RingTypes { START,CP,FINISH, START_AND_FINISH };
    public RingTypes ring_type;
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
            this.GetComponent<CapsuleCollider>().enabled = true;
        }
        else
        {
            this.GetComponent<MeshRenderer>().enabled = false;
            this.GetComponent<CapsuleCollider>().enabled = false;
        }
    }

    // WHEN RING IS HIT
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");
        if (other.tag == "Player")
        {
            Debug.Log("Player");
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
