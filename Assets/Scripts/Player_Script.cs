using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player_Script : MonoBehaviour
{

    Rigidbody rb;

    //ataque
    public bool usarSpace;
    public static bool atirando;


    [Space]
    [Space]

    //Barra de poder
    public float power;
    public GameObject powerUi;
    public Animator powerUiAnim;

    //Barra de poder Cooldown
    public Image cooldownUi;
    float cooldownValue;
    public static bool isInCooldown;
    bool oneTimeCooldown;

    [Space]
    [Space]

    //Movimentação
    public float speedInicial;
    float multiplicador;
    

    //Limitadores
    bool charging;
    bool usingPowerBar;
    

    //Vidas
    public Image[] hudsLife;
    int life;
    bool levouDano;

    //Pontuação
    Vector3 posicaoInicial;
    float pontos;
    public TextMeshProUGUI pontosText;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        life = hudsLife.Length - 1;
        posicaoInicial = transform.position;
        oneTimeCooldown = true;
        cooldownValue = 1;
    }



    // Start is called before the first frame update
    void Start()
    {
        power = 0;
        multiplicador = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Pontuacao();

        CooldownAtk();


        if (usarSpace)
        {
            PlayerInput(KeyCode.Space);
        }
        else
        {
            PlayerInput(KeyCode.Mouse0);
        }

        
    }




    private void FixedUpdate()
    {
        if (!atirando)
        {
            Movimentacao();
            
        }
        else
        {
            rb.Sleep();
        }

        UpdateUI();
        
        if (charging)
        {
            if (power <= 10)
            {
                power += 0.1f;

            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "BreakWall")
        {
            levouDano = true;
        }
    }

    void CooldownAtk()
    {
        //Checa se esta em cooldown aparti da variavel isInCooldown, oneTimeCooldown serve apenas para evitar que o 
        //o codigo fique sendo executado mias de uma vez
        if (isInCooldown && oneTimeCooldown)
        {
            cooldownValue = 0;
            StartCoroutine(CooldownValui());
            oneTimeCooldown = false;
        }

    }

    IEnumerator CooldownValui()
    {
        if (cooldownValue == 0)
        {
            //Tempo da animação de tacar a bola
            yield return new WaitForSeconds(2f);
        }

        yield return new WaitForSeconds(0.3f);
        cooldownValue += 0.1f;
        if (cooldownValue < 1)
        {
            StartCoroutine(CooldownValui());
        }
        else //if(cooldownValue == 1)
        {
            isInCooldown = false;
            oneTimeCooldown = true;
        }

    }

    void PlayerInput(KeyCode tecla)
    {
        //quando o jogador clikar e segurar = começa a carregar
        //ao soltar o mouse indica para a bola a força de arremeço e que ela pode ser lançada
        //depois disso zera o poder
        if (!isInCooldown)
        {
            if (Input.GetKey(tecla))
            {
                atirando = true;
                charging = true;
                usingPowerBar = true;
            }
            else if (Input.GetKeyUp(tecla))
            {
                charging = false;
                FireBall_Script.atkPower = power;
                print("Ataquei");
                Volcano_Script.canSpawn = true;
            }
            else
            {
                power = 0;
                usingPowerBar = false;
            }

        }



    }

    void Movimentacao()
    {
        //Movimentação do jogador , aumenta conforme o multiplicador de dificuladade
        //rb.AddForce(new Vector3(0, 0, 1) * speedInicial, ForceMode.Impulse);
        float speedFinal = speedInicial * multiplicador;
        rb.velocity = new Vector3(0, 0, 1) * speedFinal * Time.fixedDeltaTime;
    }


    void Pontuacao()
    {
        //calcula os pontos baseado na distancia
        pontos = Vector3.Distance(posicaoInicial, transform.position);
        Mathf.Abs(pontos);
    }



    void UpdateUI()
    {
        //atualiza a ui da barra de poder
        if (usingPowerBar)
        {
            powerUiAnim.SetBool("Aparecer", true);
            powerUiAnim.SetTrigger("Comecar");
        }
        else if (!usingPowerBar)
        {
            powerUiAnim.SetBool("Aparecer", false);
        }
        powerUi.GetComponent<Slider>().value = power;


        //Atualiza a hud da vida
        if (levouDano == true)
        {
            hudsLife[life].color = new Color(0.2830189f, 0, 0, 1);
            life--;
            levouDano = false;
        }

        //Atualiza a ui da pontuação
        pontosText.text = Mathf.Round(pontos).ToString();

        //Atualiza a ui do cooldown
        cooldownUi.fillAmount = cooldownValue;
    }




}


