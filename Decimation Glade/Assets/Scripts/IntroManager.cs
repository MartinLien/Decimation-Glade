using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private PopupUI _introImage = null;
    [SerializeField] private TextMeshProUGUI _introText = null;
    
    private IEnumerator Cutscene()
    {


        yield return null;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
