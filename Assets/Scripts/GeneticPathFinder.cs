using UnityEngine;

public class GeneticPathfinder : MonoBehaviour
{
    public float beesSpeed;
    public float pathMultiplier;
    public DNA dna;
    public bool hasFinished = false;

    private int pathIndex = 0;
    private bool hasBeenInitialized = false;
    private Vector2 target;
    private Vector2 nextPoint;

    public void InitCreature(DNA newDna, Vector2 _target)
    {
        dna = newDna;
        target = _target;
        nextPoint = transform.position;
        hasBeenInitialized = true;
    }

    public float GetFitness
    {
        get
        {
            float dist = Vector2.Distance(transform.position, target);
            if (dist == 0)
            {
                dist = 0.0001f;
            }
            return 60 / dist;
        }
    }

    private void Update()
    {
        if (hasBeenInitialized && !hasFinished)
        {
            if(pathIndex == dna.genes.Count || Vector2.Distance(transform.position, target) < 0.5f)
            {
                hasFinished = true;
            }
            if((Vector2)transform.position == nextPoint)
            {
                nextPoint = (Vector2)transform.position + dna.genes[pathIndex];
                pathIndex++;
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, nextPoint, beesSpeed * Time.deltaTime);
            }
        }
    }
}
