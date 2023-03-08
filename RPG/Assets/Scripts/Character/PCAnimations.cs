using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCAnimations : MonoBehaviour
{
    //introducimos el nombre de los layers en el inspector
    [SerializeField] private string layerIdle;
    [SerializeField] private string layerWalk;
    [SerializeField] private string layerAttack;

    private Animator animator;
    private PCMovement pcMovement;
    private CharacterAttack characterAttack;

    //Declaramos estos hash para no hardcodear el id de la posicion X e Y en los metodos que los requiramos.
    private readonly int directionX = Animator.StringToHash("X");
    private readonly int directionY = Animator.StringToHash("Y");

    //Hash para derrotado del animator
    private readonly int defeat = Animator.StringToHash("Defeat");

    //Obtenemos antes del inicio de la ejecución el componente animator y PCMovement instanciados en este contexto.
    private void Awake()
    { 
        animator = GetComponent<Animator>();
        pcMovement = GetComponent<PCMovement>();
        characterAttack = GetComponent<CharacterAttack>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLayers();

        if(pcMovement.isMoving)
        {
            animator.SetFloat(directionX, pcMovement.MovementDirection.x);
            animator.SetFloat(directionY, pcMovement.MovementDirection.y);
        }
    }

    // Pone todos los layers a 0 y activa el layer que necesitamos.
    private void ActivateLayer(string layerName)
    {
        for(int i=0; i < animator.layerCount; i++)
        {
            animator.SetLayerWeight(i, 0);
        }

        animator.SetLayerWeight(animator.GetLayerIndex(layerName), 1);
    }

    //activa layerwalk o layerIdle en funcion de si esta quieto o en movimiento
    private void UpdateLayers()
    {
        if(characterAttack.Attacking)
        {
            ActivateLayer(layerAttack);
        }
        else if(pcMovement.isMoving)
        {
            ActivateLayer(layerWalk);
        }
        else
        {
            ActivateLayer(layerIdle);
        }
    }

    //Respuesta salta el evento de defeat. Comprueba que esta en la layer correcta y poner el bool defeat a true
    //con lo cual, en el animator pasará a realizar la animación del personaje derrotado.
    private void DefeatedCharacterResponse()
    {
        if(animator.GetLayerWeight(animator.GetLayerIndex(layerIdle)) == 1)
        {
            animator.SetBool(defeat, true);
        }
    }

    //El metodo activa el layer Idle y la transición se hace de Defeat a Idle Tree.
    public void ReviveAnimation()
    {
        ActivateLayer(layerIdle);
        animator.SetBool(defeat, false);
    }

    //Se suscribe al evento
    private void OnEnable()
    {
        CharacterHealth.DefeatedCharacterEvent += DefeatedCharacterResponse;
    }

    //Se desuscribe al evento
    private void OnDisable()
    {
        CharacterHealth.DefeatedCharacterEvent -= DefeatedCharacterResponse;
    }
}
