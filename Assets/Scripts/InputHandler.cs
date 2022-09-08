using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    BallMovement ball;
    ForceDrawer forceDrawer;
    [SerializeField] Vector3 startPoint;
    [SerializeField] Vector3 endPoint;
    [SerializeField] Vector3 direction;
    [SerializeField] Vector3 force;
    [SerializeField] float distance;

    void Awake()
    {
        ball = GetComponent<BallMovement>();
        forceDrawer = GetComponentInParent<ForceDrawer>();
    }

    void Update()
    {
#if UNITY_EDITOR
        if (UIManager.Instance.es.currentSelectedGameObject == null ||
            (UIManager.Instance.es.currentSelectedGameObject != null && UIManager.Instance.es.currentSelectedGameObject.layer != 5))
        {
                if (Input.GetMouseButtonDown(0))
                {
                    OnDragStart(Input.mousePosition);
                }
                if (Input.GetMouseButton(0))
                {
                    OnDrag(Input.mousePosition);
                }
                if (Input.GetMouseButtonUp(0))
                {
                    OnDragEnd();
                }
        }
#elif UNITY_ANDROID || UNITY_IOS
        if (UIManager.Instance.es.currentSelectedGameObject == null ||
            (UIManager.Instance.es.currentSelectedGameObject != null && UIManager.Instance.es.currentSelectedGameObject.layer != 5))
        {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Began)
                    {
                        OnDragStart(touch.position);
                    }
                    if (touch.phase == TouchPhase.Moved)
                    {
                        OnDrag(touch.position);
                    }

                    if (touch.phase == TouchPhase.Ended)
                    {
                        OnDragEnd();
                    }
                }
        }
#endif
    }
    void OnDragStart(Vector3 position)
    {
        Vector3 temp = Camera.main.ScreenToViewportPoint(position);
        startPoint = new Vector3(temp.x, 0.5f, temp.y);
    }
    void OnDrag(Vector3 position)
    {
        Vector3 temp = Camera.main.ScreenToViewportPoint(position);
        endPoint = new Vector3(temp.x, 0.5f, temp.y);
        distance = Vector3.Distance(startPoint, endPoint);
        direction = (startPoint - endPoint).normalized;
        force = direction * distance;
        forceDrawer.Draw(force.normalized,force.magnitude);
    }
    void OnDragEnd()
    {
        ball.Push(force);
        forceDrawer.Hide();
    }
}
