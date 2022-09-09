using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCardCollision : MonoBehaviour
{
    [SerializeField] private Rigidbody rbTitle;
    [SerializeField] private List<PlayerType> skin = new List<PlayerType>();
    [SerializeField] private Material material;
    // Start is called before the first frame update
    void Start()
    {
        rbTitle = transform.gameObject.GetComponent<Rigidbody>();
        rbTitle.useGravity = false;

        //need to be changed
        switch (Random.Range(0, skin.Count))
        {
            case 0:
                drawSphere(skin[0]);
                break;
            case 1:
                drawSphere(skin[1]);
                break;
            case 2:
                drawSphere(skin[2]);
                break;
            case 3:
                drawSphere(skin[3]);
                break;
            case 4:
                drawSphere(skin[4]);
                break;
        }

        StartCoroutine(fall());
    }


    void drawSphere(PlayerType type)
    {
        transform.gameObject.GetComponent<MeshRenderer>().material =type.skin;
        transform.gameObject.GetComponent<Rigidbody>().mass = type.mass;
        transform.localScale = new Vector3(type.scale, type.scale, type.scale)*5f;
    }
    // Update is called once per frame
    void Update()
    {
        //;
    }

    public IEnumerator fall()
    {
        Debug.Log("FALL");
        Physics.gravity = new Vector3(0, -30.0F, 0);
        yield return new WaitForSeconds(2.5f);
        Debug.Log("START");
        rbTitle.useGravity = true;
    }
}
