using UnityEngine;
using System.Collections;

public class QuitGame : MonoBehaviour {

    // this script goes on the appropriate button

    public void quit()
    {
        Application.Quit(); //You have to select the action in the button inspector
    }
}
