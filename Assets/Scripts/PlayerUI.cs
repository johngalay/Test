using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

	[SerializeField] public Text m_UIText;

	public void UpdateUI(int newHealth) {
		Debug.Log("Passed: " + newHealth);
		m_UIText.text = newHealth.ToString();
	}

}
