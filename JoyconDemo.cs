using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyconDemo : MonoBehaviour {
	
    private Joycon j;

    // Values made available via Unity
    public float[] stick;
    public Vector3 gyro;
    public Vector3 accel;
    public Quaternion orientation;

    public AudioSource tuning;
    public AudioSource song;
    public AudioSource crash;
    public float smooth;
    public float smoothtime = 1;
    public float v;
    public bool playor;
    public float smoother;
    public float smoothertime = 3;
    public float vr;
    public float timer;
    void Start ()
    {
        gyro = new Vector3(0, 0, 0);
        accel = new Vector3(0, 0, 0);

        // get the public Joycon object attached to the JoyconManager in scene
        j = JoyconManager.Instance.j;

	}

    // Update is called once per frame
    void Update () {
		// make sure the Joycon only gets checked if attached
        if (j != null && j.state > Joycon.state_.ATTACHED)

        {
            stick = j.GetStick();  // Gyro values: x, y, z axis values (in radians per second)
            gyro = j.GetGyro();

            // Accel values:  x, y, z axis values (in Gs)
            accel = j.GetAccel();

            orientation = j.GetVector();
            smooth = Mathf.SmoothDamp(smooth, Mathf.Abs(accel.x)*2,ref v, smoothtime);
            smoother = Mathf.SmoothDamp(smoother, Mathf.Abs(accel.x) * 2, ref vr, smoothertime);


            }


            gameObject.transform.rotation = orientation;
        if (smooth > 1 && playor == false) 
            {
                tuning.Stop();
                song.Play();
                playor = true;

            }
        if ( playor == true)
        {
            if (Mathf.Abs(accel.x) < 0.3f)
            {
                timer += Time.deltaTime;
                
            }
            else { timer = 0; }

            song.pitch = smoother*1.5f;
            song.pitch = Mathf.Clamp(song.pitch, 0.8f, 1.5f);
            if(timer > 1)
            {

                song.Stop();
                
            }
            if(timer > 10)
            {
                tuning.Play();
                playor = false;
            }  
        }
        }
    }
