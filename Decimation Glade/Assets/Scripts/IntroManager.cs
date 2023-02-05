using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private List<PopupUI> _introImages = null;
    [SerializeField] private TextMeshProUGUI _introText = null;
    [SerializeField] private List<float> _durations = new List<float>();

    private void Awake()
    {
        StartCoroutine(Cutscene());
    }

    private IEnumerator Cutscene()
    {
        for (int i = 0; i < _introImages.Count; i++)
        {
            if (i - 1 >= 0)
            {
                _introImages[i - 1].ToggleUI(false);
            }

            _introImages[i].ToggleUI(true);

            yield return new WaitForSeconds(_durations[i]);
        }
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
