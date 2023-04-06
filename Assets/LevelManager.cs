using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public struct DataSoal
    {
        public string pertanyaan;
        public Sprite petunjukJawaban;

        public string[] pilihanJawaban;
        public bool[] adalahbenar;
    }

    [SerializeField]
    private DataSoal[] _soalsoal = new DataSoal[0];

    [SerializeField]
    private UI_Pertanyaan _pertanyaan = null;

    [SerializeField]
    private UI_PoinJawaban[] _pilihanJawaban = new UI_PoinJawaban[0];

    private int _indexSoal = -1;

    private void Start()
    {
        NextLevel();
    }

    public void NextLevel()
    {
        //soal index selanjutnya
        _indexSoal++;

        //jika index melampaui soal terakhir, ulang dari awal
        if (_indexSoal >= _soalsoal.Length)
        {
            _indexSoal = 0;
        }

        //ambil data pertanyaan
        DataSoal Soal = _soalsoal[_indexSoal];

        //set informasi soal
        _pertanyaan.SetPertanyaan($"Quiz {_indexSoal + 1}", 
            Soal.pertanyaan, Soal.petunjukJawaban);

        for (int i = 0; i < _pilihanJawaban.Length; i++)
        {
            UI_PoinJawaban poin = _pilihanJawaban[i];
            poin.SetJawaban(Soal.pilihanJawaban[i], Soal.adalahbenar[i]);
        }
    }
}
