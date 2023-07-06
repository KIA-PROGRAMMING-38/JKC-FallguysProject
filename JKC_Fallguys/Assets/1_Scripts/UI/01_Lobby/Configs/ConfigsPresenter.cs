using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigsPresenter : Presenter
{
    private ConfigsView _configsView;
    public override void OnInitialize(View view)
    {
        _configsView = view as ConfigsView;

        InitializeRx();
    }

    protected override void OnOccuredUserEvent()
    {
    }

    protected override void OnUpdatedModel()
    {
        // 활성화 설정

    }

    public override void OnRelease()
    {
    }
}
