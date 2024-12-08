using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public static DoorManager instance; // Singleton para acceso global

    private HashSet<string> collectedKeys = new HashSet<string>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Registrar una llave recogida
    public void RegisterKey(string keyID)
    {
        if (!collectedKeys.Contains(keyID))
        {
            collectedKeys.Add(keyID);
            Debug.Log("Llave registrada: " + keyID);
        }
    }

    // Validar si una llave fue recogida
    public bool IsKeyCollected(string keyID)
    {
        return collectedKeys.Contains(keyID);
    }
}
