using UnityEngine;

public class ExplodeTRex : MonoBehaviour
{
    public Rigidbody[] tRexBones;
    public float explosionForce = 500f;
    public float explosionRadius = 5f;
    public int scoreExplosion = 20000;
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
            AddScoreToMainCamera();
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

    void AddScoreToMainCamera()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            ScorringManager scoreManager = mainCamera.GetComponent<ScorringManager>();
            if (scoreManager != null)
            {
                scoreManager.AddScore(scoreExplosion);
            }
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
