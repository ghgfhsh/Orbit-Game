using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialOrbitMotion : MonoBehaviour
{
    public Transform orbitingObject;
    public Ellipse orbitPath;

    [Range(0f, 1f)]
    public float orbitProgress = 0f;
    [Range(60f, 10000f)]
    public float orbitPeriod = 3f;
    public bool orbitActive = true;

    private void Start()
    {
        if(orbitingObject == null)
        {
            orbitActive = false;
            return;
        }
        SetOrbitingObjectPosition();
        StartCoroutine(AnimateOrbit());
        
    }

    void SetOrbitingObjectPosition()
    {
        Vector2 orbitPos = orbitPath.Evaluate(orbitProgress);
        orbitingObject.localPosition = new Vector3(orbitPos.x, orbitPos.y, 0f);
    }

    IEnumerator AnimateOrbit()
    {
        if (orbitPeriod < 0.1f)
            orbitPeriod = 0.1f;

        float orbitSpeed = 1f / orbitPeriod;

        while (orbitActive)
        {
            orbitProgress += Time.deltaTime * orbitSpeed;
            orbitProgress %= 1f;
            SetOrbitingObjectPosition();
            yield return null;
        }
        yield return null;
    }

    private void OnValidate()
    {
        SetOrbitingObjectPosition();
    }
}
