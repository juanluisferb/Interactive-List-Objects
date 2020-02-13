using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMenuController : MonoBehaviour
{

    [SerializeField] GameObject _lvlObj;
    public List<GameObject> tag_targets = new List<GameObject>();


    //Cada objeto que tiene este script añade todos los objetos hijos que tiene por debajo a una lista
    private void Start()
    {
        foreach (Transform child in transform)
        {
            tag_targets.Add(child.gameObject);
        }

    }


    //Método que activa el objeto con los objetos hijo correspondiente a este botón
    private void EnableChildButtons()
    {
        if (_lvlObj.transform.childCount > 0)
        {
            _lvlObj.gameObject.SetActive(true);
        }
    }

    //Se lanza desactivar los objetos con tag "LVL" de la lista de objetos hijos del objeto que tenga este script
    private void DisableChildButtons()
    {
        DeleteTagItems(_lvlObj.transform, "LVL", tag_targets);
        _lvlObj.gameObject.SetActive(false);

    }


    //De forma recursiva se desactivan los objetos hijos de los hijos (si los hay) nivel a nivel
    private void DeleteTagItems(Transform parent, string tag, List<GameObject> list)
    {
        foreach (Transform child in parent)
        {
            if (child.gameObject.tag == tag)
            {
                child.gameObject.SetActive(false);
            }

            if (parent.childCount > 0)
            {
                DeleteTagItems(child, tag, list);
            }

        }
    }


    //Al pulsar el trigger del botón se activan o desactivan los botones si procede
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finger")
        {
            EnableChildButtons();
        }

        if (this.gameObject.tag == "CloseButton" && other.gameObject.tag == "Finger")
        {
            DisableChildButtons();
        }

    }
}
