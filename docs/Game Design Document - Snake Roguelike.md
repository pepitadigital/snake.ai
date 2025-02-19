# Snake Roguelike - Documento de Design do Jogo

## Visão Geral
Snake Roguelike é uma reimaginação moderna do clássico jogo da cobrinha, incorporando elementos de roguelike e sistemas de multiplicadores inspirados em Balatro. O jogador controla uma cobra que cresce ao comer, mas com sistemas modernos de progressão, poderes especiais e modificadores de pontuação.

## Mecânicas Principais

### Mecânicas Base (Snake)
- Movimentação em grade (4 direções)
- A cobra cresce ao comer itens
- Colisão com próprio corpo ou paredes causa derrota
- Sistema de pontuação baseado em itens coletados

### Elementos Roguelike
- Runs procedurais com diferentes modificadores
- Sistema de progressão entre runs
- Desbloqueio permanente de novos modificadores
- Sistema de "builds" baseado em combinações de multiplicadores

### Sistema de Poder Inicial (Pre-Run)
- Escolha de 3 poderes ativos antes de iniciar a run
- Poderes são desbloqueados progressivamente com conquistas

#### Poderes Comuns (Início do Jogo)
- Fantasma (chance de atravessar próprio corpo)
- Shrink (menor tamanho inicial, mas mais ágil)
- Veloz (velocidade base aumentada)

#### Poderes Raros (Desbloqueáveis)
- Magnético (área de coleta maior)
- Sortudo (mais chances de multiplicadores raros)
- Vampiro (ganha pontos extras ao quase colidir)

#### Poderes Épicos (Conquistas Difíceis)
- Mutante (pode trocar um poder durante a run)
- Alquimista (multiplicadores duram mais tempo)
- Caçador (detecta multiplicadores raros através de paredes)

### Sistema de Multiplicadores (Durante Run)

#### Multiplicadores por Raridade

##### Comuns (75% chance)
- x2 (dobra pontos por tempo limitado)
- Combo Básico (aumenta multiplicador a cada 5 comidas)
- Mini Chain (mantém multiplicador por 10 segundos)

##### Raros (20% chance)
- x3 (triplica pontos)
- Combo Plus (aumenta multiplicador a cada 3 comidas)
- Long Chain (mantém multiplicador por 20 segundos)
- Perfect (multiplicador extra se não bater nas paredes)

##### Épicos (4% chance)
- x5 (quintuplica pontos)
- Mega Combo (aumenta multiplicador a cada comida)
- Eternal Chain (multiplicador permanente até colisão)
- Double Perfect (dobro de pontos sem bater em nada)

##### Lendários (1% chance)
- Infinity (multiplicador cresce infinitamente até colisão)
- Time Lord (slow motion + multiplicador crescente)
- Gold Rush (transforma todas comidas em douradas)
- Snake God (imunidade temporária + multiplicador massivo)

### Sistema de Sinergias

#### Combinações Poderosas
- Fantasma + Infinity = "Espírito Imortal" (pode atravessar paredes)
- Magnético + Gold Rush = "Rei Midas" (atrai itens dourados de longe)
- Sortudo + Snake God = "Divindade" (aumenta duração de God Mode)
- Vampiro + Perfect = "Dança com a Morte" (multiplicador maior quanto mais perto das paredes)
- Alquimista + Time Lord = "Mestre do Tempo" (slow motion não consome tempo de multiplicador)

#### Sinergias de Power-ups
- Speed Burst + Shield = "Bala Blindada"
- Size Down + Magnet = "Buraco Negro"
- Chain + Perfect = "Precisão Perfeita"

### Power-ups Especiais (Durante Run)

#### Comuns
- Speed Burst (rajada de velocidade)
- Shield (proteção contra colisão)
- Size Down (diminui tamanho)
- Magnet Pulse (atrai itens)

### Interface e Controles

### Controles Mobile
- Touch screen dividida em 4 quadrantes para direções
- Interface minimalista sem botões adicionais
- Foco total na movimentação da cobra

### Interface Principal
- Pontuação atual
- Multiplicador atual
- Poder especial e seu cooldown
- Minimapa (opcional)

## Economia do Jogo

### Recursos
- Scales (moeda principal)
- Crystals (moeda premium)
- Multiplicadores (itens colecionáveis)
- Experiência

### Sistema de Progressão
- Árvore de talentos com diferentes focos:
  - Sobrevivência
  - Pontuação
  - Coleta de recursos
  - Poderes especiais

## Conteúdo

### Arenas
- Clássica (sem obstáculos)
- Labirinto (com paredes)
- Portal (com teletransportes)
- Espelhada (reflexos aumentam dificuldade)

### Sistema de Coleta

#### Comidas Básicas
- Padrão (1 ponto, crescimento normal)
- Dourada (3 pontos, crescimento maior)
- Arco-íris (5 pontos, escolhe efeito)
- Cristal (moeda premium + poder temporário)

#### Frutas Especiais de Movimento
- Flecha (inverte direção da cobra)
- Portal (teleporta para local aleatório seguro)
- Espelho (inverte controles por 5 segundos)
- Divisor (divide cobra em duas partes, mantendo pontos)
- Âncora (reduz velocidade mas aumenta multiplicador)
- Turbina (aumenta velocidade e adiciona rastro de pontos)
- Scissor (corta tamanho pela metade, converte em pontos)

#### Frutas Raras de Transformação
- Hydra (divide em 3 cobras pequenas por 10 segundos)
- Quantum (cria 3 portais interligados temporários)
- Mimética (copia poder de última fruta consumida)
- Paradoxo (permite movimento na direção oposta ao corpo)
- Fusion (une com última cobra que você colidiu, somando pontos)

#### Spawn de Itens Especiais
- Sequência de comidas da mesma cor geram multiplicador
- Padrões específicos de coleta podem invocar itens raros
- Eventos especiais podem transformar todas as comidas
- Sistema de "Fome" aumenta raridade quanto mais tempo sem comer
- Frutas especiais aparecem em locais estratégicos ou após sequências específicas

### Colecionáveis
- Skins para cobra
- Efeitos visuais
- Trilhas
- Bordas de arena

## Monetização

### Modelo Free-to-Play
- Battle Pass sazonal
- Compra de Crystals
- Pacotes de multiplicadores
- Skins premium

### Sistemas de Recompensa
- Login diário
- Missões semanais
- Conquistas permanentes
- Eventos especiais

## Roadmap de Desenvolvimento

### Fase 1 - MVP
- Mecânicas base do Snake
- Sistema básico de multiplicadores
- Interface principal
- Uma arena

### Fase 2 - Expansão
- Sistema completo de progressão
- Multiplicadores especiais
- Arenas adicionais
- Sistema de missões

### Fase 3 - Monetização
- Battle Pass
- Loja in-game
- Skins e cosméticos
- Sistema de eventos

### Fase 4 - Social
- Ranking global
- Sistema de clãs
- Eventos cooperativos
- Desafios diários

## Referências Visuais e Sonoras

### Estilo Visual
- Minimalista mas com efeitos impactantes
- Paleta de cores vibrante
- Efeitos de partículas para poderes
- Interface clean e moderna

### Som e Música
- Música eletrônica adaptativa
- Efeitos sonoros satisfatórios
- Feedback sonoro para multiplicadores
- Temas diferentes por arena