using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorPSJump : MonoBehaviour
{
    [SerializeField]private List<PSAndSize> jumpPSs;
    [SerializeField] private PlayerNanoSetup setup;
    [SerializeField]private PlayerController controller;
    public struct PSAndSize
    {
        public float startSize;
        public ParticleSystem ps;

        public PSAndSize(float startSize, ParticleSystem ps)
        {
            this.startSize = startSize;
            this.ps = ps;
        }
    }
    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        ParticleSystem[] rangePS = GetComponentsInChildren<ParticleSystem>();
        setup = GetComponent<PlayerNanoSetup>();
        jumpPSs = new List<PSAndSize>();
        foreach (var ps in rangePS)
        {
            PSAndSize psize = new PSAndSize(ps.startSize, ps);
            jumpPSs.Add(psize);
        }
    }


    private void Update()
    {
        if(Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            jumpPSs.ForEach((ps) => 
            { if (setup.settuped)
                ps.ps.startSize = ps.startSize * 0.2f;
              else
                ps.ps.startSize = ps.startSize;
                ps.ps.Play();
            });
        }
    }
}
