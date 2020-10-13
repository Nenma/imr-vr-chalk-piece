using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ChalkPieceBehaviour : MonoBehaviour  
{
    private bool isGrabbed = false;
    public GameObject lineRendererPrefab;
    private LineRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<VRTK_InteractableObject>() == null)
        {
            Debug.LogError("Not grabbable!");
            return;
        }

        GetComponent<VRTK_InteractableObject>().InteractableObjectUsed += ChalkPieceBehavior_InteractableObjectUsed;
        GetComponent<VRTK_InteractableObject>().InteractableObjectUnused += ChalkPieceBehavior_InteractableObjectUnused;
    }

    private void ChalkPieceBehavior_InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
    {
        Debug.Log("Grabbed!" + transform.position);
        isGrabbed = true;

        GameObject lineRendererInstance = Instantiate(lineRendererPrefab, transform);
        renderer = lineRendererInstance.GetComponent<LineRenderer>();
        renderer.positionCount = 0;
    }

    private void ChalkPieceBehavior_InteractableObjectUnused(object sender, InteractableObjectEventArgs e)
    {
        Debug.Log("Ungrabbed!" + transform.position);
        isGrabbed = false;
    }

    void FixedUpdate()
    {
        if (isGrabbed)
        {
            renderer.positionCount++;
            renderer.SetPosition(renderer.positionCount - 1, transform.position);
        }
    }
}
