using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
//using UnityEditor.AnimatedValues;
using UnityEngine;

public class OrbController : MonoBehaviour
{
    public Transform RightHand;

    public GameObject playerCamera;

    [SerializeField] private float ThrowForce = 550f;
    [SerializeField] private float CallBackSpeed = 40f;

    [SerializeField] private float SecondsAnimation;
    [SerializeField] private float SecondsBeforeCallBack;
    [SerializeField] bool CastSpell = false;
    [SerializeField] bool OrbToHand = true;
    [SerializeField] bool isOrbInHand = true;


    // Update is called once per frame
    void FixedUpdate()
    {

        //if (isOrbInHand)
        //{
        //    if (CastSpell)
        //    {
                
        //        OrbToHand = false;
        //        GetComponent<Rigidbody>().useGravity = true;
        //        isOrbInHand = false;
        //        GetComponent<Rigidbody>().AddForce(playerCamera.transform.forward * ThrowForce);

        //       StartCoroutine(delayCallBack());
               
        //    }

        //}

        //if (OrbToHand)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, RightHand.position, Time.deltaTime * CallBackSpeed);
        //    GetComponent<Rigidbody>().useGravity = false;

        //    if (Vector3.Distance(transform.position, RightHand.position) < 0.05f)
        //    {
        //        CastSpell = false;
        //        isOrbInHand = true;
        //    }
        //}
    }

    void Update ()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            
            StartCoroutine(delayAnimationPlaying());
            
        }

        if (isOrbInHand)
        {
            if (CastSpell)
            {

                OrbToHand = false;
                GetComponent<Rigidbody>().useGravity = true;
                isOrbInHand = false;
                GetComponent<Rigidbody>().AddForce(playerCamera.transform.forward * ThrowForce);

                StartCoroutine(delayCallBack());

            }

        }

        if (OrbToHand)
        {
            transform.position = Vector3.MoveTowards(transform.position, RightHand.position, Time.deltaTime * CallBackSpeed);
            GetComponent<Rigidbody>().useGravity = false;

            if (Vector3.Distance(transform.position, RightHand.position) < 0.05f)
            {
                CastSpell = false;
                isOrbInHand = true;
            }
        }
    }


    IEnumerator delayAnimationPlaying()
    {
        yield return new WaitForSeconds(SecondsAnimation);
        
        CastSpell = true;

    }

    IEnumerator delayCallBack()
    {
        yield return new WaitForSeconds(SecondsBeforeCallBack);

        OrbToHand = true;
    }

}
