using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainSceneUI : MonoBehaviour
{
    [SerializeField] private Button startBtn;
    [SerializeField] private Button settingBtn;
    [SerializeField] private GameObject settingPopup;

    private void Start()
    {
        startBtn.onClick.AddListener(GameSceneLoad);
        settingBtn.onClick.AddListener(SettingActive);
    }

    private void GameSceneLoad()
    {
        SceneManager.LoadScene("StageScene");
    }

    private void SettingActive()
    {
        settingPopup.SetActive(true);
    }
}
