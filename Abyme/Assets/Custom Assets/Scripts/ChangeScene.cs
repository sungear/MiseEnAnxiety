using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChangeScene : MonoBehaviour 
{
    //Script goes on the appropriate button
    public void Scenechange(string name)
    {
        SceneManager.LoadScene(name);//You have to change the name of the scene in the button inspector
    }
}
