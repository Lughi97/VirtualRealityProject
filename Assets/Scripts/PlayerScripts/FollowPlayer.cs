using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The camera follows the player
/// </summary>
public class FollowPlayer : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset;
    public float height = 25f;
    // Start is called before the first frame update
    void Start()
    {
        if (player != null)
        {
            player = GameManager.Instance.tempPlayer.transform.GetChild(0).gameObject;
            offset = transform.position - player.transform.position;
            transform.position = new Vector3(player.transform.position.x, height, player.transform.position.z);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameManager.Instance.tempPlayer;
            //player = GameManager.Instance.tempPlayer.transform.GetChild(0).gameObject;
            if (player != null)
            {
                offset = transform.position - player.transform.position;
                transform.position = new Vector3(player.transform.position.x, height, player.transform.position.z);
            }


        }
        else if (player.activeSelf == false)
        {
            transform.position = this.transform.position;
        }
        else
        {

            transform.position = new Vector3(player.transform.position.x, player.transform.position.y + height, player.transform.position.z);

        }

    }
}
