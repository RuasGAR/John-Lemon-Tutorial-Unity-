using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    public GameEnding gameEnding;
    bool m_IsPlayerInRange;

    void OnTriggerEnter(Collider other)
    {
        if(other.transform == player)
        {
            m_IsPlayerInRange = true;   
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.transform == player)
        {
            m_IsPlayerInRange = false;
        }
    }

    void Update()
    {
        if(m_IsPlayerInRange)
        {
            //Simples opera��o de vetor pra medir a dire��o entre o jogador e a gorgoyle.
            //O Vector3.up � para levantar a posi��o do John para o seu centro de massa, tornando-o assim
            //vis�vel na mesma altura que as lanternas das g�rgulas.
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray (transform.position, direction);
            RaycastHit raycastHit;
            
            if (Physics.Raycast(ray, out raycastHit))
            {
                if(raycastHit.collider.transform == player)
                {
                    gameEnding.CaughtPlayer();
                }
            }
        }
    }
}
