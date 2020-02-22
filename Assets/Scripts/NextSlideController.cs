using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSlideController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] slides;

    private int currentSlide = 0;

    public void NextSlide()
    {
        slides[currentSlide].SetActive(false);
        ++currentSlide;
        if (currentSlide >= slides.Length)
        {
            if (SceneManager.GetActiveScene().buildIndex + 1 >= SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            return;
        }
        slides[currentSlide].SetActive(true);

    }
}
