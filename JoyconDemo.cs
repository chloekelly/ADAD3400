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
    public AudioSource shoosh;
   
    public float smooth;
    public float smoothtime = 1;
    public float v;
    public bool playor;
    public float smoother;
    public float smoothertime = 3;
    public float vr;
    public float timer;
    public bool crashy = false;
    public float average;

    public float songFinishedTimer = 0;
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
            smooth = Mathf.SmoothDamp(smooth, Mathf.Abs(average)*2,ref v, smoothtime);
            smoother = Mathf.SmoothDamp(smoother, Mathf.Abs(average) * 2, ref vr, smoothertime);
            smoother = Mathf.Clamp(smoother, 0.8f, 1.2f);


            }

        average = (gyro.x + gyro.y + gyro.z) / 3;

            gameObject.transform.rotation = orientation;
        if (smooth > 1 && playor == false) 
            {
                tuning.Stop();
            shoosh.Play();
                song.Play();
                playor = true;
            crashy = false;
            songFinishedTimer = 0;
            

            }
        if ( playor == true)
        {

            songFinishedTimer += Time.deltaTime;
            if (Mathf.Abs(average) < 0.3f)
            {
                //timer += Time.deltaTime;
                
            }
            else { timer = 0; }

            if (timer < 1)
            {
                crashy = false;
                song.pitch = smoother * 1.5f;
                song.pitch = Mathf.Clamp(song.pitch, 0.8f, 1.5f);
            }
            if (timer > 1)
            {
                
                song.pitch = 0;
                if (crashy == false) {
                    if (songFinishedTimer < 80 && songFinishedTimer > 5)
                    {
                        crash.Play();
                        crashy = true;
                    }
                    else {
                        tuning.Play();
                            playor = false;
                        crashy = true;

                    }
                   
                    
                
                }
                if (timer > 30.9f)
                {
                    tuning.Play();
                    playor = false;

                }


            }
            
        }

        if (Mathf.Abs(average) < 0.3f)
        {
            timer += Time.deltaTime;

        }

        
    }
    }
