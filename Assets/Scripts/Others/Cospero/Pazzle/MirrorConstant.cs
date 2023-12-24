using UnityEngine;

public class MirrorConstant : MonoBehaviour
{
    [SerializeField] private PartNumber _partNumber;
    public PartNumber GetPartNumber() => _partNumber;

    public bool Completed = false;
}
