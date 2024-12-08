using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public string keyID; // Identificador único de la llave

    private bool isCollected = false; // Controla si la llave ya fue recogida

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && !isCollected)
        {
            Debug.Log("Llave recogida: " + keyID);
            isCollected = true; // Marcar la llave como recogida
            DoorManager.instance.RegisterKey(keyID); // Notificar al sistema que la llave fue recogida
            Destroy(gameObject); // Destruir la llave
        }
    }
}
