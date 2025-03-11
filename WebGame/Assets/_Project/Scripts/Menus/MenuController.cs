using UnityEngine.UI;
using UnityEngine;

namespace Platformer397
{
    public class MenuController : MonoBehaviour
    {

        [SerializeField] private Button playBtn;
        [SerializeField] private Button loadBtn;
        [SerializeField] private Button optionsBtn;
        [SerializeField] private Button quitBtn;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            playBtn.onClick.AddListener(() => SceneController.Instance.ChangeScene("Gameplay"));
        }

    }
}
