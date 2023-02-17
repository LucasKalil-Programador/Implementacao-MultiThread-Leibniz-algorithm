# Implementação MultiThread Leibniz algorithm

[![NPM](https://img.shields.io/npm/l/react)](https://github.com/LucasKalil-Programador/Implementacao-MultiThread-Leibniz-algorithm/blob/e26c8530887781cc6300d273c37fa70e11c128f6/LICENCE)

# Sobre o projeto

Esse projeto foi inicialmente idealizado para servir de estudo sobre programação concorrente com múltiplos processos sendo executado ao mesmo tempo e como esses processos se comunicam e juntos resolvem um problema maior.

## Leibniz algorithm

Gottfried Wilhelm Leibniz foi um polímata e filosofo ele foi o responsável por criar uma formula matemática que permitia chegar a uma aproximação para PI.

[Explicação detalhada da formula](https://pt.wikipedia.org/wiki/F%C3%B3rmula_de_Leibniz_para_%CF%80)

![Formula Leibniz para PI!](https://user-images.githubusercontent.com/82661706/219649758-ebf3bb63-e87b-4062-bb02-8c752d86fa13.svg)

Essa formula permite realizar uma iteração infinita e por isso demanda muito poder computacional por a isso foi um ótimo modo de usar o multi processamento paralelo para que essa resolução fosse feita de forma mais performática.

## Interface do software

O software foi feito para ser usado no console como mostra as imagens abaixo.

### Menu

O usuário pode realizar configurações nos parâmetros de execução.

![Menu exemplo](https://user-images.githubusercontent.com/82661706/219707806-cd0c18ff-f7bf-48ef-9651-0b3e51fab860.png)

### Durante a execução

o usuário tem como visualizar o progresso, valor atual e a expectativa de tempo ate o termino da execução.

![Captura de tela_20230217_093104](https://user-images.githubusercontent.com/82661706/219707789-8516d972-ac73-4135-a715-713bfc56adbc.png)

## Testes

### Configuração: 4 Threads limite 10.000.000.000

### Tempo ate finalização 00:59:48

![Captura de tela_20230217_123107](https://user-images.githubusercontent.com/82661706/219707801-b4b0205a-e45f-4b56-aa6d-8c0f66c1b16f.png)

### Configuração: 12 Threads limite 10.000.000.000

### Tempo ate finalização 00:32:48

![Captura de tela_20230217_130434](https://user-images.githubusercontent.com/82661706/219707804-60e94f90-3784-4369-8c39-177d73c94a93.png)

### Conclusão dos testes

Na configuração com 12 threads levou cerca de 54% do tempo do teste com 4 threads mostrando assim que de fato ouve uma melhora considerável. a configuração da maquina testada era ryzen 5 3600 que tem 6 cores e 12 threads caso fosse um processador melhor, o impacto seria ainda mais perceptível.

o valor de PI é aproximadamente 3,14159 26535 89793 ambos os teste chegaram no resultado que se aproxima a 9ª casa decimal caso o software fosse configurado para mais iterações era possível chegar a uma aproximação ainda mais alta.

## Como executar o projeto

Existem diversas formas de execultar esse projeto sendo a mais fácil baixar a [release](https://github.com/LucasKalil-Programador/Implementacao-MultiThread-Leibniz-algorithm/releases) atual do projeto e executar o .exe

Para a segunda forma sera necessário o [.NET SDK 7.0](https://dotnet.microsoft.com/en-us/download) a pois isso baixe o repositório em um arquivo local e execute o comando no prompt de comando

```bash
# estando na pasta local do projeto execulte o comando
dotnet run
```

# Outras informações

Criador: Lucas Guimarães Kalil - desenvolvedor full stack

Linkedin: https://www.linkedin.com/in/lucas-kalil-436a6220a/<br>
Contato: lucas.prokalil2020@outlook.com
