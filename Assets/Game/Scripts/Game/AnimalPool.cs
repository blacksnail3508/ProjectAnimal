using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalPool : MonoBehaviour
{
    [SerializeField] Animal animalPrefab;

    List<Animal> animals = new List<Animal>();
    private void Awake()
    {
        GameServices.AnimalPool=this;
    }

    public Animal GetAnimal()
    {
        //reuse
        foreach (var animal in animals)
        {
            if(animal.gameObject.activeSelf == false) return animal;
        }

        //create new
        var newAnimal = Instantiate(animalPrefab, transform);
        animals.Add(newAnimal);

        return newAnimal;
    }
    public void ReleaseAll()
    {
        if (animals.Count<=0) return;
        foreach(var animal in animals)
        {
            animal.ReturnPool();
        }
    }
}
