using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEditor;
using Object = System.Object;

public class MovimientoPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    public float velocidad = 10;

/*----------------------------------------------------------------------------------------------------------*/
    private int contador_coleccionables = 0;    // Variable de Contador de objetos coleccionables ** 
     public Text textoContador, textoGanar;      //Texto para el contador y para ganar
    public GameObject Cronometro;               //Cronómetro del UI
/*----------------------------------------------------------------------------------------------------------*/
   
    void Start()
    {
        //--------------------------------------------------------------------------------------------------------
        Cronometro = GameObject.FindWithTag("TextoFinal");  //Usamos el Tag para mostrar en cronómetro TextoFinal
        TextoTiempo.instanciar.iniciarTiempo();
        //--------------------------------------------------------------------------------------------------------

        rb = GetComponent<Rigidbody>();    //Instanciamos el componente Rigidbody con la variable rb

        //Actualizar el texto del contador por primera vez
        setTextContador();
        textoGanar.text = "";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
  private void FixedUpdate()
    {
        //Estas variables no ayudan a capturar en movimiento horizontal y vertical de nuestro teclado
        float movimientoH = Input.GetAxis("Horizontal");
        float movimientoV = Input.GetAxis("Vertical");
        
        //Un vector 3 es un trio de posiciones en un espacio XYZ, y corresponde al movimiento del player
        Vector3 movimiento = new Vector3(movimientoH, 0.0f, movimientoV);   
        
        //Asignamos ese movimiento o desplazamiento a mi RigidBody "rb" / Se multiplica el movimiento por la velocidad
        rb.AddForce(movimiento*velocidad);

        // Contador 
        if (contador_coleccionables >= 20)
        {
            Cronometro.gameObject.SetActive(true);
        }
        else
        {
            Cronometro.gameObject.SetActive(false);
        }
    } 
/*------------------------------------------------------------------------------------------------------------------------------------*/
          //Este método se ejecuta al entrar a un objeto con la opción isTrigger seleccionada, nos permite contar los coleccionables
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coleccionable"))
        {
            other.gameObject.SetActive(false);
            //Incrementar el contador en 1, según detección de Trigger
            contador_coleccionables++;
            Debug.Log("Coleccionables recogidos: "+ contador_coleccionables);  //Mostramos por consola los colecionables recogidos
            setTextContador();   //Actualizar el texto del contador
        }

    }
        void setTextContador()
    {
        //Para encadenar o concatenar un texto con una variable, el texto va entre comillas y la variable se le agrega el signo +
        textoContador.text = "Contador: " + contador_coleccionables.ToString();

       
        if (contador_coleccionables == 7)
        {
            textoGanar.text = "Pasaste el Nivel";
            EliminarPuerta();
        }
        
        if (contador_coleccionables == 8)
        {
            textoGanar.text = "";
        }
        if (contador_coleccionables == 13)
        {
            EliminarPuerta2();
            textoGanar.text = "";
        }
        if (contador_coleccionables == 20)
        {
            FinalTiempo();
            textoGanar.text = "¡Has Ganado! ";
        }
    
    }
    void EliminarPuerta()    //Método para que desaparezcan las puertas
    {
        if (GameObject.FindGameObjectWithTag("Puerta"))
        {
            Destroy(GameObject.FindGameObjectWithTag("Puerta"));
            Destroy(GameObject.FindGameObjectWithTag("Puerta1"));
        }
    }
    void EliminarPuerta2()
    {
        if (GameObject.FindGameObjectWithTag("Puerta2"))
        {
            Destroy(GameObject.FindGameObjectWithTag("Puerta2"));
        }
    }
        void FinalTiempo()  //Método para instanciar el tiempo final al llegar a la cuenta todal de objetos coleccionados
    {
        if ((contador_coleccionables == 20))
        {
            TextoTiempo.instanciar.FinTiempo();
        }
    }
      private void OnCollisionEnter(Collision collision)  //Usamos un OnCollisionEnter normal - NO 2D
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            //Función para hacer que el personaje aparezca al inicio del nivel nuevamente  (Respawn)
            transform.position = new Vector3(x:-5.6464f, y:9.0774f, z:134.97f);
           
        }
    }
/*-----------------------------------------------------------------------------------------------------------------------------------*/    
}