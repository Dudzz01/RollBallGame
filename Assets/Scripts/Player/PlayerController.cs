using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region ComponentsOfPlayerVariables
    [SerializeField] private Rigidbody rig;
    #endregion
    #region MovimentVelocityPlayerVariables
    public float SpeedPlayer{get; set;}
    #endregion
    #region DirectionPlayerVariables
    public float DirX{get; set;}
    public float DirZ{get; set;}

    
    #endregion
    #region RotationPlayerVariables
    public float rotX{get; set;}
    public float rotZ{get; set;}
    #endregion
    #region RaycastPlayerVariables

    RaycastHit hit;
    #endregion

    #region ModePlayerVariables
    private string modePlayerStyle;
    #endregion
    private void Start()
    {
        rotX = 1;
        rotZ = 2;
        SpeedPlayer = 5;
        modePlayerStyle = "WalkPlayer";
    }
    void Update()
    {
        #region SetDirectionsPlayer
        DirX = Input.GetAxisRaw("Horizontal");
        DirZ = Input.GetAxisRaw("Vertical");
        #endregion
        
        #region ActionPlayerMethods
        
        Walk();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        SetModePlayer();
        #endregion
        
    }

    void FixedUpdate() {
        PlayerRotateFollowingMouse();
    }
    
    public void Walk()
    {
        
            rig.velocity = new Vector3(DirX*SpeedPlayer,rig.velocity.y,DirZ*SpeedPlayer);
        
            if(modePlayerStyle == "WalkPlayer")
            {
                if(!Input.anyKey || Input.GetKey("a") || Input.GetKey("d"))
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(0,transform.rotation.y,0), Time.deltaTime+0.05f);
                } 
             
             
                transform.rotation *= Quaternion.Euler(new Vector3((DirZ*rotX) ,0,(DirX*rotZ) * -1));
            }
        
    }

    public void SetModePlayer()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            modePlayerStyle = "ShootPlayer";
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            modePlayerStyle = "WalkPlayer";
        }
    }

    public void PlayerRotateFollowingMouse()
    {
        
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit ,100) && modePlayerStyle == "ShootPlayer")
        {

            Vector3 playerToMouse = hit.point - transform.position;
            playerToMouse.y = 0;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            rig.MoveRotation(newRotation);

            
        }

    }

    public void Shoot()
    {
        Camera.main.ScreenPointToRay(Input.mousePosition);
    }

    public void Jump()
    {
        rig.AddForce(0,7,0,ForceMode.Impulse);
    }

    

    
}
