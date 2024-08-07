using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(Collider))]
public class Stick : MonoBehaviour
{

    public List<string> StruckObjectTags;
    /*private DragAndShoot m_dragAndShoot;

    private void Start()
    {
        m_dragAndShoot= GetComponent<DragAndShoot>();
    }*/

    private void OnCollisionEnter(Collision other)
    {
        /*if (!m_dragAndShoot.IsShoot())
        {
            return;
        }*/

        for (int i = 0; i < StruckObjectTags.Count; i++)
        {
            if (other.gameObject.CompareTag(StruckObjectTags[i]))
            {
                GetComponent<Rigidbody>().isKinematic = true;
                break;
            }
            
        }
    }
}
