using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CameraControlsTopDown3D : MonoBehaviour {
    /* A bunch of stuff that relates to how the camera shakes*/
    public enum ShakePresets
    {
        NONE,
        BOTH,
        HORIZONTAL,
        VERTICAL
    };
	private float shakeIntensity = 0.0f;
	private float shakeDuration = 0.0f;
	private ShakePresets shakeDirection = ShakePresets.BOTH;
	private Vector2 shakeOffset = new Vector2();

    public float zoomSpeed = 20f;
    public float maxZoomFOV = 10f;

    public GameObject focalPointPrefab;

    public LayerMask layerMask;

    private Camera cameraComponent;

    public GameObject target;
    public List<GameObject>visibleTargets; //accounts for all other targets that aren't just the main focus

    private float original_camera_size;
    private float min_camera_size;
    private float max_camera_size;
    private float target_camera_size;

    //Self references
    private Rigidbody selfBody;


    /* camera moving constants */
    public float Y_OFFSET = 40.0f;
    public float Z_OFFSET = 40.0f;
    private float OFFSET_FROM_PLAYER;
    private const float MIN_OFFSET_FROM_PLAYER = 0.3f;

    public float targetRotation_Y;

    private const float TARGETING_LOWER_BOUND = 0.0f;
    private const float TARGETING_UPPER_BOUND = 1.0f;
    private const float ZOOM_IN_LOWER_BOUND = 0.3f;
    private const float ZOOM_IN_UPPER_BOUND = 0.7f;
    private const float ZOOM_OUT_LOWER_BOUND = 0.2f;
    private const float ZOOM_OUT_UPPER_BOUND = 0.8f;

    private const float ZOOM_RATE = 0.02f;

    private const float PAN_SPEED = 5.0f;

    public bool isFixed;

    public static CameraControlsTopDown3D instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (this != instance)
            {
                Destroy(this.gameObject);
            }
        }
    }

    // Use this for initialization
    void Start () {
        cameraComponent = GetComponent<Camera>();

        visibleTargets = new List<GameObject>();
        original_camera_size = cameraComponent.orthographicSize;
        min_camera_size = 0.75f * original_camera_size;
        max_camera_size = 2.0f * original_camera_size;
        target_camera_size = original_camera_size;


        OFFSET_FROM_PLAYER = -Z_OFFSET / Mathf.Cos(gameObject.transform.rotation.x);
        this.selfBody = this.GetComponent<Rigidbody>();

        //float Cam_X_OFFSET = -OFFSET_FROM_PLAYER * Mathf.Sin(Mathf.Deg2Rad * targetRotation_Y);
        //float Cam_Z_OFFSET = -OFFSET_FROM_PLAYER * Mathf.Cos(Mathf.Deg2Rad * targetRotation_Y);

        //transform.position = target.transform.position + new Vector3(Cam_X_OFFSET, Y_OFFSET, Cam_Z_OFFSET);

    }

    void FixedUpdate () {

        //Do shake calculations
        if (shakeDuration > 0)
        {
            shakeDuration -= Time.deltaTime;
            if (shakeDuration <= 0)
                stopShaking();
            else
                applyShake();
        }

        //Handles clipping into terrain
        //Actually forget this its kind of a pair we'll figure out a better solution later
        /*
        Vector3 relativePos = transform.position - (target.transform.position);
        RaycastHit hit;
        float OFFSET_FROM_PLAYER_NOCLIP = OFFSET_FROM_PLAYER;
        if (Physics.Raycast(target.transform.position, relativePos, out hit, Mathf.Infinity, layerMask))
        {
            //Need to redo everything to play nice with this
            Debug.Log("Hit something" + hit.rigidbody.gameObject.name);
            Debug.DrawLine(target.transform.position, hit.point);
            OFFSET_FROM_PLAYER_NOCLIP = Mathf.Clamp(hit.distance, MIN_OFFSET_FROM_PLAYER, OFFSET_FROM_PLAYER);
            print(hit.distance);
            print(OFFSET_FROM_PLAYER_NOCLIP);
        }
        */

        float Cam_X_OFFSET = -OFFSET_FROM_PLAYER * Mathf.Sin(Mathf.Deg2Rad * targetRotation_Y);
        float Cam_Z_OFFSET = -OFFSET_FROM_PLAYER * Mathf.Cos(Mathf.Deg2Rad * targetRotation_Y);
        
        //Now follow the target
        if (transform.position != target.transform.position + new Vector3(Cam_X_OFFSET, Y_OFFSET, Cam_Z_OFFSET))
        {
            float x = ((target.transform.position + new Vector3(Cam_X_OFFSET, Y_OFFSET, Cam_Z_OFFSET)) - transform.position).x;
            float y = ((target.transform.position + new Vector3(Cam_X_OFFSET, Y_OFFSET, Cam_Z_OFFSET)) - transform.position).y;
            float z = ((target.transform.position + new Vector3(Cam_X_OFFSET, Y_OFFSET, Cam_Z_OFFSET)) - transform.position).z;

            //Do not path horizontally with the player if the camera is fixed
            if (isFixed)
            {
                x = 0;
                z = 0;
            }

            selfBody.velocity = new Vector3(x * PAN_SPEED, y * PAN_SPEED, z * PAN_SPEED);
        }
        else
        {
            selfBody.velocity.Set(0.0f, 0.0f, 0.0f);
        }

        //Match the target rotation
        if((targetRotation_Y - transform.rotation.eulerAngles.y) > 1.0f)
        {
            float y = targetRotation_Y - transform.rotation.eulerAngles.y;


            selfBody.angularVelocity = new Vector3(0, y * PAN_SPEED * Time.deltaTime, 0);
        }
        else
        {
            selfBody.angularVelocity.Set(0.0f, 0.0f, 0.0f);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, targetRotation_Y, transform.rotation.eulerAngles.z);
        }

        //used to scale the camera's size in response to more targets
        List<GameObject> targetsToRemove = new List<GameObject>();

        foreach(GameObject targ in visibleTargets)
        {
            //check that the target is in bounds
            if (inCameraBounds(targ))
            {
                resizeToFitTarget(targ);
            }
            else
            {
                //crap need to do other things like changing player state, changing camera modes, etc.
                targetsToRemove.Add(targ);
                continue;
            }
                
            /*
            Vector3 targetCameraPosition = cameraComponent.WorldToViewportPoint(targ.transform.position);
            float cameraSize = cameraComponent.orthographicSize;

            if (!(0.1f < targetCameraPosition.x && targetCameraPosition.x < 0.9f && 0.1f < targetCameraPosition.y && targetCameraPosition.y < 0.9f))
            {
                if (cameraSize < max_camera_size)
                    cameraComponent.orthographicSize = Mathf.MoveTowards(cameraSize, cameraSize * 1.02f, zoomSpeed * Time.deltaTime);
            }

            if(0.3 < targetCameraPosition.x && targetCameraPosition.x < 0.7f && 0.3f < targetCameraPosition.y && targetCameraPosition.y < 0.7f)
            {
                if (cameraComponent.orthographicSize > min_camera_size)
                    cameraComponent.orthographicSize = Mathf.MoveTowards(cameraSize, cameraSize * 0.98f, zoomSpeed * Time.deltaTime);
            }
             */
        }

        foreach (GameObject targ in targetsToRemove)
        {
            visibleTargets.Remove(targ);
        }

        //If no targets are in range, go back to the original player
        if (visibleTargets.Count == 1)
        {
            Target(visibleTargets[0]);
        }

        //external gradual resizing
        float cameraSize = cameraComponent.orthographicSize;
        //Current issue is that if the character moves too quickly, the opponent then leaves the FOV too quickly, resulting in an awkward camera
        if (cameraSize < target_camera_size)
            cameraComponent.orthographicSize = Mathf.MoveTowards(cameraSize, cameraSize * 1 + ZOOM_RATE, zoomSpeed * Time.deltaTime);
        if (cameraSize > target_camera_size)
            cameraComponent.orthographicSize = Mathf.MoveTowards(cameraSize, cameraSize * 1 - ZOOM_RATE, zoomSpeed * Time.deltaTime);
        
	}

    public bool HasTarget()
    {
        return target == null;
    }

    //Used to just follow a specific mobile
    public void Target(GameObject target)
    {
        this.target = target.gameObject;
    }

    //Used when the player needs to target some enemy etc. Creates a focus point game object
    public void Target(GameObject player, GameObject target)
    {
        GameObject new_target = Instantiate(focalPointPrefab);
        new_target.GetComponent<FocalPoint>().setTargets(player, target);
        this.target = new_target.GetComponent<FocalPoint>().gameObject;

        //hacky test case stuff
        this.visibleTargets.Add(player.gameObject);
        this.visibleTargets.Add(target.gameObject);
    }

    public void Shake(float Intensity = 0.1f, 
                        float Duration = 0.5f,
                        bool Force = true, 
                        ShakePresets Direction = ShakePresets.BOTH)
    {
        if(!Force && ((shakeOffset.x != 0) || (shakeOffset.y != 0)))
			return;
		shakeIntensity = Intensity;
		shakeDuration = Duration;
		shakeDirection = Direction;
        shakeOffset.Set(0, 0);
    }

    private void stopShaking()
    {
        shakeOffset.Set(0, 0);
    }

    private void applyShake()
    {
        if (shakeDirection == ShakePresets.BOTH || shakeDirection == ShakePresets.HORIZONTAL)
                    shakeOffset.x = (UnityEngine.Random.Range(-1.0F, 1.0F) * shakeIntensity);
        if (shakeDirection == ShakePresets.BOTH || shakeDirection == ShakePresets.VERTICAL)
              shakeOffset.y = (UnityEngine.Random.Range(-1.0F, 1.0F) * shakeIntensity);

        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;

        transform.position = new Vector3(x + shakeOffset.x, y + shakeOffset.y, z);
    }

    private bool inCameraBounds(GameObject target)
    {
        if (cameraComponent.orthographicSize != target_camera_size)
            return true;

        Vector3 targetCameraPosition = cameraComponent.WorldToViewportPoint(target.transform.position);
        return (0.0f < targetCameraPosition.x && targetCameraPosition.x < 1.0f && 0.0f < targetCameraPosition.y && targetCameraPosition.y < 1.0f);
    }

    private void resizeToFitTarget(GameObject target)
    {
        float originalSize = cameraComponent.orthographicSize;
        float calculatedSize = originalSize;

        Vector3 targetCameraPosition = cameraComponent.WorldToViewportPoint(target.transform.position);

        while (!(ZOOM_OUT_LOWER_BOUND < targetCameraPosition.x && targetCameraPosition.x < ZOOM_OUT_UPPER_BOUND
              && ZOOM_OUT_LOWER_BOUND < targetCameraPosition.y && targetCameraPosition.y < ZOOM_OUT_UPPER_BOUND))
        {
            if (calculatedSize < max_camera_size)
            {
                cameraComponent.orthographicSize *= 1+ZOOM_RATE;
                calculatedSize = cameraComponent.orthographicSize;
            }
            else
                break;

            targetCameraPosition = cameraComponent.WorldToViewportPoint(target.transform.position);
        }

        while (ZOOM_IN_LOWER_BOUND < targetCameraPosition.x && targetCameraPosition.x < ZOOM_IN_UPPER_BOUND
            && ZOOM_IN_LOWER_BOUND < targetCameraPosition.y && targetCameraPosition.y < ZOOM_IN_UPPER_BOUND)
        {
            if (calculatedSize > min_camera_size)
            {
                cameraComponent.orthographicSize *= 1-ZOOM_RATE;
                calculatedSize = cameraComponent.orthographicSize;
            }
            else
                break;

            targetCameraPosition = cameraComponent.WorldToViewportPoint(target.transform.position);
        }

        cameraComponent.orthographicSize = originalSize;
        target_camera_size = calculatedSize;
    }
}
