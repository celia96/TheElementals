using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinPanel : MonoBehaviour {


	public Text coinText;
	public int numCoins;
	public static CoinPanel instance;

	void Awake () {
		instance = this;
	}

	public void CollectCoins () {
	
		numCoins++;
		setCoinText ();
	
	}

	public void removeCoins () {
		numCoins -= 10;
		setCoinText ();
	} 

	public void setCoinText () {
		coinText.text = numCoins.ToString ();

	}

	public bool canUseItem () {
		if (numCoins >= 10) {
			return true;
		} else { 
			return false; 
		}
	}





}
