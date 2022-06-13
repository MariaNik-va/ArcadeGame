using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLose : MonoBehaviour
{
    [SerializeField] private GameObject youDied;

    private void Update()
    {
       if (this.GetComponent<Health>().dead)
        {
            youDied.SetActive(true);
        } 
    }
    private void RestartGame()
    {
        youDied.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }
}
