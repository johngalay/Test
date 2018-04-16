using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetwork : MonoBehaviour {
	[SerializeField] private Transform playerCamera;
	[SerializeField] private MonoBehaviour[] playerControlSripts;

	private delegate void UpdateUI(int newHealth);
	private event UpdateUI updateUI;

	private PhotonView photonView;
	public int playerHealth = 90;

	private void Start() {
		photonView = GetComponent<PhotonView>();

		// Update the event with a method
		updateUI += FindObjectOfType<PlayerUI>().UpdateUI;
		//myRigidbody.gameObject.GetComponent<PlayerUI>().m_UIText = textUI.GetComponent<Text>();
		//this.GetComponentInParent<PlayerUI>().m_UIText = this.GetComponentInParent<GeneratePlayerUI>().textUI.GetComponent<Text>();

		Initialize();
	}

	private void Initialize() {
		if(photonView.isMine) {

		} else {
			// Disabling camera
			playerCamera.gameObject.SetActive(false);
			// Disabling control scripts
			foreach(MonoBehaviour m in playerControlSripts){
				m.enabled = false;
			}
		}
		// Update UI on start
		updateUI(playerHealth);
	}

	private void Update() {
		if(!photonView.isMine) {
			return;
		}

		if(Input.GetKeyDown(KeyCode.E)){
			playerHealth = playerHealth - 5;
			updateUI(playerHealth);
		}
	}

	private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
		// send data
		if(stream.isWriting) {
			Debug.Log("Writing");
			stream.SendNext(playerHealth);
		}

		// receive data
		else if(stream.isReading) {
			Debug.Log("Reading");
			playerHealth = (int)stream.ReceiveNext();
			updateUI(playerHealth);
		}
	}
}
