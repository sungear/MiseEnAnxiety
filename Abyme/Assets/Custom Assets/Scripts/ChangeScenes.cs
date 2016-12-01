using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChangeScenes : MonoBehaviour {

	Timer timer;
	public Anxiety anxiety;

	bool isEffectOver;

	void Start() {
		timer = GetComponent<Timer>();

		isEffectOver = timer.EffectEnded();
	}

	void Update(){
		if (timer.timerTime <= 0){
			if (anxiety.anxietyGauge <= (anxiety.anxietyMax/2) - 10){
				if (!isEffectOver){
					timer.EndEffect();
					isEffectOver = timer.EffectEnded();
				}
				else {
					ChangeScene("win");
				}				
			}
			else {

                if (!isEffectOver){
                    timer.EndEffect();
                    isEffectOver = timer.EffectEnded();
                }
                else {
                    ChangeScene("gameover");
                }
			}
		}

	}

	void ChangeScene(string name) {
		SceneManager.LoadScene(name);
	}

}