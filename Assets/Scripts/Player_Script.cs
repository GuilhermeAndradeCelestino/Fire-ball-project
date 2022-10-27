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
    public static bool overCharge;
    bool oneTimeOverChange;
    bool charging;
    bool usingPowerBar;
    public static bool oneTimeChanging;

    //Barra de poder Cooldown
    public Image cooldownUi;
    public Image cooldownUiPronto;
    float cooldownValue;
    public static bool isInCooldown;
    bool oneTimeCooldown;

    [Space]
    [Space]

    //Movimentação
    public float speedInicial;
    float multiplicador;
    

    //Vidas
    public Image[] hudsLife;
    int life;
    bool levouDano;

    //Pontuação
    Vector3 posicaoInicial;
    float pontos;
    public TextMeshProUGUI pontosText;

    [Space]
    [Space]
    //Invunerabilidade
    public GameObject playerModel;
    bool isInvisible;



    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        life = hudsLife.Length - 1;
        posicaoInicial = transform.position;
        oneTimeCooldown = true;
        oneTimeOverChange = true;
        oneTimeChanging = true;
        cooldownValue = 1;
        overCharge = false;
        isInvisible = false;
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
            if (power <= 11)
            {
                power += 0.1f;

            }
            else if( power >= 11)
            {
                overCharge = true;
                oneTimeOverChange=true;
            }
           
        }
    }


    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DamageWall" && !isInvisible)
        {
            levouDano = true;
            StartCoroutine(Ivencibilidade(3, 0.3f));
            isInvisible = true;
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

        //OverChange é uma mecanica que faz com que a bola seja lançada a força caso o jogador carregue ao maximo a barra
        //porem essa bola não vai ter força nenhuma, se o jogador queiser atingir a força maxima ele precisa carregar
        //e soltar no momento certo

        if (!isInCooldown)
        {
            if (!overCharge)
            {
                if (Input.GetKey(tecla) && !FireBall_Script.onAnimation && oneTimeChanging)
                {
                    atirando = true;
                    charging = true;
                    usingPowerBar = true;
                    
                }
                else if (Input.GetKeyUp(tecla) && !FireBall_Script.onAnimation)
                {
                    if(oneTimeChanging == true)
                    {
                        charging = false;
                        if (power > 10 && power < 11)
                        {
                            FireBall_Script.atkPower = 10;
                        }
                        else
                        {
                            FireBall_Script.atkPower = power;
                        }
                        print("Ataquei");
                        Volcano_Script.canSpawn = true;
                        oneTimeChanging = false;
                    }
                }
                else
                {
                    power = 0;
                    usingPowerBar = false;
                    
                }
            }
            else if (overCharge && oneTimeOverChange)
            {
                charging = false;
                power = 0;
                usingPowerBar = false;
                FireBall_Script.atkPower = 0.1f;
                print("Ataquei com overchange");
                Volcano_Script.canSpawn = true;
                oneTimeOverChange = false;
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
        if(!isInCooldown)
        {
            cooldownUiPronto.enabled = true;
        }
        else if(isInCooldown)
        {
            cooldownUiPronto.enabled = false;
        }
    }


 
    //Animação de invunerabilidade temporaria ao tomar dano, numeroPiscadas define quantas vezes o modelo do player vai desaparecer e
    //reaparecer. duracaoPiscada define o tempo que o modelo vai ficar desligado
    IEnumerator Ivencibilidade(int numeroPiscadas, float duracaoPiscada)
    {        
        for(int i = 1; i < numeroPiscadas+1; i++)
        {
            yield return new WaitForSeconds(duracaoPiscada);
            playerModel.SetActive(false);
            yield return new WaitForSeconds(duracaoPiscada);
            playerModel.SetActive(true);
        }
        isInvisible = false;
    }

}


