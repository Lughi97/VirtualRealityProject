using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    /*
    public static async void LoadScene(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        //woodBoard.SetActive(true);
        crossFade.SetTrigger("Start");
        await Task.Delay(3000);

        scene.allowSceneActivation = true;
        woodBoard.SetActive(false);

    }
    */
    public static IEnumerator load(Animator animator)
    {

        animator.SetTrigger("Start");
        yield return new WaitForSeconds(3f);
         GameManager.Instance.tempGround.GetComponent<RotationWorld>().enabled = false;
        // if (GameManager.Instance.isGameOver==false) {
        Debug.Log("HEELO THHHHEHEHRERE");
        animator.SetTrigger("Start");
        GameManager.Instance.nextLevel();
        yield return new WaitForSeconds(1.5f);
        GameManager.Instance.tempGround.GetComponent<RotationWorld>().enabled = true;

        //  }
        // else
        //  {

        //    GameManager.Instance.returnMenu();
        //woodBoard.SetActive(true);
        //crossFade.SetTrigger("Start");
        // }


    }

    public static IEnumerator  loadToMainMenu(Animator animator)
    {
        animator.SetTrigger("Start");

        GameManager.Instance.typeScene = SceneLevel.MainMenu;
        yield return new WaitForSeconds(3f);


        Debug.Log("CHANGE");
        GameManager.Instance.mainMenuLevel = true;
        SceneManager.LoadScene("MainMenu");


        //cross fade from this to next scene 
    }
}
