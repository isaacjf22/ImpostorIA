# 🕵️ ImpostorIA
> Jogo do Impostor com inteligência artificial rodando no terminal, desenvolvido em **C#**.

---

## 📖 Sobre o Projeto

**ImpostorIA** é uma versão do popular jogo de dedução social **"Jogo do Impostor"**, onde um jogador é secretamente o impostor e não sabe a palavra sorteada.

O diferencial é que a palavra e a dica de cada rodada são geradas pela **IA do Google Gemini**, baseada no tema escolhido pelos jogadores — garantindo palavras variadas, equilibradas e contextualizadas.

---

## 🎮 Como Jogar

### Objetivo
Descobrir quem é o impostor! O impostor não sabe a palavra sorteada e precisa se esconder entre os jogadores usando apenas uma dica de categoria gerada pela IA.

### Fluxo de uma Rodada
```
1. 👥 Cadastre os jogadores (mínimo 3)
2. 🎯 Escolha um tema (ex: "Dragon Ball", "Filmes de Ação", "carros")
3. 🤖 A IA sorteia uma palavra conhecida relacionada ao tema
4. 🕵️ Um jogador é sorteado secretamente como o Impostor
5. 📱 Cada jogador vê sua tela individualmente:
      - Jogador normal → vê a palavra sorteada
      - Impostor       → vê "VOCÊ É O IMPOSTOR!" + uma dica de categoria
6. 🗣️ Os jogadores debatem e tentam descobrir quem é o impostor
7. 🏆 Ao final, o impostor e a palavra são revelados
```

---

## 🤖 Integração com IA

A palavra e a dica de cada rodada são geradas pelo **Google Gemini**, com prompts calibrados para equilibrar o jogo.

### Geração de Palavra
A IA escolhe uma palavra reconhecida no contexto do tema, mas que não seja a mais óbvia. Se o tema for muito amplo, ela afunila para uma subcategoria automaticamente.

| Tema | Palavra gerada (exemplo) |
|------|--------------------------|
| `Dragon Ball` | `Vegeta` |
| `Filmes de Ação` | `John Wick` |
| `carros` | `Corolla` |
| `animes` | `Kakashi` |

### Dica para o Impostor
O impostor recebe uma dica no formato **`[tipo] de [origem]`**, vaga o suficiente para não revelar a palavra, mas útil para ele se passar por alguém que conhece o tema.

| Tema | Palavra | Dica recebida pelo impostor |
|------|---------|-----------------------------|
| `Dragon Ball` | `Kamehameha` | `técnica de Dragon Ball` |
| `Filmes de Ação` | `Exterminador do Futuro` | `personagem de Terminator` |
| `animes` | `Kakashi` | `personagem de Naruto` |
| `carros` | `Corolla` | `modelo de Toyota` |

> A dica **não descreve** o que a palavra é ou faz — apenas indica sua categoria e origem, exigindo que o impostor raciocine para se passar por quem conhece a palavra.

---

## 🛠️ Tecnologias Utilizadas

| Tecnologia | Uso |
|------------|-----|
| **C#** | Linguagem principal do projeto |
| **.NET** | Plataforma de execução |
| **Google Gemini API** | Geração de palavras e dicas via IA |
| **Git & GitHub** | Controle de versão |

### Dependências (NuGet)

| Pacote | Função |
|--------|--------|
| `Google.GenAI` | Integração com a API do Google Gemini |
| `DotNetEnv` | Leitura de variáveis de ambiente do arquivo `.env` |

---

## ▶️ Como Executar

### Pré-requisitos
- .NET instalado
- Conta no [Google AI Studio](https://aistudio.google.com/) para obter a API Key

### Passo a passo

**1. Clone o repositório**
```bash
git clone https://github.com/seu-usuario/ImpostorIA.git
cd ImpostorIA
```

**2. Configure a API Key**

Crie um arquivo `.env` na raiz do projeto baseado no `.env.example`:
```
GOOGLE_API_KEY=sua-chave-aqui
```

> ⚠️ Nunca compartilhe sua API Key. O arquivo `.env` já está no `.gitignore`.

**3. Instale as dependências**
```bash
dotnet restore
```

**4. Rode o projeto**
```bash
dotnet run
```

---

## 📁 Estrutura do Projeto

```
ImpostorIA/
├── ImpostorIA/
│   ├── Program.cs     # Código principal do jogo
│   ├── .env           # API Key (não vai pro GitHub)
│   └── .env.example   # Modelo do .env para novos usuários
├── .gitignore
└── README.md
```

---

## 🧠 Técnicas Aplicadas

- **Programação Assíncrona** — Uso de `async/await` para aguardar a resposta da IA sem travar o programa
- **Integração com API** — Consumo da API do Google Gemini via SDK oficial
- **Engenharia de Prompt** — Prompts calibrados com formato fixo, exemplos concretos e restrições explícitas para controlar o comportamento da IA
- **Variáveis de Ambiente** — API Key protegida com `.env` e `DotNetEnv`
- **Modularização** — Código dividido em métodos específicos (`Rodada`, `GerenciarJogador`, `SorteioImpostor`, etc.)
- **Validação de Entrada** — Loops de proteção contra entradas inválidas
- **Normalização de Strings** — `.Trim()` e `.ToLower()` na comparação de palavras para evitar duplicatas por espaços ou capitalização diferente

---

## 👤 Autor

Desenvolvido por **Isaac José Oliveira Ferreira**

---

## 📄 Licença

Este projeto está sob a licença **MIT**.
