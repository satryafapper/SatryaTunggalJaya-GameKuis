using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class UI_Pertanyaan : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _tempatJudul = null;

    [SerializeField]
    private TextMeshProUGUI _tempatText = null;
    
    [SerializeField]
    private Image _tempatGambar = null;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(_tempatText.text);
    }

    public void SetPertanyaan(string judul, string textPertanyaan, Sprite gambarHint)
    {
        _tempatJudul.text = judul;
        _tempatText.text = textPertanyaan;
        _tempatGambar.sprite = gambarHint;
    }
}
