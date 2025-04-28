using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private bool tutorialOpen = true;



    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.H))
        {
            if (!tutorialOpen)
            {
                animator.SetBool("Opened", true);
                tutorialOpen = true;
            }
            else if (tutorialOpen)
            {
                animator.SetBool("Opened", false);
                tutorialOpen = false;
            }
        }
    }
}
