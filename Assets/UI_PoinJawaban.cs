using TMPro;
using UnityEngine;

public class UI_PoinJawaban : MonoBehaviour
{
    [SerializeField]
    private UI_PesanLevel _tempatPesan = null;

    [SerializeField]
    private TextMeshProUGUI _textJawaban = null;

    [SerializeField]
    private bool _adalahBenar = false;

    public void PilihJawaban()
    {
        //Debug.Log($"{_textJawaban.text} Adalah {_adalahBenar}");
        _tempatPesan.Pesan = $"{_textJawaban.text} Adalah {_adalahBenar}";
    }

    public void SetJawaban(string textJawaban, bool adalahBenar)
    {
        _textJawaban.text = textJawaban;
        _adalahBenar = adalahBenar;
    }
}
