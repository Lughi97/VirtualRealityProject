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
    public static IEnumerator loadNextLevel(GameObject canvas)
    {
        crossFade = canvas.transform.Find("CrossFade").gameObject.GetComponent<Animator>();
        crossFade.SetTrigger("Start");
        AnimatorClipInfo[] clips = crossFade.GetCurrentAnimatorClipInfo(0);
        //Fetch the current Animation clip information for the base layer
        float lenght = clips[0].clip.length;
        Debug.Log("TIME " + lenght);
        //Access the current length of the clip
        yield return new WaitForSeconds(lenght);
        GameManager.Instance.tempGround.GetComponent<RotationWorld>().enabled =false ;
        crossFade.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        GameManager.Instance.nextLevel();
       // yield return new WaitForSeconds(1.5f);
        

    }

    // change to the menu scene
    public static IEnumerator  loadToMenuFormTitle(GameObject canvas)
    {

        crossFade = canvas.transform.Find("CrossFade").gameObject.GetComponent<Animator>();
        crossFade.SetTrigger("Start");

        GameManager.Instance.typeScene = SceneLevel.MainMenu;
        yield return new WaitForSeconds(3f);


        Debug.Log("CHANGE");
        GameManager.Instance.mainMenuLevel = true;
        SceneManager.LoadScene("MainMenu");
    }
    public static IEnumerator loadMainMenuFromGame(GameObject canvas)
    {

        crossFade = canvas.transform.Find("CrossFade").gameObject.GetComponent<Animator>();
        AnimatorClipInfo[] clips = crossFade.GetCurrentAnimatorClipInfo(0);
        float lenght = clips[0].clip.length;
        crossFade.SetTrigger("Start");
       
      

        yield return new WaitForSeconds(lenght);

        GameManager.Instance.playerDeath = false;
        Debug.Log("LOAD MENU SCENE");
        GameManager.Instance.isLoaded = false;
        GameManager.Instance.mainMenuLevel = true;
        GameManager.Instance.isGameOver = false;
        MusicManager.Instance.StopAll();
        SFXManager.Instance.StopAll();

        GameManager.Instance.playerLifes = 1;
        GameManager.Instance.typeScene = SceneLevel.MainMenu;
        Debug.Log("CHANGE");
        GameManager.Instance.mainMenuLevel = true;
        SceneManager.LoadScene("MainMenu");
    }
    //change between different menu type in te same scene
    public static IEnumerator loadNewMenu(GameObject canvas)
    {
        crossFade = canvas.transform.Find("CrossFade").gameObject.GetComponent<Animator>();
        crossFade.speed = 2f;
        crossFade.SetTrigger("Start");
        yield return new WaitForSeconds(2f);
        crossFade.SetTrigger("Start");

    }
   public static IEnumerator WaitForFade(GameObject canvas)
    {
        // AnimationClip[] clips = crossFade.GetComponent<Animator>().runtimeAnimatorController.animationClips;
        // Debug.Log(clips);
        //Get them_Animator, which you attach to the GameObject you intend to animate.
        crossFade = canvas.transform.Find("CrossFade").gameObject.GetComponent<Animator>();
        AnimatorClipInfo[] clips = crossFade.GetCurrentAnimatorClipInfo(0);
        //Fetch the current Animation clip information for the base layer
        float lenght = clips[0].clip.length;
        Debug.Log("TIME " + lenght);
        //Access the current length of the clip
        yield return new WaitForSeconds(lenght);
        GameManager.Instance.tempGround.GetComponent<RotationWorld>().enabled = true;

    }
}
