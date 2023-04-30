using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_OpsiLevelPack : MonoBehaviour
{
    public static event System.Action<UI_OpsiLevelPack, LevelPackKuis, bool> EventSaatKlik;

    [SerializeField]
    private Button _tombol = null;

    [SerializeField]
    private TextMeshProUGUI _packName = null;

    [SerializeField]
    private LevelPackKuis _levelPack = null;

    [SerializeField]
    private TextMeshProUGUI _laberTerkunci = null;

    [SerializeField]
    private TextMeshProUGUI _laberHarga = null;

    [SerializeField]
    private bool _terkunci = false;

    private void Start()
    {
        if (_levelPack != null)
            SetLevelPack(_levelPack);

        //Subscribe Event
        _tombol.onClick.AddListener(SaatKlik);
    }

    private void OnDestroy()
    {
        //Unsubscribe Event 
        _tombol.onClick.RemoveListener(SaatKlik);
    }

    public void SetLevelPack(LevelPackKuis levelPack)
    {
        _packName.text = levelPack.name;
        _levelPack = levelPack;
    }

    private void SaatKlik()
    {
        //Debug.Log("Klik!!");
        EventSaatKlik?.Invoke(this, _levelPack, _terkunci);
    }

    public void KunciLevelPack()
    {
        _terkunci = true;
        _laberTerkunci.gameObject.SetActive(true);
        _laberHarga.transform.parent.gameObject.SetActive(true);
        _laberHarga.text = $"{_levelPack.Harga}";
    }

    public void BukaLevelPack()
    {
        _terkunci = false;
        _laberTerkunci.gameObject.SetActive(false);
        _laberHarga.transform.parent.gameObject.SetActive(false);
    }

}