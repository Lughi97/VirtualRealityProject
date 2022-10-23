using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerTemplate : MonoBehaviour
{
    public abstract void checkStatusGame();
    public abstract IEnumerator coolDown();

}
public static class ActivePower{
    public static bool powerCameraActive = false;
    public static bool powerArrowActive = false;
    public static float Timer;


    public static void setUpPower()
    {
        switch (GameManager.Instance.currentLevel)
        {
            case 1:
                Timer = 10f;
                break;
            case 2:
                Timer = 12f;
                break;
            case 3:
                Timer = 15f;
                break;

        }
    }
}
