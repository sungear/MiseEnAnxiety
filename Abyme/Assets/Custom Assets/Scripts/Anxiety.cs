using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using UnityStandardAssets.Characters.FirstPerson;

public class Anxiety : MonoBehaviour {
	
	public GameObject player; //FPS Controller
	private MoveCheck movingCheck;
	private bool isMoving;
	
	public GameObject mainCam; //FirstPersonCharacter (enfant de FPS Controller)	
	public float maxBlurTime = 1; //mettre à 10 pour un effet satisfaisant
	private float blurTime;
	private BlurOptimized blurringEffect;

	public float anxietyGauge;
	public int anxietyMax;
	public int threatLevel;

	public float maxParalysingTime;
	public float paralyzingTime;
	private bool isParalyzed;
	private CharacterController characterController;	
	private MouseLook mouse;

	public float anxietyFactor;

	public float invincibilityTime;
	private bool isInvicible;
	private float maxInvincibilityTime;
	private int newThreatLevel;

	public Text anxText;
	private string anxLabel = "ANXIETY : ";

	public GameObject enemy;

	void Start() {
		blurringEffect = mainCam.GetComponent<BlurOptimized>();
		characterController = player.GetComponent<CharacterController>();
		movingCheck = player.GetComponent<MoveCheck>();

		isMoving = movingCheck.Moving();

		threatLevel = 0;
		blurTime = maxBlurTime;

		blurringEffect.downsample = 0;
		blurringEffect.blurSize = 0;
		blurringEffect.blurIterations = 1;		
		blurringEffect.enabled = false;

		mouse = mainCam.GetComponent<MouseLook>();
		mouse.enabled = true;
		isParalyzed = false;
		paralyzingTime = maxParalysingTime;

		maxInvincibilityTime = invincibilityTime;
	}

	void FixedUpdate(){


		if(Vector3.Distance(enemy.transform.position, transform.position) > 0)
			threatLevel = (int)(1/ Vector3.Distance(enemy.transform.position, transform.position) * 10);
		else
			threatLevel = 0;
		threatLevel--;

		anxText.text = anxLabel + Mathf.Ceil(anxietyGauge).ToString();

		isMoving = movingCheck.Moving();

		AnxietyUp();

        if (anxietyGauge < 0) {
            anxietyGauge = 0;
        }

		if (isInvicible) {
			invincibilityTime -= Time.deltaTime;
			if (invincibilityTime <= 0) {
				isInvicible = false;
				invincibilityTime = maxInvincibilityTime;
			}
			else if (threatLevel <= 0) {
				isInvicible = false;
				invincibilityTime = maxInvincibilityTime;
			}
		}

		if (threatLevel > 0 ) {
			Blurring();
		}

		if (anxietyGauge >= (anxietyMax/2) + 10) {
			if (isMoving) {
				Blurring();
			}
			else if (!isMoving && threatLevel <= 0){
				Unblurring();
			}

			if (anxietyGauge >= anxietyMax){
				anxietyGauge = anxietyMax;
				isParalyzed = true;
				Paralyzing();
				isParalyzed = false;
				isInvicible = true;	
			}
			else if (!isParalyzed && !isMoving) {
				AnxietyDown();
			}
		}
		

		else if (!isMoving && threatLevel <= 0) {
			Unblurring();
			AnxietyDown();
		}

	}


	void Blurring() {
		blurringEffect.enabled = true;
		if (blurringEffect.blurIterations < 3) {
			blurTime -= Time.deltaTime;
			if (blurTime <= 0) {
				blurringEffect.blurIterations++;
				blurTime = maxBlurTime;
			}
		}
		if(blurringEffect.blurIterations >= 2 && blurringEffect.downsample < 2) {
			blurTime -= Time.deltaTime;
			if (blurTime <= 0) {
				blurringEffect.downsample++;
				blurTime = maxBlurTime;
			}
		}						
		if (blurringEffect.blurSize < 6){			
			blurringEffect.blurSize += 0.2f * Time.deltaTime;
		}
	}

	void Unblurring() {
		if (blurringEffect.blurIterations <= 1 && blurringEffect.downsample <= 0 && blurringEffect.blurSize <= 0) {
			blurringEffect.downsample = 0;
			blurringEffect.blurSize = 0;
			blurringEffect.blurIterations = 1;				
			blurringEffect.enabled = false;
		}
		if (blurringEffect.blurIterations > 1){
			blurTime -= Time.deltaTime;
			if (blurTime <= 0) {
				blurringEffect.blurIterations--;
				blurTime = maxBlurTime;
			}
		}
		if(blurringEffect.blurIterations <= 2 && blurringEffect.downsample > 0) {
			blurTime -= Time.deltaTime;
			if (blurTime <= 0) {
				blurringEffect.downsample--;
				blurTime = maxBlurTime;
			}
		}							
		if (blurringEffect.blurSize > 0){			
			blurringEffect.blurSize -= 0.2f * Time.deltaTime;
		}

	}

	void Paralyzing() {
		characterController.enabled = false;
		mouse.enabled = false;
		paralyzingTime -= Time.deltaTime;

		if (paralyzingTime <= 0) {
			characterController.enabled = true;
			mouse.enabled = true;
			paralyzingTime = maxParalysingTime;
			anxietyGauge -= 3;
		}
	}

	void AnxietyUp() {
		if (anxietyGauge < anxietyMax && !isInvicible){
			anxietyGauge += Time.deltaTime * threatLevel * anxietyFactor;
			if (anxietyGauge > anxietyMax){
				anxietyGauge = anxietyMax;
			}
		}	
	}

	void AnxietyDown() { 
		if (threatLevel <= 0){
			anxietyGauge -= Time.deltaTime;
			if (anxietyGauge < 0){
				anxietyGauge = 0;
			}
		}
	}
}
