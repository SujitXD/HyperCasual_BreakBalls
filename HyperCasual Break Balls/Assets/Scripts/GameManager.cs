using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        playerPrefsManager = PlayerPrefsManager.instance;
        if (RestartLV)
        {
            MainMenu.instance.LvButton(0);
        }
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    #endregion
    public static bool RestartLV;
    PlayerPrefsManager playerPrefsManager;
    AudioManager audioManager;
    CharacterController characterController;

    public string currentBG = "Main Menu";
    public int startingBallCount = 30;
    int currentBallCount;

    public int readyLevel;

    public List<GameObject> ballCountImgs;

    [System.Serializable]
    public class Powerups
    {
        public string powerupTag;
        public List<Button> powerUpBt;
        public Image powerUpFill;
        public float powerUpTime;
    }

    [Header("Powerup Section")]
    public Transform powerupUI;

    public List<Powerups> powerups;
    bool isUsingPowerup;

    [Header("Time Settings")]
    public float slowdownFactor = 0.05f;
    public float slowdownLength = 2f;
    float currentTimeScale;

    [Header("UI Section")]
    public List<Button> interactableBts;

    public Image fadeImg;

    public TextMeshProUGUI ballTxt;
    public Image ballCountFillImg;
    public GameObject screenEffect;
    public GameObject gameEndUI;

    public Transform nextPoint;
    float totalDistance;
    public Image progressUI;

    public GameObject gameEndScore, gameEndNewScore;
    public GameObject inGameHighScore;

    public Button musicBt;
    public Button startBt;
    public Button sfxBt;
    public AudioMixer sfxMixer;
    public Button graphicsBt;
    public AudioMixer musicMixer;

    public List<GameObject> checkPoints;

    public GameObject particleEffect;



    private void OnApplicationQuit()
    {
        // playerPrefsManager.SetLvStart(0);
        RestartLV = false;
    }

    private void Start()
    {
        //ObjectPooler.instance.SpawnFromPool("Intro Stage", Vector3.zero, Quaternion.identity, transform,false, true);
        //ObjectPooler.instance.SpawnFromPool("Checkpoint 1", Vector3.zero, Quaternion.identity, transform,false, true);

        GameObject cp1 = Instantiate(Resources.Load("Checkpoints/Intro Stage")) as GameObject;
        GameObject cp2 = Instantiate(Resources.Load("Checkpoints/Checkpoint 1")) as GameObject;

        checkPoints.Add(cp1);
        checkPoints.Add(cp2);

        if (PlayerPrefs.HasKey("currentLv"))
        {
            readyLevel = PlayerPrefs.GetInt("currentLv");
        }


        
        audioManager = AudioManager.instance;
        characterController = CharacterController.instance;

        currentBallCount = startingBallCount;
        ballTxt.text = "x" + currentBallCount.ToString();
        currentTimeScale = Time.timeScale;

        if (nextPoint != null)
        {
            totalDistance = nextPoint.position.z - characterController.transform.position.z;
        }

        if (playerPrefsManager.GetCurrentScore() > 0)
        {
            inGameHighScore.SetActive(true);
            inGameHighScore.transform.position += new Vector3(0f, 0f, playerPrefsManager.GetCurrentScore() - 99.465f);
        }

        foreach (Button interactableBt in interactableBts)
        {
            interactableBt.onClick.AddListener(() => { audioManager.Play("Click"); });
        }



        audioManager = AudioManager.instance;

        //startBt.onClick.AddListener(() => { FadeScene(SceneManager.GetActiveScene().buildIndex + 1); });

        foreach (Button interactableBt in interactableBts)
        {
            interactableBt.onClick.AddListener(() => { audioManager.Play("Click"); });
        }

        musicBt.onClick.AddListener(() => { OptionsPanel("Music", musicBt.transform); });
        sfxBt.onClick.AddListener(() => { OptionsPanel("SFX", sfxBt.transform); });
        graphicsBt.onClick.AddListener(() => { OptionsPanel("Graphics", graphicsBt.transform); });
    }

    private void Update()
    {
        foreach (Powerups powerup in powerups)
        {
            foreach (Button bt in powerup.powerUpBt)
            {
                if (!isUsingPowerup)
                    bt.onClick.AddListener(() => { PowerUp(powerup, powerup.powerupTag, bt); });
            }
        }

        ProgressBar();
    }
    public void OptionsPanel(string btName, Transform optionBt)
    {
        for (int i = 0; i < optionBt.childCount; i++)
        {
            if (optionBt.GetChild(i).gameObject.activeSelf)
            {
                if (i == optionBt.childCount - 1)
                {
                    optionBt.GetChild(i).gameObject.SetActive(false);
                    optionBt.GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    optionBt.GetChild(i).gameObject.SetActive(false);
                    optionBt.GetChild(i + 1).gameObject.SetActive(true);
                }

                break;
            }
        }

        if (btName.Contains("Music"))
        {
            if (optionBt.GetChild(0).gameObject.activeSelf)
                musicMixer.SetFloat("Volume", -80f);
            else if (optionBt.GetChild(1).gameObject.activeSelf)
                musicMixer.SetFloat("Volume", -20f);
            else if (optionBt.GetChild(2).gameObject.activeSelf)
                musicMixer.SetFloat("Volume", 0f);
            else if (optionBt.GetChild(3).gameObject.activeSelf)
                musicMixer.SetFloat("Volume", 20f);
        }

        if (btName.Contains("SFX"))
        {
            if (optionBt.GetChild(0).gameObject.activeSelf)
                sfxMixer.SetFloat("Volume", -80f);
            else if (optionBt.GetChild(1).gameObject.activeSelf)
                sfxMixer.SetFloat("Volume", -20f);
            else if (optionBt.GetChild(2).gameObject.activeSelf)
                sfxMixer.SetFloat("Volume", -0f);
            else if (optionBt.GetChild(3).gameObject.activeSelf)
                sfxMixer.SetFloat("Volume", 20f);
        }

        if (btName.Contains("Graphics"))
        {
            int qualityIndex = QualitySettings.GetQualityLevel();

            if (qualityIndex == 2)
                QualitySettings.SetQualityLevel(0);
            else
                QualitySettings.SetQualityLevel(qualityIndex + 1);
        }
    }
    private void ProgressBar()
    {
        progressUI.fillAmount = (characterController.transform.position.z + 99.465f) / totalDistance;     
    }

    public void SetNextPoint(Transform _nextPoint)
    {
        nextPoint = _nextPoint;
        totalDistance = nextPoint.position.z - characterController.transform.position.z;
        progressUI.fillAmount = 0f;
    }

    private async void PowerUp(Powerups _powerup, string powerUp, Button bt)
    {
        if (isUsingPowerup)
            return;

        isUsingPowerup = true;

        float xOffset = bt.transform.localPosition.x + 100f;
        float yOffset = bt.transform.localPosition.y + 100f;

        AlignPowerupUI();

        _powerup.powerUpFill.fillAmount = 1f;
        _powerup.powerUpFill.DOFillAmount(0f, _powerup.powerUpTime).SetEase(Ease.Linear);

        switch (powerUp)
        {
            case "MachineBalls":

                bt.transform.DOLocalMoveX(xOffset, 0.5f).SetEase(Ease.Linear);
                bt.transform.DOLocalMoveY(yOffset, 0.5f).SetEase(Ease.Linear);
                bt.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f).SetEase(Ease.Linear);

                bt.GetComponent<Image>().DOFade(0f, 0.5f).SetEase(Ease.Linear).OnComplete(() => {
                    bt.transform.SetParent(ObjectPooler.instance.transform);
                    bt.gameObject.SetActive(false);
                });

                ScreenEffect(Color.green, _powerup.powerUpTime);

                characterController.MachineBalls(true);
                await WaitForSomeTime(_powerup.powerUpTime);
                characterController.MachineBalls(false);

                break;
            case "FireBalls":

                bt.transform.DOLocalMoveX(xOffset, 0.5f).SetEase(Ease.Linear);
                bt.transform.DOLocalMoveY(yOffset, 0.5f).SetEase(Ease.Linear);
                bt.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f).SetEase(Ease.Linear);

                bt.GetComponent<Image>().DOFade(0f, 0.5f).SetEase(Ease.Linear).OnComplete(() => {
                    bt.transform.SetParent(ObjectPooler.instance.transform);
                    bt.gameObject.SetActive(false);
                });

                ScreenEffect(new Color(1f, 0.51f, 0f), _powerup.powerUpTime);

                characterController.FireBalls(true);
                await WaitForSomeTime(_powerup.powerUpTime);
                characterController.FireBalls(false);

                break;
            case "SlowMotion":

                bt.transform.DOLocalMoveX(xOffset, 0.03f).SetEase(Ease.Linear);
                bt.transform.DOLocalMoveY(yOffset, 0.03f).SetEase(Ease.Linear);
                bt.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.03f).SetEase(Ease.Linear);

                bt.GetComponent<Image>().DOFade(0f, 0.03f).SetEase(Ease.Linear).OnComplete(() => {
                    bt.transform.SetParent(ObjectPooler.instance.transform);
                    bt.gameObject.SetActive(false);
                });

                ScreenEffect(new Color(1f, 0f, 0.87f), _powerup.powerUpTime);

                DoSlowmotion();
                await WaitForSomeTime(_powerup.powerUpTime);
                currentTimeScale = 1f;
                Time.timeScale = currentTimeScale;

                break;
            default:
                break;
        }

        isUsingPowerup = false;
    }

    public void AlignPowerupUI()
    {
        for (int i = 0; i < powerupUI.childCount; i++)
        {
            if (i == powerupUI.childCount - 1)
            {
                powerupUI.GetChild(i).GetComponent<RectTransform>().localPosition = new Vector2(0f, -330f);

                powerupUI.GetChild(i).GetComponent<RectTransform>().sizeDelta = new Vector2(145f, 145f);
                powerupUI.GetChild(i).GetComponent<Button>().interactable = true;
            }
            else
            {
                powerupUI.GetChild(i).GetComponent<RectTransform>().localPosition = new Vector2(0f, -330f + (150f * (powerupUI.childCount - (i + 1))));

                powerupUI.GetChild(i).GetComponent<RectTransform>().sizeDelta = new Vector2(130f, 130f);
                powerupUI.GetChild(i).GetComponent<Button>().interactable = false;
            }
        }
    }
    
    private void ControlTime()
    {
        Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }

    public void DoSlowmotion()
    {
        currentTimeScale = slowdownFactor;
        Time.timeScale = currentTimeScale;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }

    public async void ManageBallCount(int amount)
    {
        if (amount < 0)
            amount = -amount <= currentBallCount ? amount : -currentBallCount;

        currentBallCount += amount;

        ballTxt.text = "x" + currentBallCount.ToString();

        if (amount > 0)
        {
            if (characterController.ballSpawnCount < 5)
            {
                ballCountFillImg.fillAmount += 1f / 10f;

                if (ballCountFillImg.fillAmount >= 1f)
                {
                    characterController.ballSpawnCount++;
                    ballCountFillImg.fillAmount = 0f;

                    for (int i = 0; i < ballCountImgs.Count; i++)
                    {
                        if (i == characterController.ballSpawnCount - 1)
                        {
                            ballCountImgs[i].SetActive(true);
                        }
                        else
                        {
                            ballCountImgs[i].SetActive(false);
                        }
                    }
                }
            }

            Transform incrementUI = ObjectPooler.instance.SpawnFromPool("IncrementCountUI", Vector3.zero, Quaternion.identity, ballTxt.transform).transform;
            
            incrementUI.localPosition = Vector3.zero;
            incrementUI.localScale = Vector3.one;
            //incrementUI.GetComponent<TextMeshProUGUI>().text = amount.ToString();

            incrementUI.DOLocalMoveX(150f, 1f).SetEase(Ease.Linear).OnComplete(() => {
                incrementUI.gameObject.SetActive(false);
                incrementUI.SetParent(ObjectPooler.instance.transform);
            });
        }

        if (currentBallCount == 0)
        {
            audioManager.Play("Fail");

            characterController.transform.gameObject.SetActive(false);
            characterController.forwardPlayerSpeed = 0f;
            characterController.enabled = false;
            gameEndUI.SetActive(true);

            // transition

            int playerScore = (int)(characterController.transform.position.z + 99.465f);

            if (playerPrefsManager.GetCurrentScore() == 0) 
            {
                playerPrefsManager.SetCurrentScore(playerScore);
                gameEndScore.SetActive(true);
                gameEndScore.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = playerScore.ToString();
            }
            else
            {
                if(playerScore <= playerPrefsManager.GetCurrentScore())
                {
                    gameEndScore.SetActive(true);
                    gameEndScore.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = playerScore.ToString();

                    
                }
                else
                {
                    playerPrefsManager.SetCurrentScore(playerScore);
                    gameEndNewScore.SetActive(true);
                    gameEndNewScore.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = playerScore.ToString();

                    await WaitForSomeTime(1f);
                    
                    RestartGame();

                    ResumeGame();
                    SceneManager.LoadScene(0);
                    // AudioManager.instance.Play("Rewind"); 
                    AudioManager.instance.SmoothPlay(currentBG, "Main Menu");
                    characterController.enabled = false;
                }
            }

            //Invoke(nameof(RestartGame), 2f);
        }
    }

    public void RestartGame()
    {
        //StartCoroutine(LoadYourAsyncScene());
        AudioSource[] audioSources = audioManager.GetComponents<AudioSource>();
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.Stop();
        }

        audioManager.SmoothPlay(GameManager.instance.currentBG, "MainMenu");
        //audioManager.Play("MainMenu");

        SceneManager.LoadScene(0);
    }
    IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(0);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public int GetCurrentBallCount()
    {
        return currentBallCount;
    }
    public async Task WaitForSomeTime(float duration)
    {
        float end = Time.time + duration;
        while (Time.time < end)
        {
            await Task.Yield();
        }
    }
    public void PauseGame()
    {
        //audioManager.Pause(currentBG);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        //audioManager.Play(currentBG);
        Time.timeScale = currentTimeScale;
    }
    public void SmoothLevelTransition(int index)
    {
        SceneManager.LoadScene(index);
    }

    public async void FadeScene(int sceneIndex)
    {
        fadeImg.DOFade(1f, 1f).SetEase(Ease.Linear);
        await WaitForSomeTime(0.5f);
        SmoothLevelTransition(sceneIndex);
    }
    public async void ScreenEffect(Color color, float duration = 0.5f)
    {
        screenEffect.SetActive(true);
        screenEffect.GetComponent<Image>().color = color;
        await WaitForSomeTime(duration);
        screenEffect.SetActive(false);
    }
}
