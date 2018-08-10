using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticPathFinder : MonoBehaviour
{
    #region public_variables
    public float creatureSpeed;
    public float pathMulitplier;
    #endregion

    #region private_variables
    private DNA dna;
    private bool hasFinished = false;
    private bool hasBeenInitialized = false;
    private Vector2 target;
    private Vector2 nextPoint;
    private int pathIndex = 0;
    #endregion

    private void Start()
    {
        InitCreature(new DNA(), Vector2.zero);
    }
    public void InitCreature(DNA newDna, Vector2 _target)
    {
        dna = newDna;
        target = _target;
        nextPoint = transform.position;
        hasBeenInitialized = true;
    }

    private void Update()
    {
        if (hasBeenInitialized)
        {
            if(pathIndex == dna.genes.Count && !hasFinished)
            {
                End();
            }
            if((Vector2)transform.position == nextPoint)
            {
                nextPoint = (Vector2)transform.position + dna.genes[pathIndex];
                pathIndex++;
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, nextPoint, creatureSpeed * Time.deltaTime);
            }
        }
    }

    private void End()
    {
        hasFinished = true;
    }
}
