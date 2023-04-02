using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CottonFibre
{
    private float x;
    private float y;

    private float velx;
    private float vely;
    private float velmin;
    private float velmax;


    private float speed;
    private float velocity;
    private float circleSize;


    public CottonFibre(float circleSize,float speed,float velocity)
    {

        this.speed = speed;
        this.velocity = velocity;
        this.circleSize = circleSize;

        init();
    }

    void init()
    {
        float angle = Random.Range(0f, 360f);

        this.x = Screen.width * 0.5f + (Mathf.Cos(angle) * circleSize);
        this.y = Screen.height * 0.5f - (Mathf.Sin(angle) * circleSize);

        velx = Random.Range(-20f, 20f) / 100f;
        vely = Random.Range(-20f, 20f) / 100f;

        velmin = Random.Range(2f, 10f);
        velmax = Random.Range(10f, 100f) / 10f;

    }

    public void Update()
    {
         float forceDirectionX;
         float forceDirectionY;

        forceDirectionX = Random.Range(-1f, 1f);
        forceDirectionY = Random.Range(-1f, 1f);

        if (Mathf.Abs(velx + forceDirectionX) < velmax)
        {
            velx += forceDirectionX;
        }
        if (Mathf.Abs(vely + forceDirectionY) < velmax)
        {
            vely += forceDirectionY;
        }

        GL.Vertex3(x, y, 0);
        x += velx * speed;
        y += vely * speed;

        if (Mathf.Abs(velx) > velmin)
        {
            velx *= velocity;
        }

        if (Mathf.Abs(vely) > velmin)
        {
            vely *= velocity;
         }

        GL.Vertex3(x, y, 0);

        if (x > Screen.width || x < 0) init();
        if (y > Screen.height || y < 0) init();
    }
}
