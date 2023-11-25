using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transitioner : MonoBehaviour
{
    public Animator animator;
 
    public void FadeOut()
    {
        animator.SetBool("FadeOut", true);
    }

    public void DelayFade()
    {
        Invoke(nameof(FadeOut), 1f);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
