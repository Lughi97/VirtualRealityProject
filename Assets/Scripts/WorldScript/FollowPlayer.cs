using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset;
    private static float height=30f;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.position = new Vector3(player.transform.position.x,height,player.transform.position.z);
      
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        transform.position = player.transform.position + offset;
    }
}
