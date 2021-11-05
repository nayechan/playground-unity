using System.Collections.Generic;

public class LibraryCardObserver{
    public List<LibraryCardOptionController> cardList;
    static LibraryCardObserver instance;

    LibraryCardObserver()
    {
        cardList = new List<LibraryCardOptionController>();
    }

    public static void RegisterCard(LibraryCardOptionController card)
    {
        if(instance == null)
        {
            instance = new LibraryCardObserver();
        }
        instance.cardList.Add(card);
    }

    public static void RemoveCard(LibraryCardOptionController card)
    {
        instance.cardList.Remove(card);
    }

    public static void OnClick(LibraryCardOptionController card)
    {
        foreach(LibraryCardOptionController _card in instance.cardList)
        {
            _card.DeactivateOption();
        }
        card.ActivateOption();
    }
}