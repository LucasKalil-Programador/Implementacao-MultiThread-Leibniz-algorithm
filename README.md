# Objetivo 

Esse projeto surgiu como treinamento de multi processamento utilizado Async do c#, um programa multi processamento funciona processando uma quantidade grande de dados em várias threads com o objetivo que um trabalho grande seja sub dividido em pequenos e cada threads faça uma parte

# Multi thread

Multi thread pode aumentar a performance geral do software, porem o uso pode causar efeitos inesperados se 2 threads tentarem usar o mesmo recurso em simultâneo, pode ocorrer problemas, por isso na implementação criada para esse projeto é feita de forma que cada thread tenha seus recursos e eles não são divididos entre elas, evitando assim o uso de lock e syncronized 

### Exemplo de divisão da tarefa

De 0 a 1.000.000.000 a execução com 10 threads<br>
thread 1 i = 0 ⇾ 100.000.000<br>
thread 2 i = 100.000.000 ⇾ 200.000.000<br>
thread 3 i = 200.000.000 ⇾ 300.000.000<br>

Por assim vai até o limite especificado

# Numero de thread vs tempo teste

Cada computador possui um número de threads máximo, no caso oque define isso é o cpu usado, o computador nesse teste foi um ryzen 5 3600 com 6 núcleos 12 threads, ou seja, o máximo que o software pode executar em simultâneo é 12 mais do que isso o computador espera uma acabar e agenda a próxima execução não fazendo efeito no desempenho ou até mesmo tendo desempenho pior

- 1º teste Limite: 100.000.000 Threads: 12 Precisão: 100

- 2º teste Limite: 100.000.000 Threads: 8 Precisão: 100

- 3º teste Limite: 100.000.000 Threads: 4 Precisão: 100

- 4º teste Limite: 100.000.000 Threads: 1 Precisão: 100

- 5º teste Limite: 10.000.000.000 Threads: 12 Precisão: 100

- 6º teste Limite: 10.000.000.000 Threads: 6 Precisão: 100

