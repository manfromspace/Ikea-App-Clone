using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class DataHandler : MonoBehaviour
{
    private  GameObject furniture;

   [SerializeField]private ButtonManager buttonPrefab;
   [SerializeField]private GameObject buttonContainer;
   [SerializeField] private List<Item> itmes;

    private int current_id = 0;

    private static DataHandler instance;
    public static DataHandler Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DataHandler>();
            }
            return instance;
        }
    }

    private void Start()
    {
        LoadItmes();
        CreateButtons();
    }

    void LoadItmes()
    {
        var items_obj = Resources.LoadAll("Items", typeof(Item));
        foreach (var item in items_obj)
        {
            itmes.Add(item as Item);
        }

    }

    void CreateButtons()
    {
        foreach (Item i in itmes)
        {
            ButtonManager b = Instantiate(buttonPrefab, buttonContainer.transform);
            b.ItemId = current_id;
            b.ButtonTexture = i.itemImage;
            current_id++;
        }
    }
    public void SetFurniture(int id)
    {
        furniture = itmes[id].itemPrefab;

    }
    public GameObject GetFurniture()
    {
        return furniture;
    }


}
