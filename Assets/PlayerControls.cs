using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private IPlayerControls controlsScript;
    // Start is called before the first frame update
    void Start()
    {

        controlsScript = new BasicPlayerControls();

    }

    // Update is called once per frame
    void Update()
    {

        controlsScript.MovePlayer(gameObject);

    }
}