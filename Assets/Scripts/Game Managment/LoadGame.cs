using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Load the new level or load to the menu if game over
/// </summary>
public static class LoadGame
{
    public static GameObject woodBoard;
    public static Animator crossFade;

    public static void getBoard(GameObject board, Animator animator)
    {
        woodBoard = board;
        crossFade = animator;
    }

    public static IEnumerator LoadScene(string sceneName)
    {
        crossFade.SetTrigger("Start");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(sceneName);
        woodBoard.SetActive(false);
    }
  
    // load the new level in the same scene 
    public static IEnumerator loadNextLevel(Animator animator)
    {

        animator.SetTrigger("Start");
        yield return new WaitForSeconds(3f);
         GameManager.Instance.tempGround.GetComponent<RotationWorld>().enabled =false ;
       
        animator.SetTrigger("Start");
        GameManager.Instance.nextLevel();
       // yield return new WaitForSeconds(1.5f);
        

    }

    // change to the menu scene
    public static IEnumerator  loadToMainMenu(Animator animator)
    {
        animator.SetTrigger("Start");

        GameManager.Instance.typeScene = SceneLevel.MainMenu;
        yield return new WaitForSeconds(3f);


        Debug.Log("CHANGE");
        GameManager.Instance.mainMenuLevel = true;
        SceneManager.LoadScene("MainMenu");
    }

    //change between different menu type in te same scene
    public static IEnumerator loadNewMenu(Animator animator)
    {
        animator.speed = 2f;
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(2f);
        animator.SetTrigger("Start");

    }
}
