using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCMovement : MonoBehaviour
{
    private Rigidbody2D ninjaRigidBody;
    private Vector2 input; //para el input del movimiento
    private Vector2 movement;

    // Se utiliza el serializeField para que la variable privada se muestre en el inspector de Unity y podamos modificarla.
    [SerializeField]private float speed;
    
    //Getter
    public Vector2 MovementDirection => movement;
    public bool isMoving => movement.magnitude > 0f; //False si no se mueve, true si se mueve.

    //Lo declarado en Awake solo se ejecuta una sola vez durante la vida de la instancia del objeto.
    private void Awake()
    {
        ninjaRigidBody = GetComponent<Rigidbody2D>(); //Obtenemos el componente RigidBody asociado a Ninja
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Obtiene el valor de x, y segun la tecla pulsada (WASD).
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //Eje X
        //Si x es negativo, se mueve a la izquierda, si x es positivo se mueve a la derecha, si es 0 no se mueve en horizontal.
        if(input.x > 0.1f)
        {
            movement.x = 1f;
        }

        else if(input.x < 0f)

        {
            movement.x = -1f;
        }
        else
        {
            movement.x = 0f;
        }

        //Eje Y
        //Si y es negativo, se mueve hacia abajo, si y es positivo se mueve hacia arriba, si es 0 no se mueve en vertical.
        if (input.y > 0.1f)
        {
            movement.y= 1f;
        }

        else if (input.y < 0f)

        {
            movement.y = -1f;
        }
        else
        {
            movement.y = 0f;
        }
    }

    // Movement define si será positivo o negativo y si será en x o y el movimiento. Speed determina cuanto se desplaza cada vez (como la magnitud fisica)
    // y Time es un objeto que nos ayuda a acceder a metodos de conteo de tiempo.

    //Para las fisicas se utiliza FixedUpdate, ya que las actualizaciones de las interacciones del juego deben hacerse a un intervalo fijo, update
    //hace la actualizacion segun el framrate al que el ordenador es capaz de correr el juego.
    private void FixedUpdate()
    {
        ninjaRigidBody.MovePosition(ninjaRigidBody.position + movement * speed * Time.fixedDeltaTime);
    }


}
