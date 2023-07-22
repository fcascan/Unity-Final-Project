using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchosDamage : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si la colisi�n es con el personaje principal
        if (collision.gameObject.CompareTag("Player"))
        {
            // Obtener el componente FireWarriorController2D del personaje principal
            FireWarriorController2D controller = collision.gameObject.GetComponent<FireWarriorController2D>();

            // Verificar si se encontr� el componente FireWarriorController2D
            if (controller != null)
            {
                // Llamar al m�todo Kill del componente FireWarriorController2D
                controller.Kill();
            }
        }
        //Verificar colision con Enemigo
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            //Obtener componente Enemigo
            EnemyBehaviour controller = collision.gameObject.GetComponent<EnemyBehaviour>();

            // Verificar si se encontr� el componente FireWarriorController2D
            if (controller != null)
            {
                // Llamar al m�todo Kill del componente FireWarriorController2D
                controller.Kill();
            }
        }
    }
}
