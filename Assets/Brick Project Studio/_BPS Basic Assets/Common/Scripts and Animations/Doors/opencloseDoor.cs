using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles
{
    public class opencloseDoor : MonoBehaviour
    {
        public Animator openandclose;
        public bool open;
        public Transform Player;

        public string requiredKeyID = ""; // Identificador de la llave necesaria (déjalo vacío si no requiere llave)
        private bool requiresKey => !string.IsNullOrEmpty(requiredKeyID); // Verifica si la puerta necesita llave

        void Start()
        {
            open = false;
        }

        void OnMouseOver()
        {
            if (Player)
            {
                float dist = Vector3.Distance(Player.position, transform.position);
                if (dist < 15)
                {
                    if (Input.GetMouseButtonDown(0)) // Clic izquierdo
                    {
                        if (requiresKey)
                        {
                            // Verifica si el jugador tiene la llave requerida
                            if (DoorManager.instance.IsKeyCollected(requiredKeyID))
                            {
                                ToggleDoor();
                            }
                            else
                            {
                                Debug.Log("Necesitas la llave: " + requiredKeyID);
                            }
                        }
                        else
                        {
                            // Si no requiere llave, abre/cierra la puerta
                            ToggleDoor();
                        }
                    }
                }
            }
        }

        void ToggleDoor()
        {
            if (open == false)
            {
                StartCoroutine(opening());
            }
            else
            {
                StartCoroutine(closing());
            }
        }

        IEnumerator opening()
        {
            print("Abriendo la puerta");
            openandclose.Play("Opening");
            open = true;
            yield return new WaitForSeconds(.5f);
        }

        IEnumerator closing()
        {
            print("Cerrando la puerta");
            openandclose.Play("Closing");
            open = false;
            yield return new WaitForSeconds(.5f);
        }
    }
}
