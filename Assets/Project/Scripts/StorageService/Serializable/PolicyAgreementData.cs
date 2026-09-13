using System;
using UnityEngine.Scripting;

[Serializable]
[Preserve]
public class PolicyAgreementData : SaveData
{
    public const int CurrentVersion = 1;

    public int AcceptedPolicyVersion;
}
