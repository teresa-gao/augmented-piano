using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play : MonoBehaviour
{
    [SerializeField]
    TextAsset _noteSheet;
    [SerializeField]
    float _quarter;
    [SerializeField]
    float _noteVelocity;

    [SerializeField]
    GameObject _panel;

    [SerializeField]
    Transform _noteOrigin;
    [SerializeField]
    Transform _noteTarget;

    [SerializeField]
    GameObject _note;

    [SerializeField]
    Color _leftHand;
    [SerializeField]
    Color _rightHand;

    float _lastUpdate;
    string[] _lines;
    int _currentNote;

    public static bool isPlaying;
    public static string pianoPiece;

    void Awake()
    { 
        SetPianoPiece("Ode to Joy");

        string txt = _noteSheet.text;
        _lines = txt.Split('\n');

        _currentNote = 0;

        isPlaying = false;
        _lastUpdate = Time.time;
    }

    private void Update()
    {
        if (isPlaying)
        {
            SetVelocity();

            if (Time.time - _lastUpdate >= _quarter)
            {
                var line = _lines[_currentNote];
                _currentNote++;
                _currentNote %= _lines.Length;

                string[] notes = line.Split(' ');

                Debug.Log("Notes: " + notes[0] + " " + notes[1]);

                if (notes[0][0] != '-')
                {
                    // Right hand
                    Transform octave = transform.Find(notes[0][0].ToString());

                    Transform key = octave.Find(notes[0][1].ToString());

                    Vector3[] positions1 = {
                        new Vector3(key.position.x, _noteOrigin.transform.position.y, _noteOrigin.transform.position.z),
                        new Vector3(key.position.x, _noteOrigin.transform.position.y, _noteTarget.transform.position.z)
                    };

                    var note1 = Instantiate(_note, Vector3.zero, Quaternion.identity, transform.root.gameObject.transform);
                    note1.GetComponent<LineRenderer>().SetPositions(positions1);
                    note1.GetComponent<Fall>()._noteTarget = new Vector3(key.position.x, _noteTarget.transform.position.y, _noteTarget.transform.position.z);
                    note1.GetComponent<Fall>()._noteOrigin = new Vector3(key.position.x, _noteOrigin.transform.position.y, _noteOrigin.transform.position.z);
                    note1.GetComponent<LineRenderer>().material.color = _rightHand;
                }

                if (notes[1][0] != '-')
                { 
                    // Left Hand
                    Transform octave = transform.Find(notes[1][0].ToString());

                    Transform key = octave.Find(notes[1][1].ToString());

                    Vector3[] positions2 = {
                        new Vector3(key.position.x, _noteOrigin.transform.position.y, _noteOrigin.transform.position.z),
                        new Vector3(key.position.x, _noteOrigin.transform.position.y, _noteTarget.transform.position.z)
                    };

                    var note2 = Instantiate(_note, Vector3.zero, Quaternion.identity, transform.root.gameObject.transform);
                    note2.GetComponent<LineRenderer>().SetPositions(positions2);
                    note2.GetComponent<Fall>()._noteTarget = new Vector3(key.position.x, _noteTarget.transform.position.y, _noteTarget.transform.position.z);
                    note2.GetComponent<Fall>()._noteOrigin = new Vector3(key.position.x, _noteOrigin.transform.position.y, _noteOrigin.transform.position.z);
                    note2.GetComponent<LineRenderer>().material.color = _leftHand;
                }

                _lastUpdate = Time.time;
            }
        }
    }

    public void StartNotes(int firstNote = 0)
    {
        isPlaying = false;
        _currentNote = firstNote;
    }

    public void SetPianoPiece(string piece)
    {
        pianoPiece = piece;
        _noteSheet = (TextAsset)Resources.Load(pianoPiece);
    }

    public void SetVelocity()
    {
        _note.GetComponent<Fall>()._velocity = 1 / _noteVelocity;
        _quarter = 1 / 4.0f / _noteVelocity;
    }

    public void StartPlaying()
    {
        isPlaying = true;
    }

    public void PausePlaying()
    {
        isPlaying = false;
    }
}
