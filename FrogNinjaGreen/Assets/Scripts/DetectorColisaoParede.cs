using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorColisaoParede : MonoBehaviour
{
    [SerializeField]
    private Transform pontoColisaoParedeEsquerda;

    [SerializeField]
    private Transform pontoColisaoParedeDireita;

    [SerializeField]
    private Vector2 areaColisao;

    [SerializeField] 
    private LayerMask layerMaskColisao;
    private void OnDrawGizmos()
    {
        if (this.pontoColisaoParedeEsquerda != null)
        {
            Gizmos.DrawWireCube(this.pontoColisaoParedeEsquerda.position, this.areaColisao);
        }
        if (this.pontoColisaoParedeDireita != null)
        {
            Gizmos.DrawWireCube(this.pontoColisaoParedeDireita.position, this.areaColisao);
        }
    }

    public bool EstaColidindoComParedeEsquerda()
    {
        return EstaColidindo(this.pontoColisaoParedeEsquerda.position);
    }

    public bool EstaColidindoComParedeDireita()
    {
        return EstaColidindo(this.pontoColisaoParedeDireita.position);
    }

    private bool EstaColidindo(Vector2 posicaoDetectarColisao)
    {
        Collider2D colisor = Physics2D.OverlapBox(posicaoDetectarColisao, this.areaColisao, 0, this.layerMaskColisao);
        if (colisor != null)
        {
            // Debug.Log("COLIDIU NO"+colisor.name);
            return true;
        }
        return false;
    }

    //falhas na morte do jogador ao cair do FallingPlatform? é  pq vc estava ainda em contato com a plataforma e te impediu de se movimentar nos espinhos(um bug, pois era pra morrer no instante do contato com ele)
}