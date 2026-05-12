using System.Linq;
using UnityEngine;

public class PadLockPassword : MonoBehaviour
{
    MoveRuller _moveRull;

    // كلمة السر
    public int[] _numberPassword = { 0, 0, 0, 0 };

    // الباب
    public GameObject door;
    // عشان ما يعيد الفتح كل فريم
    private bool isUnlocked = false;

    private void Awake()
    {
        _moveRull = FindObjectOfType<MoveRuller>();
    }

    public void Password()
    {
        // إذا انفتح الباب لا يعيد
        if (isUnlocked)
            return;

        // التحقق من كلمة السر
        if (_moveRull.numberArray.SequenceEqual(_numberPassword))
        {
            Debug.Log("Password correct");

            // يفتح الباب
            door.SetActive(false);

            isUnlocked = true;

            // يوقف الإضاءة
            for (int i = 0; i < _moveRull.rullers.Count; i++)
            {
                _moveRull.rullers[i]
                    .GetComponent<PadLockEmissionColor>()
                    ._isSelect = false;

                _moveRull.rullers[i]
                    .GetComponent<PadLockEmissionColor>()
                    .BlinkingMaterial();
            }
        }
    }
}