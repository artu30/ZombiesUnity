using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon{

    // Metodo que solo llevaran las armas para disparar.
    int shoot(Vector3 dir);

    // Metodo para devolver la municion del arma en cuestion
    int getAmmo();

    // Metodo que lanza el arma
    void throwWeapon(Vector3 dir);
}
