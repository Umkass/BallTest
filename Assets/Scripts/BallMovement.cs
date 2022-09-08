using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] GameObject ballModel;
    [SerializeField] ParticleSystem psDustPrefab;
    SphereCollider ballCollider;
    MeshRenderer ballRenderer;
    Vector3 force;
    float pushForce = 20;
    LayerMask layerMask = 1 << 6;
    bool pushed = false;

    void Awake()
    {
        ballRenderer = ballModel.GetComponent<MeshRenderer>();
        ballCollider = GetComponent<SphereCollider>();
    }
    public void Push(Vector3 force)
    {
        this.force = force * pushForce;
        pushed = true;
    }

    void Update()
    {
        if (pushed)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + force.x, transform.position.y + force.y, transform.position.z + force.z), Time.deltaTime); //move ball
            ballModel.transform.Rotate(force * 60 * Time.deltaTime); //rotation ball
            force = force - (force * Time.deltaTime / 2); //force damping
        }
    }

    void ForceReflecion(Vector3 force, float distance, out Vector3 forceReflection) //reflects the force vector if it hits a wall
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, force, out hit, distance, layerMask);
        Debug.DrawRay(transform.position, force, Color.blue);
        if (hit.collider != null)
        {
            forceReflection = Vector3.Reflect(force, hit.normal);
        }
        else
        {
            forceReflection = Vector3.zero;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Vector3 newForce;
            if (force.magnitude >= 2f)
            {
                Vector3 hitDirection = collision.contacts[0].point - transform.position;
                Instantiate(psDustPrefab, collision.contacts[0].point - hitDirection / 3, Quaternion.LookRotation(hitDirection));
                StartCoroutine(HitBallCoroutine(hitDirection));
            }
            ForceReflecion(force, 300, out newForce);
            force = newForce;
        }
    }

    IEnumerator HitBallCoroutine(Vector3 hitDirection)
    {
        transform.rotation = Quaternion.LookRotation(hitDirection);
        transform.position += hitDirection / 3;
        ballCollider.radius = 0.35f;
        transform.localScale = new Vector3(1f, 1f, 0.7f); //ball squeeze
        ballRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        ballRenderer.material.color = Color.white;
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;
        ballCollider.radius = 0.5f;
    }
}
