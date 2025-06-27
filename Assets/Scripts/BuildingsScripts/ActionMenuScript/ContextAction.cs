using System;
using UnityEngine;

public class ContextAction
{
    public string label; // Nome del tasto da visualizzare nel menu contestuale
    public Action<GameObject> onClick; // Azione da eseguire quando il tasto viene cliccato

    public ContextAction(string label, Action<GameObject> onClick)
    {
        this.label = label;       // Assegna il testo da mostrare nel bottone
        this.onClick = onClick;   // Assegna la funzione da eseguire al click
    }
}
