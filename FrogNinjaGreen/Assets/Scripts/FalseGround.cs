using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FalseGround : MonoBehaviour
{
    public Tilemap tilemap; // Arraste o Tilemap para cá

    // public TileBase tileChaoFalso; // Arraste o tile do chão falso para cá
    public float tempoDesmoronamento = 1f; // Tempo em segundos para o chão desmoronar
    public int raioDesmoronamento = 2; // Raio da área que desmorona

    //me explique como pegas as referencias do "Tilemap tilemap" e do "TileBase tileChaoFalso". eu não entendi essa parte, por que eu separei um tilemap de chão normal e outro que seria o falso. como devo fazer na engine unity isso?
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        // Obtém a posição da célula do Tilemap onde o jogador está
        Vector3Int cellPosition = tilemap.WorldToCell(transform.position);
        // Debug.Log("Posição do Jogador no Tilemap: " + cellPosition);
        // Verifica se o tile naquela posição é o chão falso
        //   if (tilemap.GetTile(cellPosition) == tileChaoFalso)
        if (tilemap.GetTile(cellPosition) == tilemap)
        {
            // Debug.LogError("Tilemap não está configurado!");
            return;
        }

        // if (tilemap.GetTile(cellPosition) == tilemap)
        // {
        //     Debug.Log("CHÃO FALSO!");
        //     StartCoroutine(DesmoronarChao());
        // }

        if (IsTouchingTilemap())
        {
            StartCoroutine(DesmoronarChao());
        }
    }

    bool IsTouchingTilemap()
    {
        // Obtém a posição da célula do Tilemap onde o jogador está
        Vector3Int cellPosition = tilemap.WorldToCell(transform.position);

        // Verifica se há um tile naquela posição
        return tilemap.GetTile(cellPosition) != null;
    }

    IEnumerator DesmoronarChao()
    {
        Debug.Log("VAI CAIR!!");
        float tempoDecorrido = 0;
        while (tempoDecorrido < tempoDesmoronamento)
        {
            // Obtém a posição da célula do Tilemap onde o jogador está
            Vector3Int cellPosition = tilemap.WorldToCell(transform.position);

            // Remove tiles em um raio ao redor do jogador
            for (int x = -raioDesmoronamento; x <= raioDesmoronamento; x++)
            {
                for (int y = -raioDesmoronamento; y <= raioDesmoronamento; y++)
                {
                    Vector3Int pos = cellPosition + new Vector3Int(x, y, 0);
                    // if (tilemap.GetTile(pos) == tileChaoFalso)
                    if (tilemap.GetTile(pos) == tilemap)
                    {
                        // Debug.Log("Removendo tile na posição: " + pos);
                        tilemap.SetTile(pos, null);
                    }
                }
            }

            tempoDecorrido += Time.deltaTime;
            yield return null;
        }
    }
}
