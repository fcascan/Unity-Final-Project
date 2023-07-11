using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWarriorSpawnScript : MonoBehaviour
{
    public GameObject spawnPointLeft;
    public GameObject spawnPointMiddle;
    public GameObject spawnPointRight;

    // Start is called before the first frame update
    void Start() {
        switch (PortalTrigger.portalTriggered) {
            case PortalTrigger.PortalTypes.LEFT :
                this.transform.position = spawnPointLeft.transform.position;
                break;
            case PortalTrigger.PortalTypes.MIDDLE :
                this.transform.position = spawnPointMiddle.transform.position;
                break;
            case PortalTrigger.PortalTypes.RIGHT :
                this.transform.position = spawnPointRight.transform.position;
                break;
            default : 
                //Case NONE -> Do Nothing
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
