using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    private KeyCode leftKey;
    private KeyCode rightKey;
    private KeyCode jumpKey;

    private float xAxis;
    private float yAxis;
    private float deceleration;
    private Player player;

    private void Start()
    {
        player = gameObject.GetComponent<Player>();
    }

    public bool getLeftKey() 
    {
        return Input.GetKey(leftKey);
    }

    public bool getRightKey()
    {
        return Input.GetKey(rightKey); ;
    }

    public bool getJumpKey()
    {
        return Input.GetKeyDown(jumpKey);
    }
    
    public float getXAxis() 
    {
        if (player.isOnGround()) 
        {
            deceleration = 3f;
        } else {
            deceleration = .5f;
        }

        if (getLeftKey())
        {
            xAxis -= 1f;
        }
        else if (getRightKey())
        {
            xAxis += 1f;
        } 

        xAxis = Mathf.Clamp(xAxis, -1, 1);
        return xAxis;
    }


    public void setLeftKey(KeyCode key) {
        leftKey = key;
    }

    public void setRightKey(KeyCode key)
    {
        rightKey = key;
    }

    public void setJumpKey(KeyCode key)
    {
        jumpKey = key;
    }
}
