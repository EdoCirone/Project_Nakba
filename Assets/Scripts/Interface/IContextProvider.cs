using System.Collections.Generic;
using UnityEngine;

public interface IContextProvider // Interfaccia per fornire azioni contestuali in un gioco
{
    List<ContextAction> GetContextActions(GameObject player); // Metodo per ottenere le azioni contestuali disponibili per un determinato giocatore
}
