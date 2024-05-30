using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    Camera cam;
    MoveController mc;
    Trayertory t;
    bool isDragging;
    Vector2 starPoint;
    Vector2 mouseStartPoint;
    Vector2 endPoint;
    Vector2 direction;
    Vector2 force;
    float distance;

    void Start()
    {
        cam = Camera.main;
        mc = GetComponent<MoveController>();
        t = GetComponent<Trayertory>();
    }

    void Update()
    {
        if(mc.canThrow)
        {
            if(Input.GetMouseButtonDown(0))
            {
                mouseStartPoint = cam.ScreenToViewportPoint(Input.mousePosition);
            }
            if(Input.GetMouseButton(0))
            {
                isDragging = true;
                OnDrawStart();
            }
            if(Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                OnDrawEnd();
            }
            if(isDragging)
            {
                OnDraw();
            }
        }
    }
    private void OnDrawStart()
    {
        starPoint = mc.SpawnPoint.position;
        t.Show();
    }
    private void OnDraw()
    {
        endPoint = cam.ScreenToViewportPoint(Input.mousePosition);
        distance = Vector2.Distance(mouseStartPoint, endPoint) * mc.throwForce;
        direction = (mouseStartPoint - endPoint).normalized;
        force = distance * mc.throwForce * direction;

        t.UpdateDots(mc.SpawnPoint.position, force);
    }
    private void OnDrawEnd()
    {
        mc.ThrowBomb(force);
        t.Hide();
        Clear();
    }
    private void Clear()
    {
        endPoint = Vector2.zero;
        distance = 0;
        direction = Vector2.zero;
        force = Vector2.zero;
    }
}
