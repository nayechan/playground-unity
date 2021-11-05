using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDetailOperation : SandboxCardOnClickOperation
{
    [SerializeField] LibraryCardOptionController cardOption;
    public override void execute()
    {
        LibraryCardObserver.OnClick(cardOption);
    }
}
