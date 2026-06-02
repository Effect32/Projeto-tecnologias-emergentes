using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Gerador procedural do Complexo Trapiche Santo Ângelo - São Luís/MA
/// Compatível com Built-in RP, URP e HDRP.
/// Menu: Tools > Gerar Trapiche Santo Ângelo
/// </summary>
public class TrapiheGenerator : MonoBehaviour
{
#if UNITY_EDITOR

    // ════════════════════════════════════════════════════════════════
    // PALETA DE CORES DO COMPLEXO
    // ════════════════════════════════════════════════════════════════
    static readonly Color COR_PAREDE_BRANCA  = new Color(0.94f, 0.91f, 0.85f);
    static readonly Color COR_TELHA_VERMELHA = new Color(0.70f, 0.18f, 0.10f);
    static readonly Color COR_TIJOLO         = new Color(0.62f, 0.32f, 0.18f);
    static readonly Color COR_PISO_PRACA     = new Color(0.78f, 0.72f, 0.62f);
    static readonly Color COR_PISO_PATIO     = new Color(0.68f, 0.64f, 0.56f);
    static readonly Color COR_GRAMA          = new Color(0.25f, 0.52f, 0.20f);
    static readonly Color COR_CONCRETO       = new Color(0.56f, 0.56f, 0.54f);
    static readonly Color COR_JANELA_MADEIRA = new Color(0.32f, 0.18f, 0.08f);
    static readonly Color COR_TRONCO         = new Color(0.42f, 0.28f, 0.14f);
    static readonly Color COR_COPA_PALMEIRA  = new Color(0.18f, 0.52f, 0.18f);
    static readonly Color COR_COPA_ARVORE    = new Color(0.22f, 0.48f, 0.16f);
    static readonly Color COR_RUA            = new Color(0.38f, 0.38f, 0.36f);
    static readonly Color COR_BANCO          = new Color(0.50f, 0.20f, 0.10f);
    static readonly Color COR_GALPAO_CONC    = new Color(0.72f, 0.70f, 0.66f);

    [MenuItem("Tools/Gerar Trapiche Santo Angelo")]
    static void GenerateTrapiche()
    {
        GameObject existing = GameObject.Find("Trapiche_Santo_Angelo");
        if (existing != null) { DestroyImmediate(existing); }

        GameObject root = new GameObject("Trapiche_Santo_Angelo");
        root.transform.position = Vector3.zero;

        string shader = DetectarShader();
        Debug.Log($"[Trapiche] Pipeline detectado, usando shader: {shader}");

        // Materiais compartilhados
        Material mBranco   = Mat(shader, "Parede_Branca",  COR_PAREDE_BRANCA);
        Material mTelha    = Mat(shader, "Telha_Vermelha", COR_TELHA_VERMELHA);
        Material mTijolo   = Mat(shader, "Tijolo",         COR_TIJOLO);
        Material mPisoP    = Mat(shader, "Piso_Praca",     COR_PISO_PRACA);
        Material mPisoPat  = Mat(shader, "Piso_Patio",     COR_PISO_PATIO);
        Material mGrama    = Mat(shader, "Grama",          COR_GRAMA);
        Material mConc     = Mat(shader, "Concreto",       COR_CONCRETO);
        Material mJanela   = Mat(shader, "Janela_Madeira", COR_JANELA_MADEIRA);
        Material mTronco   = Mat(shader, "Tronco",         COR_TRONCO);
        Material mPalmeira = Mat(shader, "Copa_Palmeira",  COR_COPA_PALMEIRA);
        Material mArvore   = Mat(shader, "Copa_Arvore",    COR_COPA_ARVORE);
        Material mRua      = Mat(shader, "Rua",            COR_RUA);
        Material mBanco    = Mat(shader, "Banco",          COR_BANCO);
        Material mGalConc  = Mat(shader, "Galpao_Conc",   COR_GALPAO_CONC);

        // ── TERRENO ────────────────────────────────────────────────
        Cubo("Terreno_Grama",  root, V(  0f, -0.10f,   0f), V(170f, 0.20f, 130f), mGrama);
        Cubo("Rua",            root, V( -5f, -0.05f, -65f), V(150f, 0.10f,  20f), mRua);
        Cubo("Praca_Frontal",  root, V( -8f,  0.01f, -50f), V(130f, 0.06f,  20f), mPisoP);
        Cubo("Patio_Interno",  root, V(  8f,  0.01f,   2f), V( 55f, 0.06f,  45f), mPisoPat);

        // ── EDIFÍCIO PRINCIPAL (colonial, 2 andares) ───────────────
        Vector3 pM = V(-4f, 0f, -28f);
        Cubo("Principal_1Andar",  root, pM + V(0, 4.0f,  0f),   V(22f, 8.0f, 14f), mBranco);
        Cubo("Principal_2Andar",  root, pM + V(0, 9.5f,  0f),   V(20f, 3.0f, 12f), mBranco);
        Cubo("Principal_Varanda", root, pM + V(0, 8.3f, -7.4f), V(18f, 0.3f, 1.8f),mBranco);
        Telhado4Aguas("Telhado_Principal", root, pM + V(0, 11.5f, 0), 22f, 14f, 3.5f, mTelha);

        // Arcadas frontais
        for (int i = -1; i <= 1; i++)
            Cubo($"Arco_{i+2}", root, pM + V(i * 5f, 3.0f, -7.2f), V(3.2f, 6.0f, 0.3f), mJanela);

        // Óculo circular (frontão)
        var oculo = Prim("Oculo", root, pM + V(0, 12.5f, -7.1f), PrimitiveType.Cylinder, mBranco);
        oculo.transform.localScale  = V(3f, 0.15f, 3f);
        oculo.transform.eulerAngles = V(90f, 0f, 0f);

        // ── GALPÃO ESQUERDO FRONTAL (shed) ─────────────────────────
        GalpaoShed("Galpao_Esq_Frontal", root, V(-42f, 0f, -28f),
                   58f, 14f, 7f, 5, mBranco, mTelha);

        // ── GALPÃO ESQUERDO TRASEIRO ────────────────────────────────
        GalpaoShed("Galpao_Esq_Traseiro", root, V(-50f, 0f, 20f),
                   42f, 14f, 6f, 4, mBranco, mTelha);

        // ── CORPO CENTRAL ───────────────────────────────────────────
        Cubo("Corpo_Central",    root, V(-20f, 3.5f, -28f), V(14f, 7f, 14f), mBranco);
        Telhado2Aguas("Telhado_CC", root, V(-20f, 7.5f, -28f), 14f, 14f, 2.2f, mTelha);

        // ── GALPÃO DIREITO (concreto) ───────────────────────────────
        GalpaoShed("Galpao_Direito", root, V(40f, 0f, 8f),
                   22f, 14f, 6f, 3, mGalConc, mTelha);

        // ── EDIFÍCIO LATERAL DIREITO ────────────────────────────────
        Cubo("Edificio_Dir",       root, V(50f, 4.0f, -16f), V(18f, 8f, 16f), mGalConc);
        Telhado2Aguas("Telhado_ED",root, V(50f, 8.5f, -16f), 18f, 16f, 2.5f, mTelha);

        // ── CALDEIRAS ──────────────────────────────────────────────
        Cubo("Caldeiras",          root, V(22f, 3.5f, -8f), V(14f, 7f, 10f), mTijolo);
        Telhado2Aguas("Telhado_CA",root, V(22f, 7.5f, -8f), 14f, 10f, 2.0f, mTelha);

        // ── CHAMINÉS ───────────────────────────────────────────────
        Chamine("Chamine_1", root, V(22f, 0f, 4f), 30f, 1.4f, mTijolo);
        Chamine("Chamine_2", root, V(28f, 0f, 4f), 34f, 1.2f, mTijolo);
        Cubo("Passarela",    root, V(25f, 6.0f, 4f), V(8f, 0.5f, 2f), mConc);

        // ── MURO FRONTAL ────────────────────────────────────────────
        Cubo("Muro_Frontal", root, V(-8f, 0.75f, -42f), V(130f, 1.5f, 0.3f), mBranco);

        // ── PALMEIRAS (praça frontal) ───────────────────────────────
        float[] pxPalm = { -35f, -20f, -5f, 10f, 25f, 40f };
        for (int i = 0; i < pxPalm.Length; i++)
            Palmeira($"Palmeira_{i}", root, V(pxPalm[i], 0f, -46f), mTronco, mPalmeira);

        // ── ÁRVORES (pátio) ─────────────────────────────────────────
        var arvs = new (Vector3 p, float h)[]
        {
            (V( 5f, 0f,  -2f), 5.5f), (V(18f, 0f, 12f), 4.8f),
            (V(-2f, 0f,  18f), 5.0f), (V(12f, 0f,-12f), 4.2f),
        };
        for (int i = 0; i < arvs.Length; i++)
            Arvore($"Arvore_{i}", root, arvs[i].p, arvs[i].h, mTronco, mArvore);

        // ── BANCOS ──────────────────────────────────────────────────
        foreach (float x in new float[] { -20f, -8f, 4f, 16f })
            Cubo("Banco_Praca", root, V(x, 0.3f, -39f), V(3f, 0.4f, 0.8f), mBanco);
        foreach (float x in new float[] { 0f, 14f })
            Cubo("Banco_Patio", root, V(x, 0.3f, 5f),   V(3f, 0.4f, 0.8f), mBanco);

        Debug.Log("[Trapiche] Complexo gerado com sucesso em 'Trapiche_Santo_Angelo'.");
        Selection.activeGameObject = root;
        SceneView.FrameLastActiveSceneView();
    }

    // ════════════════════════════════════════════════════════════════
    // DETECÇÃO DE PIPELINE
    // ════════════════════════════════════════════════════════════════

    static string DetectarShader()
    {
        string[] candidatos =
        {
            "Universal Render Pipeline/Lit",  // URP
            "HDRP/Lit",                       // HDRP
            "Standard",                       // Built-in
            "Unlit/Color",                    // Fallback universal
        };
        foreach (var s in candidatos)
            if (Shader.Find(s) != null) return s;
        return "Diffuse";
    }

    // ════════════════════════════════════════════════════════════════
    // CRIAÇÃO DE MATERIAL (compatível com todos os pipelines)
    // ════════════════════════════════════════════════════════════════

    static Material Mat(string shaderNome, string nome, Color cor)
    {
        Shader sh = Shader.Find(shaderNome);
        if (sh == null)
        {
            Debug.LogWarning($"[Trapiche] Shader '{shaderNome}' nao encontrado. Usando Unlit/Color.");
            sh = Shader.Find("Unlit/Color");
        }

        var mat = new Material(sh) { name = nome };

        // Aplica a cor nas propriedades corretas de cada pipeline
        mat.color = cor;
        if (mat.HasProperty("_BaseColor"))   mat.SetColor("_BaseColor",   cor);
        if (mat.HasProperty("_Color"))       mat.SetColor("_Color",       cor);

        // Reduz brilho para aparência mais matte/arquitetônica
        if (mat.HasProperty("_Smoothness"))  mat.SetFloat("_Smoothness",  0.08f);
        if (mat.HasProperty("_Glossiness"))  mat.SetFloat("_Glossiness",  0.08f);
        if (mat.HasProperty("_Metallic"))    mat.SetFloat("_Metallic",    0.00f);

        return mat;
    }

    // ════════════════════════════════════════════════════════════════
    // HELPERS DE GEOMETRIA
    // ════════════════════════════════════════════════════════════════

    static Vector3 V(float x, float y, float z) => new Vector3(x, y, z);

    static GameObject Cubo(string nome, GameObject pai, Vector3 pos,
                            Vector3 escala, Material mat)
    {
        var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.name = nome;
        go.transform.SetParent(pai.transform);
        go.transform.localPosition = pos;
        go.transform.localScale    = escala;
        go.GetComponent<Renderer>().sharedMaterial = mat;
        DestroyImmediate(go.GetComponent<BoxCollider>());
        return go;
    }

    static GameObject Prim(string nome, GameObject pai, Vector3 pos,
                            PrimitiveType tipo, Material mat)
    {
        var go = GameObject.CreatePrimitive(tipo);
        go.name = nome;
        go.transform.SetParent(pai.transform);
        go.transform.localPosition = pos;
        go.GetComponent<Renderer>().sharedMaterial = mat;
        var col = go.GetComponent<Collider>();
        if (col) DestroyImmediate(col);
        return go;
    }

    static void Telhado2Aguas(string nome, GameObject pai, Vector3 centro,
                               float larg, float prof, float alt, Material mat)
    {
        float ang = Mathf.Atan2(alt, larg / 2f) * Mathf.Rad2Deg;
        float hip = Mathf.Sqrt(larg / 2f * larg / 2f + alt * alt);
        for (int s = -1; s <= 1; s += 2)
        {
            var p = Cubo($"{nome}_L{s}", pai,
                         centro + V(s * larg / 4f, alt / 2f, 0),
                         V(hip, 0.25f, prof), mat);
            p.transform.localEulerAngles = V(0f, 0f, -s * ang);
        }
    }

    static void Telhado4Aguas(string nome, GameObject pai, Vector3 centro,
                               float larg, float prof, float alt, Material mat)
    {
        float angFP = Mathf.Atan2(alt, prof / 2f) * Mathf.Rad2Deg;
        float hipFP = Mathf.Sqrt(prof / 2f * prof / 2f + alt * alt);
        for (int s = -1; s <= 1; s += 2)
        {
            var p = Cubo($"{nome}_FT{s}", pai,
                         centro + V(0, alt / 2f, s * prof / 4f),
                         V(larg, 0.25f, hipFP), mat);
            p.transform.localEulerAngles = V(s * angFP, 0f, 0f);
        }
        float angL = Mathf.Atan2(alt, larg / 2f) * Mathf.Rad2Deg;
        float hipL = Mathf.Sqrt(larg / 2f * larg / 2f + alt * alt);
        for (int s = -1; s <= 1; s += 2)
        {
            var p = Cubo($"{nome}_LD{s}", pai,
                         centro + V(s * larg / 4f, alt / 2f, 0),
                         V(hipL, 0.25f, prof), mat);
            p.transform.localEulerAngles = V(0f, 0f, -s * angL);
        }
    }

    static void GalpaoShed(string nome, GameObject pai, Vector3 pos,
                            float comp, float larg, float altBase,
                            int nDentes, Material mParede, Material mTelha)
    {
        Cubo($"{nome}_Corpo", pai, pos + V(0, altBase / 2f, 0),
             V(comp, altBase, larg), mParede);

        float mod = comp / nDentes;
        for (int i = 0; i < nDentes; i++)
        {
            float dx = -comp / 2f + mod / 2f + i * mod;
            var pl = Cubo($"{nome}_Shed{i}", pai,
                          pos + V(dx, altBase + 0.8f, 0),
                          V(mod * 0.88f, 0.25f, larg), mTelha);
            pl.transform.localEulerAngles = V(0f, 0f, 18f);

            Cubo($"{nome}_Vert{i}", pai,
                 pos + V(dx + mod * 0.44f, altBase + 1.1f, 0),
                 V(0.2f, 1.6f, larg), mParede);
        }
    }

    static void Chamine(string nome, GameObject pai, Vector3 pos,
                         float alt, float raio, Material mat)
    {
        var corpo = Prim($"{nome}_Corpo", pai, pos + V(0, alt / 2f, 0),
                         PrimitiveType.Cylinder, mat);
        corpo.transform.localScale = V(raio * 2f, alt / 2f, raio * 2f);

        var bas = Prim($"{nome}_Base", pai, pos + V(0, 1.5f, 0),
                       PrimitiveType.Cylinder, mat);
        bas.transform.localScale = V(raio * 3.6f, 1.5f, raio * 3.6f);

        var topo = Prim($"{nome}_Topo", pai, pos + V(0, alt + 0.4f, 0),
                        PrimitiveType.Cylinder, mat);
        topo.transform.localScale = V(raio * 2.6f, 0.4f, raio * 2.6f);
    }

    static void Palmeira(string nome, GameObject pai, Vector3 pos,
                          Material mTronco, Material mCopa)
    {
        var t = Prim($"{nome}_Tronco", pai, pos + V(0, 6f, 0),
                     PrimitiveType.Cylinder, mTronco);
        t.transform.localScale = V(0.35f, 6f, 0.35f);

        var c = Prim($"{nome}_Copa", pai, pos + V(0, 13f, 0),
                     PrimitiveType.Sphere, mCopa);
        c.transform.localScale = V(3.2f, 1.8f, 3.2f);
    }

    static void Arvore(string nome, GameObject pai, Vector3 pos, float h,
                        Material mTronco, Material mCopa)
    {
        var t = Prim($"{nome}_Tronco", pai, pos + V(0, h * 0.35f, 0),
                     PrimitiveType.Cylinder, mTronco);
        t.transform.localScale = V(0.45f, h * 0.35f, 0.45f);

        var c = Prim($"{nome}_Copa", pai, pos + V(0, h * 0.82f, 0),
                     PrimitiveType.Sphere, mCopa);
        c.transform.localScale = V(h * 0.72f, h * 0.60f, h * 0.72f);
    }

#endif
}
