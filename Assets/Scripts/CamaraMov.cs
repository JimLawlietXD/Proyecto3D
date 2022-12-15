using UnityEngine;

public class CamaraMov : MonoBehaviour
{
    //Variable para referenciar nuestro jugador
    public GameObject Player;
    //Variable para registrar la diferencia entre la posición de la cámara y la del jugador
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        //Diferencia entre la posición de la cámara y la del jugador
        offset = transform.position - Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Se ejecuta cada frame, pero después de haber procesado todo. Esto es más exacto
    //para la cámara
    private void LateUpdate()
    {
        //Actualiza la posición de la cámara
        transform.position = Player.transform.position + offset;
    }
}