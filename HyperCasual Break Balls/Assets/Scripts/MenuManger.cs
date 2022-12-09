using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Threading.Tasks;
using DG.Tweening;

public class MenuManger : MonoBehaviour
{
    #region Singleton
    public static MenuManger instance;

    private void Awake()
    {
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
    AudioManager audioManager;

    [Header("UI Section")]
    public Button startBt;
    public List<Button> interactableBts;

    public Button musicBt;
    public AudioMixer musicMixer;
    public Button sfxBt;
    public AudioMixer sfxMixer;
    public Button graphicsBt;

    public Image fadeImg;



    public Transform mainCam, menuCam;
    
    

    private void Start()
    {
        audioManager = AudioManager.instance;

        startBt.onClick.AddListener(() => { FadeScene(SceneManager.GetActiveScene().buildIndex + 1); });

        foreach (Button interactableBt in interactableBts)
        {
            interactableBt.onClick.AddListener(() => { audioManager.Play("Click"); });
        }

        musicBt.onClick.AddListener(() => { OptionsPanel("Music", musicBt.transform); });
        sfxBt.onClick.AddListener(() => { OptionsPanel("SFX", sfxBt.transform); });
        graphicsBt.onClick.AddListener(() => { OptionsPanel("Graphics", graphicsBt.transform); });
    }
    public void SmoothLevelTransition(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void FadeScene(int sceneIndex)
    {
        //pannig
        menuCam.transform.DOMove(mainCam.position, 2f).OnComplete(() =>
        {
            //mainCam.gameObject.SetActive(true);
            // mainCanvas.gameObject.SetActive(true);
            // menuCam.gameObject.SetActive(false);
            //player.GetComponent<CharacterController>().enabled = true;
            SmoothLevelTransition(sceneIndex);
        });
        menuCam.transform.DOLocalRotate(new Vector3(0f, 90f, 0f), 2f);
        fadeImg.DOFade(0.7f, 1f).SetEase(Ease.Linear);
        //await WaitForSomeTime(0.5f);
        
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
    public async Task WaitForSomeTime(float duration)
    {
        float end = Time.time + duration;
        while (Time.time < end)
        {
            await Task.Yield();
        }
    }
}
