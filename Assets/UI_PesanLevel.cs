using TMPro;
using UnityEngine;

public class UI_PesanLevel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _tempatPesan = null;

    public string Pesan
    {
        get => _tempatPesan.text;
        set => _tempatPesan.text = value;
    }
    private void Awake()
    {
        //untuk mematikan halaman pesan level
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
    }
}
