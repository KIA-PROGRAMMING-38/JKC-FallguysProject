using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigsViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find( "ConfigsView" ).GetComponent<ConfigsView>();
        Debug.Assert( View != null );
        Presenter = new ConfigsPresenter();
        Debug.Assert( Presenter != null );
    }
}
