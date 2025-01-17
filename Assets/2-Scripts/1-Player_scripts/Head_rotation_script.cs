using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HajyGames;

public class Head_rotation_script : MonoBehaviour
{
    private Transform crosshair;
    private Dima_script dimaScript;
    private Vector3 latestRot; 
    private float latestSpriteRot = 1;
    
    void Start()
    {
        crosshair = GameObject.Find("Crosshair").GetComponent<Transform>();
        dimaScript = GameObject.Find("Dima").GetComponent<Dima_script>();
    }

    
    void Update()
    {
        if (dimaScript.health > 0) {
            LookAtTarget();
        }
    }

    void LookAtTarget() { // rotate head to look at crosshair
        float maxRotUp = dimaScript.spriteRotation == 1 ? 125 : 240;
        float maxRotDown = dimaScript.spriteRotation == 1 ? 412 : 300;

        Quaternion lookRot = GenericFunctions.GetLookRotation(crosshair, transform);
        Vector3 adjustedRot = lookRot.eulerAngles;
        adjustedRot = new Vector3(0, 0, adjustedRot.z + 90);

        if (dimaScript.spriteRotation != latestSpriteRot) {
            if (latestRot.z <= 92 || latestRot.z <= 240) {
                latestRot.z = dimaScript.spriteRotation == 1 ? latestRot.z + 230 : latestRot.z - 230; // TODO: adjust thse more precicely
            } else if (adjustedRot.z >= 450 || adjustedRot.z >= 300) {
                latestRot.z = dimaScript.spriteRotation == 1 ? latestRot.z + 100 : latestRot.z - 100; // TODO: adjust thse more precicely
            }    
            latestSpriteRot = dimaScript.spriteRotation;
        }

        if (dimaScript.spriteRotation == 1) {
            latestRot = new Vector3(0, 0, Mathf.Abs(latestRot.z));
        } else {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, -transform.rotation.z));
        }

        if  (dimaScript.spriteRotation == 1 && adjustedRot.z >= 92 && adjustedRot.z <= 125 ||
             dimaScript.spriteRotation == 1 && adjustedRot.z <= 450 && adjustedRot.z >= 412) {
            latestRot = adjustedRot;
        } else if (dimaScript.spriteRotation == -1 && adjustedRot.z >= 240 && adjustedRot.z <= 300) {
            latestRot = adjustedRot;
        }

        transform.rotation = Quaternion.Euler(latestRot);
    }
}
