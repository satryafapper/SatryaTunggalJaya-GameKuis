using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Timer : MonoBehaviour
{
    [SerializeField]
    private UI_PesanLevel _tempatPesan = null ;

    [SerializeField]
    private Slider _timeBar = null;

    [SerializeField]
    private float _waktuJawab = 30f;

    private float _siswaWaktu = 0f; //Data Sementara
    private bool _waktuBerjalan = true;

    public bool WaktuBerjalan
    {
        get => _waktuBerjalan;
        set => _waktuBerjalan = value;
    }

    private void Start()
    {
        UlangWAktu();
    }

    public void Update()
    {
        if (!_waktuBerjalan)
            return;

        _siswaWaktu -= Time.deltaTime;
        _timeBar.value = _siswaWaktu / _waktuJawab; 

        if (_siswaWaktu <= 0f)
        {
            _tempatPesan.Pesan = "Gagal! Waktu Habis!";
            _tempatPesan.gameObject.SetActive(true);

            //Debug.Log("Waktu Habis");
            _waktuBerjalan = false;
            return;
        }

        //Debug.Log(_siswaWaktu);
    }

    public void UlangWAktu()
    {
        _siswaWaktu = _waktuJawab;
    }


}
