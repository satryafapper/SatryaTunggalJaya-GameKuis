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

    private int _indexSoal = -1;

    private void Start()
    {
        //if (!_playerProgress.MuatProgres())
        //{
        //    _playerProgress.SimpanProgres(); 
        //}

        _soalsoal = _inisialData.levelPack;
        _indexSoal = _inisialData.levelIndex - 1;

        NextLevel();

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
        if (adalahBenar)
        {
            _playerProgress.progresData.koin += 20;
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
