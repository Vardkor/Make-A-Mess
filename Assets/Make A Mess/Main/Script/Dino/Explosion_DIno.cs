using UnityEngine;

public class ExplodeTRex : MonoBehaviour
{
    public Rigidbody[] tRexBones; // Liste des morceaux du T-Rex
    public float explosionForce = 500f; // Force de l'explosion
    public float explosionRadius = 5f; // Rayon de l'explosion
    private bool isPlayerNear = false;
    private bool hasExploded = false;

    void Start()
    {
        foreach (Rigidbody bone in tRexBones)
        {
            bone.useGravity = false;
            bone.isKinematic = true;
        }
    }

    void Update()
    {
        if (isPlayerNear && !hasExploded)
        {
            hasExploded = true;
            TriggerExplosion();
        }
    }

    void TriggerExplosion()
    {
        foreach (Rigidbody bone in tRexBones)
        {
            bone.isKinematic = false;
            bone.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            bone.useGravity = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isPlayerNear = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isPlayerNear = false;
    }
}
