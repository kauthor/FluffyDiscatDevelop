using System;
using FluffyDisket.UI;
using Tables.Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FluffyDisket
{
    public class LobbyUnit:IUnit
    {
        [SerializeField] private Job job;

        [SerializeField] private int id;
        //[SerializeField] private PlayerSubTable table;

        private Vector3 destination;

        private float speed = 0.5f;

        private void Awake()
        {
            destination = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-3.0f, 3.0f));
        }

        private void Update()
        {
            if (Vector3.SqrMagnitude(destination - transform.position) > 0.2f)
            {
                var position = transform.position;
                position = position +
                           (destination - position).normalized * speed * Time.deltaTime;
                transform.position = position;
            }
            else
            {
                destination = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-3.0f, 3.0f));
            }
        }

        private void OnMouseDown()
        {
            UIHeroInfoPopup.OpenPopup(job, id);
        }
    }
}