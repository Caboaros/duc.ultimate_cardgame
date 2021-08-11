using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    // Textura

    public Material tendencyMaterial;

    // Singleton

    public static Energy Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    // Enum que define qual tipo de energia será afetada

    public Type energyType;

    // Energia dos dois lados

    public int YangMeter = 50;
    public int YinMeter = 50;

    // Variáveis secundárias, que pegam os valores de atribuição e custo para formar o valor final e realizar operações

    public int attributeAmount = 1;
    public int costAmount = 0;

    // Método que pega o custo de todas as cartas em campo e os adiciona ao valor de atribuição

    public void SetAttribute()
    {
        foreach (Zones.Zone zone in Zones.Instance.zoneList[(int)Sides.Instance.current].zoneObj)
        {
            // Checando se a zona está vazia
            if (zone.zoneState != ZoneState.Empty)
            {
                // Pegando os pontos de custo da carta em zona



                // = EFEITOS =
                if (zone.cardOnZone.cardInfo.cardEffect != null)
                {
                    // Efeito "Attribute Raise" que aumenta o valor ganho
                    if (zone.cardOnZone.cardInfo.cardEffect.effect == EffectType.AttributeRaise)
                    {
                        // Somando o custo original ao valor atribuido no efeito

                        int tmpRaise = zone.cardOnZone.cardInfo.cost + zone.cardOnZone.cardInfo.cardEffect.amount;

                        attributeAmount += tmpRaise;
                    }
                }

                // Sem Efeitos

                else
                {
                    attributeAmount += zone.cardOnZone.cardInfo.cost;
                }
            }
        }
    }

    // Método que pega o custo da carta selecionada (Fica exposta no CardManager) e o adiciona à variável de custo
    // (Método usado no botão de invocação)
    public void SetCost()
    {
        // =EFEITOS= 

        // Efeito "NoCost" (Obviamente elimina o custo)
        
        if (CardManager.Instance.selectedCard.GetComponent<CardScript>().cardScriptable.cardInfo.cardEffect != null)
        {
            if (CardManager.Instance.selectedCard.GetComponent<CardScript>().cardScriptable.cardInfo.cardEffect.effect == EffectType.NoCost)
            {
                costAmount = 0;
            }

            else
            {
                costAmount += CardManager.Instance.selectedCard.GetComponent<CardScript>().cardScriptable.cardInfo.cost;
            }
        }
        
        // Sem Efeitos

        else
        {
            costAmount += CardManager.Instance.selectedCard.GetComponent<CardScript>().cardScriptable.cardInfo.cost;
        }
        

    }

    // Método usado no evento de atribuição para chamar o método que muda os valores de energia na fase de Manutenção
    // O método pega a variável "attributeAmount" que tem seu valor construído em "SetAttribute" e faz a operação com o mesmo
    public void AttributePoints()
    {
        // Mudando apenas energia com jogador Yin por hora
        ChangeEnergy(attributeAmount, (Type)Sides.Instance.current);
    }

    // Método usado ao invocar cartas para usar energia ao invocar cartas
    public void SubtractCost()
    {   
        // Efeitos

        ChangeEnergy(-costAmount, (Type)Sides.Instance.current);
    }

    public void ChangeEnergy(int amount, Type energyType)
    {
        // Textura
        Vector2 textureScale = tendencyMaterial.mainTextureScale;
        Debug.Log(textureScale.y);

        if (energyType == Type.Yin)
        {
            YinMeter += amount;
            YangMeter -= amount;

            // Textura
            tendencyMaterial.mainTextureScale.y.Equals(textureScale.y + (amount / 10));
        }

        else
        {
            YangMeter += amount;
            YinMeter -= amount;

            // Textura
            tendencyMaterial.mainTextureScale.y.Equals(textureScale.y - (amount / 10));
        }
    }

    // Retornando os valores originais às variáveis

    public void ResetSecondaries()
    {
        attributeAmount = 1;
        costAmount = 0;
    }

}

public enum Type
{
    Yin = 0,
    Yang = 1,
}
