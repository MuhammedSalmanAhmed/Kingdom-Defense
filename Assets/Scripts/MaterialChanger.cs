using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class NewBehaviourScript : MonoBehaviour
{
    public Material CorrectMat;
    public Material WrongMat;
    private MeshRenderer m_meshRenderer;
    public string TargerGameObjectTag = "Wall";
    public string UnWantedGameObjectTag = "Ground";

    // Removed dependency on DragAndShoot
    // private DragAndShoot m_dragAndShoot;

    // Start is called before the first frame update
    private void Start()
    {
        m_meshRenderer = GetComponent<MeshRenderer>();
        // m_dragAndShoot = GetComponent<DragAndShoot>();
    }

    private void OnCollisionEnter(Collision other)
    {
        // Removed dependency check
        // if (!m_dragAndShoot.IsShoot())
        // {
        //     return;
        // }

        if (other.gameObject.name == TargerGameObjectTag)
        {
            m_meshRenderer.material = CorrectMat;
        }
        else if (other.gameObject.tag == UnWantedGameObjectTag)
        {
            m_meshRenderer.material = WrongMat;
        }
    }
}
