using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private InisialDataGameplay _inisialData = null;

    [SerializeField]
    private PlayerProgress _playerProgress = null;

    [SerializeField]
    private LevelPackKuis _soalsoal = null;

    [SerializeField]
    private UI_Pertanyaan _pertanyaan = null;

    [SerializeField]
    private UI_PoinJawaban[] _pilihanJawaban = new UI_PoinJawaban[0];

    [SerializeField]
    private GameSceneManager _gameSceneManager = null;

    [SerializeField]
    private string _namaScenePilihMenu = string.Empty;

    [SerializeField]
    private PemanggilSuara _pemanggilSuara = null;

    [SerializeField]
    private AudioClip _suaraMenang = null;

    [SerializeField]
    private AudioClip _suaraKalah = null;

    private int _indexSoal = -1;

    private void Start()
    {

        _soalsoal = _inisialData.levelPack;
        _indexSoal = _inisialData.levelIndex - 1;

        NextLevel();
        AudioManager.instance.PlayBGM(1);

        //Subscribe Event
        UI_PoinJawaban.EventJawabSoal += UI_PoinJawaban_EventJawabSoal;
    }

    private void OnDestroy()
    {
        //Unsubscribe Event
        UI_PoinJawaban.EventJawabSoal -= UI_PoinJawaban_EventJawabSoal;
    }

    private void OnApplicationQuit()
    {
        _inisialData.SaatKalah = false;
    }

    private void UI_PoinJawaban_EventJawabSoal(string jawaban, bool adalahBenar)
    {
        _pemanggilSuara.PanggilSuara(adalahBenar ? _suaraMenang : _suaraKalah);

        //Cek jika tidak benar, maka abaikan prosedur
        if (!adalahBenar) return;

        string namaLevelPack = _inisialData.levelPack.name;
        int levelTerakhir = _playerProgress.progresData.progresLevel[namaLevelPack];

        //Cek apabila level terakhir kali main telah diselesaikan
        if (_indexSoal + 2 > levelTerakhir)
        {
            //Tambahkan koin sebagai hadian setelah menyelesaikan kuis
            _playerProgress.progresData.koin += 20;

            //Membuka level selanjutnya agar dapat diakses di menu level
            _playerProgress.progresData.progresLevel[namaLevelPack] = _indexSoal + 2;
            _playerProgress.SimpanProgres();
        }
    }

    public void NextLevel()
    {
        //soal index selanjutnya
        _indexSoal++;

        //jika index melampaui soal terakhir, ulang dari awal
        if (_indexSoal >= _soalsoal.BanyakLevel)
        {
            //_indexSoal = 0;
            _gameSceneManager.BukaScene(_namaScenePilihMenu);
            return;
        }

        //ambil data pertanyaan
        LevelSoalKuis Soal = _soalsoal.AmbilLevelKe(_indexSoal);

        //set informasi soal
        _pertanyaan.SetPertanyaan($"Soal {_indexSoal + 1}",
            Soal.pertanyaan, Soal.petunjukJawaban);

        for (int i = 0; i < _pilihanJawaban.Length; i++)
        {
            UI_PoinJawaban poin = _pilihanJawaban[i];
            LevelSoalKuis.OpsiJawaban opsi = Soal.opsiJawaban[i];
            poin.SetJawaban(opsi.jawabanText, opsi.adalahBenar);
        }
    }
}