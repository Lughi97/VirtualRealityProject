using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarEffect : MonoBehaviour
{
    [SerializeField] private Animator pillarAnimator;

    void Start()
    {
      
        pillarAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.restartLevel == true) StopCoroutine(DeactivateAnimation());
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            pillarAnimator.Play("BouncyPillar", 0, 0.0f);
            StartCoroutine(DeactivateAnimation());
        }

    }

    IEnumerator DeactivateAnimation()
    {
        yield return new WaitForSeconds(3f);
        pillarAnimator.enabled = false;

    }
}
