# Snake.AI - MVP (Minimum Viable Product)

## Funcionalidades Essenciais

### 1. Mecânicas Base
- Movimento da cobra em grid (4 direções)
- Colisão com paredes e próprio corpo
- Crescimento ao coletar comida
- Spawn de comida em posições aleatórias válidas

### 2. Sistema de Multiplicadores Base
- Multiplicador básico x2 (comidas douradas)
- Sistema de combo (aumenta pontuação por sequência de coletas)
- Duração limitada dos multiplicadores
- Interface visual para multiplicadores ativos

### 3. Power-ups Iniciais (Pré-Run)
- Apenas 3 poderes básicos para escolha inicial:
  - Fantasma (chance de atravessar próprio corpo)
  - Veloz (velocidade base aumentada)
  - Shrink (menor tamanho inicial)

### 4. Interface MVP
- Score atual
- High score
- Multiplicador ativo
- Tela de game over com pontuação
- Menu de seleção de poder inicial

### 5. Controles Mobile
- Toque em 4 quadrantes para direções
- Interface minimalista
- Resposta tátil ao toque

## Arquitetura MVP

### Classes Essenciais

```csharp
// Core
- SnakeController.cs (movimento e colisão)
- GridSystem.cs (gerenciamento do grid)
- Food.cs (spawn e coleta)

// Managers
- GameManager.cs (fluxo do jogo)
- ScoreManager.cs (pontuação e multiplicadores)
- PowerUpManager.cs (poderes ativos)

// UI
- UIManager.cs (interface básica)
- GameOverScreen.cs
- PowerSelectionScreen.cs

// Data
- PowerUp.cs (scriptable object)
- GameState.cs (enum)
```

## Fluxo do Jogo (MVP)

1. Tela Inicial
   - Botão de start
   - High score

2. Seleção de Poder
   - 3 opções básicas
   - Descrição simples
   - Confirmação

3. Gameplay
   - Grid 20x30
   - Velocidade base constante
   - Spawn de comida normal (80%)
   - Spawn de comida dourada (20%)

4. Game Over
   - Pontuação final
   - High score
   - Restart
   - Menu principal

## Métricas Iniciais

### Grid
- Tamanho: 20x30 células
- Célula: 32x32 pixels
- Borda: 1 célula de espessura

### Gameplay
- Velocidade base: 8 células/segundo
- Crescimento: 1 segmento por comida
- Multiplicador x2: 10 segundos de duração
- Combo: +10% por comida em sequência

### Power-ups
- Fantasma: 20% de chance de atravessar
- Veloz: +20% velocidade base
- Shrink: -30% tamanho inicial

## Assets Necessários (MVP)

### Sprites
- Cabeça da cobra (4 direções)
- Segmento do corpo
- Comida normal
- Comida dourada
- Background do grid
- Elementos de UI básicos

### Sons
- Coleta de comida
- Colisão
- Ativação de multiplicador
- Game over

## Cronograma MVP

### Semana 1
- Setup do projeto
- Implementação do grid
- Movimento básico da cobra
- Sistema de colisão

### Semana 2
- Sistema de comida
- Power-ups básicos
- UI essencial
- Sistema de pontuação

### Semana 3
- Multiplicadores
- Menu de seleção
- Tela de game over
- Polish inicial

### Semana 4
- Testes e ajustes
- Otimização mobile
- Bug fixes
- Preparação para release