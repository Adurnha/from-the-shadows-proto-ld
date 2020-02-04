using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorAxe : MonoBehaviour
{
    private Transform rotatingArm;

    private bool activated = false;
    private enum Sens { Aller, Retour }
    private Sens sens;

    public float cooldown;
    public float firstHitDelay;

    private bool doOnce = true;


    void Start()
    {
        sens = Sens.Aller;
        rotatingArm = transform.GetChild(1);
        StartCoroutine(FirstHitDelay());
    }

    void Update()
    {
        if (activated)
        {
            if(sens == Sens.Aller)
            {
                rotatingArm.eulerAngles = new Vector3(-105f, 0, 0);

                if(doOnce)
                    StartCoroutine(Retour());

            }

            if (sens == Sens.Retour)
            {
                rotatingArm.eulerAngles = new Vector3(0, 0, 0);

                if(doOnce)
                    StartCoroutine(Cooldown());
            }

        }
    }

    IEnumerator Retour()
    {
        doOnce = false;
        yield return new WaitForSeconds(1f);
        sens = Sens.Retour;
        doOnce = true;
    }
    IEnumerator FirstHitDelay()
    {
        yield return new WaitForSeconds(firstHitDelay);
        activated = true;
    }
    IEnumerator Cooldown()
    {
        doOnce = false;
        yield return new WaitForSeconds(cooldown);
        sens = Sens.Aller;
        doOnce = true;
    }
}
