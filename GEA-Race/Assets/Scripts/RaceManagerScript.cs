using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RaceManagerScript : MonoBehaviour
{
    public TextMeshProUGUI lap_count;
    public GameObject[] CPMarkerSprites;
    private GameObject[] all_rings;
    private GameObject[] active_rings;
    private Camera cam_ref;
    public int max_laps;
    public int current_lap = 0;
    void Start()
    {
        // STORE ALL RINGS AND ENABLE STARTING LINE
        all_rings = GameObject.FindGameObjectsWithTag("Ring");
        cam_ref = Camera.main;
        foreach (GameObject ring in all_rings)
        {
            if (ring.GetComponent<RingScript>().ring_type == RingScript.RingTypes.START || ring.GetComponent<RingScript>().ring_type == RingScript.RingTypes.START_AND_FINISH)
            {
                ring.GetComponent<RingScript>().cur_enabled = true;
                if (active_rings != null)
                {
                    // IF THERE ARE CURRENT ACTIVE RINGS
                    // ADD THEM ONTO THE ARRAY
                    GameObject[] temp_rings = new GameObject[active_rings.Length+1];
                    active_rings.CopyTo(temp_rings, 0);
                    temp_rings[active_rings.Length] = ring;
                    active_rings = temp_rings;
                }
                else
                {
                    // IF THERE ARE NOT ACTIVE RINGS
                    // CREATE SINGLE SIZE ARRAY OF NEW RING
                    GameObject[] temp_rings = new GameObject[1];
                    temp_rings[0] = ring;
                    active_rings = temp_rings;
                }
            }
            else
            {
                ring.GetComponent<RingScript>().cur_enabled = false;
            }
        }
    }

    void Update()
    {
        // UPDATE UI
        lap_count.text = "LAP: ["+current_lap+"/"+max_laps+"]";
        for (int i = 0; i < CPMarkerSprites.Length; i++)
        {
            if (active_rings != null && active_rings.Length > i)
            {
                Vector3 temp_vec = active_rings[i].transform.position;
                Vector3 pos_on_cam = cam_ref.WorldToScreenPoint(temp_vec);
                CPMarkerSprites[i].GetComponent<RectTransform>().position = pos_on_cam;
                CPMarkerSprites[i].GetComponent<Image>().enabled = true;
            }
            else
            {
                CPMarkerSprites[i].GetComponent<Image>().enabled = false;
            }
        }
    }

    // HIDE OR SHOW RINGS
    public void UpdateRings(GameObject[] n_rings)
    {
        foreach (GameObject ring in all_rings)
        {
            ring.GetComponent<RingScript>().cur_enabled = false;
        }
        active_rings = null;
        foreach (GameObject ring in n_rings)
        {
            ring.GetComponent<RingScript>().cur_enabled = true;
            if (active_rings != null)
            {
                // IF THERE ARE CURRENT ACTIVE RINGS
                // ADD THEM ONTO THE ARRAY
                GameObject[] temp_rings = new GameObject[active_rings.Length + 1];
                active_rings.CopyTo(temp_rings, 0);
                temp_rings[active_rings.Length] = ring;
                active_rings = temp_rings;
            }
            else
            {
                // SHOULD NOT BE POSSIBLE, BUT JUST IN CASE
                // IF THERE ARE NOT ACTIVE RINGS
                // CREATE SINGLE SIZE ARRAY OF NEW RING
                GameObject[] temp_rings = new GameObject[1];
                temp_rings[0] = ring;
                active_rings = temp_rings;
            }
        }
    }

    // IF START OR FINISH RING HIT
    public void LapOrFinish(GameObject[] n_rings)
    {
        if (current_lap >= max_laps)
        {
            // FINISH RACE
            Debug.Log("FINISH");
        }
        else
        {
            // NEXT LAP
            UpdateRings(n_rings);
            current_lap++;
        }
    }
}
