using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarEffect : MonoBehaviour
{
   [SerializeField] private Animator pillarAnimator;
    [SerializeField] private string effect = "BouncyPillar";
    // Start is called before the first frame update
    void Start()
    {
        pillarAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            pillarAnimator.Play("BouncyPillar", 0, 0.0f);
        }

    }
}
