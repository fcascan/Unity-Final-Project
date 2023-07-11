using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTrigger : MonoBehaviour {
    [SerializeField] public PortalTypes type;
    static public PortalTypes portalTriggered = PortalTypes.NONE;

    public enum PortalTypes { 
        NONE,
        LEFT,
        MIDDLE,
        RIGHT,
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            portalTriggered = type;
            Debug.Log("portalTriggered = " + portalTriggered);
            LoadNextScene();
        }
    }

    public void LoadNextScene() {
        SceneManager.LoadScene("Level 2");
    }
}
