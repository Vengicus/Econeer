using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectTime : MonoBehaviour {

	float seconds;
	float minutes;
	//float hours;
	public Text timePlayed;
	public Text endOfTurn;



	void start(){
		
	}
	void Update(){
		
		seconds += Time.deltaTime;

		if (seconds >= 60) {
			minutes += 1;
			seconds = 0;
		}

		if (minutes >= 60) {
			//hours += 1;
			minutes = 0;
		}

		if ((int)minutes == 3) {
			endOfTurn.text = "END OF SESSION";
			Time.timeScale = 0;

			
		}
		//timePlayed.text = ("M: " + minutes + " S: " + (int)seconds);
		timePlayed.text = (minutes + ":" + (int)seconds);
		//Debug.Log ("H: " + hours + " M: " + minutes + " S: " + (int)seconds);
	}
}
