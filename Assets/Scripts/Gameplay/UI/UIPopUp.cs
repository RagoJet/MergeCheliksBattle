using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class UIPopUp : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Button OkBtn;

        private Canvas _canvas;

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _canvas.enabled = false;
            OkBtn.onClick.AddListener(OnOkBtnClicked);
        }

        public void Show(string info)
        {
            _canvas.enabled = true;
            text.SetText(info);
        }

        private void OnOkBtnClicked()
        {
            Destroy(gameObject);
        }
    }
}