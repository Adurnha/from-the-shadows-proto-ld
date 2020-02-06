using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeverMinuteur : MonoBehaviour, IInteractable
{
    [SerializeField]
    Mecanism mecanism;

    [SerializeField]
    Mecanism mecanism2;

    public int timer = 5;

    private bool isActivated = false;

    private int timeLeft;
    public Text minuteurText;

    public void InteractAutreSens(PlayerController playerController) { }

    public void Interact(PlayerController playerController)
    {
        if (isActivated)
        {
            isActivated = false;

            minuteurText.transform.parent.gameObject.SetActive(false);

            mecanism.DeactivateMecanism();

            if (mecanism2 != null)
                mecanism2.DeactivateMecanism();
        }
        else if (!isActivated)
        {
            isActivated = true;

            timeLeft = timer;

            minuteurText.transform.parent.gameObject.SetActive(true);

            mecanism.ActivateMecanism();

            if (mecanism2 != null)
                mecanism2.ActivateMecanism();

            StartCoroutine(CountDown());
        }
    }

    IEnumerator CountDown()
    {
        minuteurText.text = timeLeft.ToString();
        yield return new WaitForSeconds(1f);

        if(isActivated)
        {
            if (timeLeft != 0)
            {
                timeLeft--;

                StartCoroutine(CountDown());
            }
            else
            {
                minuteurText.transform.parent.gameObject.SetActive(false);
                isActivated = false;
                mecanism.DeactivateMecanism();

                if (mecanism2 != null)
                    mecanism2.DeactivateMecanism();

            }
        }
    }
}
