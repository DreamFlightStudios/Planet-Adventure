using UnityEngine;

[RequireComponent (typeof(BoxCollider))]
public class ArriveAtPlaceQuestTask : QuestTask
{
    private bool _isInside;

    public override void Activate()
    {
        base.Activate();

        if (_isInside && Data.IsActive)
            Complete();
    }

    private void OnTriggerEnter(Collider other)
    {
        _isInside = true;

        if (Data.IsActive)
            Complete();
    }

    private void OnTriggerExit(Collider other)
        => _isInside = false;
}