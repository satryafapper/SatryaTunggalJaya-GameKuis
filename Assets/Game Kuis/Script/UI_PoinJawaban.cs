using TMPro;
using UnityEngine;

public class UI_PoinJawaban : MonoBehaviour
{
    public static event System.Action<string, bool> EventJawabSoal;

    //[SerializeField]
    //private UI_PesanLevel _tempatPesan = null;

    [SerializeField]
    private TextMeshProUGUI _textJawaban = null;

    [SerializeField]
    private bool _adalahBenar = false;

    public void PilihJawaban()
    {
        //_tempatPesan.Pesan = $"{_textJawaban.text} Adalah {_adalahBenar}";
        EventJawabSoal?.Invoke(_textJawaban.text, _adalahBenar);
    }

    public void SetJawaban(string textJawaban, bool adalahBenar)
    {
        _textJawaban.text = textJawaban;
        _adalahBenar = adalahBenar;
    }
}
