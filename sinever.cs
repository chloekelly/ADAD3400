using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sinever : MonoBehaviour
{

    private Joycon j;

    // Values made available via Unity
    public float[] stick;
    public Vector3 gyro;
    public Vector3 accel;
    public Quaternion orientation;


    public AudioSource song;


    void Start()
    {
        gyro = new Vector3(0, 0, 0);
        accel = new Vector3(0, 0, 0);

        // get the public Joycon object attached to the JoyconManager in scene
        j = JoyconManager.Instance.j;

    }

    // Update is called once per frame
    void Update()
    {
        // make sure the Joycon only gets checked if attached
        if (j != null && j.state > Joycon.state_.ATTACHED)

        {
            stick = j.GetStick();  // Gyro values: x, y, z axis values (in radians per second)
            gyro = j.GetGyro();

            // Accel values:  x, y, z axis values (in Gs)
            accel = j.GetAccel();

            orientation = j.GetVector();



        }


        gameObject.transform.rotation = orientation;
        song.pitch = gyro.y * -0.75f + 1.25f;
        song.reverbZoneMix = accel.x / 2 + 1;
        //song.volume = gyro.x;
    }
}
