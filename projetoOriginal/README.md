# TransacaoFinanceira
### Case para refatoração

### Passos a implementar:
### 1. Corrija o que for necessario para resolver os erros de compilação.



##### Erro 1
:warning: *Ao compilar ocorreram os seguintes erros:*

```
Main.cs(15,30): error CS0826: The type of an implicitly typed array cannot be inferred from the initializer. Try specifying array type explicitly
Main.cs(26,22): error CS0411: The type arguments for method `System.Threading.Tasks.Parallel.ForEach<TSource>(System.Collections.Generic.IEnumerable<TSource>, System.Action<TSource>)' cannot be inferred from the usage. Try specifying the type arguments explicitly
Main.cs(72,31): error CS1502: The best overloaded method match for `TransacaoFinanceira.contas_saldo.contas_saldo(int, decimal)' has some invalid arguments
Main.cs(55,16): (Location of the symbol related to previous error)
Main.cs(72,48): error CS1503: Argument `#1' cannot convert `uint' expression to type `int'
Compilation failed: 4 error(s), 0 warnings

[Compilation failed with exit code 1]
```

*Os logs indicam que o compilador não conseguiu determinar implicitamente o tipo de dado de algumas variáveis no momento da compilação. Ele indica que o problema está em alguma variável do tipo 'int', que fica claro ao observar o 3° erro **CS1502: (TransacaoFinanceira.contas_saldo.contas_saldo(int, decimal)** e no 5° erro **CS1503: (cannot convert `uint' expression to type `int')**. No código foi identificado que são variáveis que tratam da informação da **'conta origem ou destino'**.*

*Ao pesquisar sobre o tipo de dado 'int' foi constatado que em C# ele aceita um intervalo de -2.147.483.648 a 2.147.483.647. Dessa forma, a conta '2147483649' estoura o limite de '2147483647' das variáveis definidas explicitamente como int.*

**✅ A solução:** foi alterar o tipo de dado para uma que comporte o tamanho '2147483649'. Foi escolhido o 'long' por questões performaticas e de compatibilidade para uma possível integração com banco de dados, integração com outras bibliotecas e etc.
____

##### Erro 2
:warning: *Ao recompilar ocorreram os seguintes erros (RESUMIDO):*

```
Unhandled Exception:
System.AggregateException: One or more errors occurred. (Index (zero based) must be greater than or equal to zero and less than the size of the argument list.) (Index (zero based) must be greater than or equal to zero and less than the size of the argument list.) ---> System.FormatException: Index (zero based) must be greater than or equal to zero and less than the size of the argument list.

[ERROR] FATAL UNHANDLED EXCEPTION: System.AggregateException: One or more errors occurred. (Index (zero based) must be greater than or equal to zero and less than the size of the argument list.) (Index (zero based) must be greater than or equal to zero and less than the size of the argument list.) ---> System.FormatException: Index (zero based) must be greater than or equal to zero and less than the size of the argument list.
  at System.Text.StringBuilder.AppendFormatHelper (System.IFormatProvider provider, System.String format, System.ParamsArray args) [0x0013d] in <12b418a7818c4ca0893feeaaf67f1e7f>:0 
  at System.String.FormatHelper (System.IFormatProvider provider, System.String format, System.ParamsArray args) [0x00026] in <12b418a7818c4ca0893feeaaf67f1e7f>:0 
  at System.String.Format (System.IFormatProvider provider, System.String format, System.Object arg0, System.Object arg1, System.Object arg2) [0x0000b] in <12b418a7818c4ca0893feeaaf67f1e7f>:0 
  at System.IO.TextWriter.WriteLine (System.String format, System.Object arg0, System.Object arg1, System.Object arg2) [0x00007] in <12b418a7818c4ca0893feeaaf67f1e7f>:0 
  at System.IO.TextWriter+SyncTextWriter.WriteLine (System.String format, System.Object arg0, System.Object arg1, System.Object arg2) [0x00000] in <12b418a7818c4ca0893feeaaf67f1e7f>:0 
  at (wrapper synchronized) System.IO.TextWriter+SyncTextWriter.WriteLine(string,object,object,object)
  at System.Console.WriteLine (System.String format, System.Object arg0, System.Object arg1, System.Object arg2) [0x00000] in <12b418a7818c4ca0893feeaaf67f1e7f>:0 
  at TransacaoFinanceira.executarTransacaoFinanceira.transferir (System.Int32 correlation_id, System.Int64 conta_origem, System.Int64 conta_destino, System.Decimal valor) [0x00079] in <bd2d9875f7654f1f92a6042ed10d72ab>:0 
  at TransacaoFinanceira.Program+<Main>c__AnonStorey0.<>m__0 (<>__AnonType0`5[<correlation_id>__T,<datetime>__T,<conta_origem>__T,<conta_destino>__T,<VALOR>__T] item) [0x00023] in <bd2d9875f7654f1f92a6042ed10d72ab>:0 
  at System.Threading.Tasks.Parallel+<ForEachWorker>c__AnonStorey4`2[TSource,TLocal].<>m__0 (System.Int32 i) [0x00000] in <12b418a7818c4ca0893feeaaf67f1e7f>:0 
  at System.Threading.Tasks.Parallel+<ForWorker>c__AnonStorey2`1[TLocal].<>m__1 (System.Threading.Tasks.RangeWorker& currentWorker, System.Int32 timeout, System.Boolean& replicationDelegateYieldedBeforeCompletion) [0x00103] in <12b418a7818c4ca0893feeaaf67f1e7f>:0 
--- End of stack trace from previous location where exception was thrown ---

[Execution complete with exit code 1]
```

*Os logs indicam que o compilador do C# assim como várias linguagens usam o Index (zero based), dessa forma o primeiro elemento se inicia em 0. É possível observar nos logs que há um erro no **System.Console.WriteLine (System.String format, System.Object arg0, System.Object arg1, System.Object arg2)** que possui 3 indices*
*No código é possível observar que o **Console.WriteLine** é chamado na seguinte linha abaixo, passando 3 indices com valores armazenados:*
```
Console.WriteLine("Transacao numero {0} foi efetivada com sucesso! Novos saldos: Conta Origem:{1} | Conta Destino: {3}", correlation_id, conta_saldo_origem.saldo, conta_saldo_destino.saldo);
```

**✅ A solução:** Foi alterar o **'Conta Destino: {3}'** para **'Conta Destino: {2}'** que é o indice correto.

#### Agora o programa está compilando! ✅

___


### 2. Execute o programa para avaliar a saida, identifique e corrija o motivo de algumas transacoes estarem sendo canceladas mesmo com saldo positivo e outras sem saldo sendo efetivadas.

*Ao executar o programa é observado claramente que há concorrência de theads, a cada execução é obtido um resultado diferente:*

```
Transacao numero 2 foi cancelada por falta de saldo
Transacao numero 5 foi cancelada por falta de saldo
Transacao numero 3 foi efetivada com sucesso! Novos saldos: Conta Origem:100 | Conta Destino: 1578
Transacao numero 4 foi cancelada por falta de saldo
Transacao numero 6 foi efetivada com sucesso! Novos saldos: Conta Origem:738 | Conta Destino: 1249
Transacao numero 1 foi efetivada com sucesso! Novos saldos: Conta Origem:30 | Conta Destino: 150
Transacao numero 7 foi cancelada por falta de saldo
Transacao numero 8 foi efetivada com sucesso! Novos saldos: Conta Origem:588 | Conta Destino: 5050

[Execution complete with exit code 0]
```

```
Transacao numero 5 foi cancelada por falta de saldo
Transacao numero 6 foi efetivada com sucesso! Novos saldos: Conta Origem:738 | Conta Destino: 1249
Transacao numero 3 foi efetivada com sucesso! Novos saldos: Conta Origem:100 | Conta Destino: 1578
Transacao numero 1 foi efetivada com sucesso! Novos saldos: Conta Origem:30 | Conta Destino: 150
Transacao numero 2 foi efetivada com sucesso! Novos saldos: Conta Origem:1 | Conta Destino: 159
Transacao numero 4 foi cancelada por falta de saldo
Transacao numero 7 foi cancelada por falta de saldo
Transacao numero 8 foi efetivada com sucesso! Novos saldos: Conta Origem:588 | Conta Destino: 5050

[Execution complete with exit code 0]
```

*Ao observar o código, é possível prever que é esperado que seja realizada a execução na sequência descrita das transações:*
```
{correlation_id= 1,datetime="09/09/2023 14:15:00", conta_origem= 938485762L, conta_destino= 2147483649L, VALOR= 150}
{correlation_id= 2,datetime="09/09/2023 14:15:05", conta_origem= 2147483649L, conta_destino= 210385733L, VALOR= 149}
{correlation_id= 3,datetime="09/09/2023 14:15:29", conta_origem= 347586970L, conta_destino= 238596054L, VALOR= 1100}
{correlation_id= 4,datetime="09/09/2023 14:17:00", conta_origem= 675869708L, conta_destino= 210385733L, VALOR= 5300}
{correlation_id= 5,datetime="09/09/2023 14:18:00", conta_origem= 238596054L, conta_destino= 674038564L, VALOR= 1489}
{correlation_id= 6,datetime="09/09/2023 14:18:20", conta_origem= 573659065L, conta_destino= 563856300L, VALOR= 49}
{correlation_id= 7,datetime="09/09/2023 14:19:00", conta_origem= 938485762L, conta_destino= 2147483649L, VALOR= 44}
{correlation_id= 8,datetime="09/09/2023 14:19:01", conta_origem= 573659065L, conta_destino= 675869708L, VALOR= 150}
```

**✅ A solução:** 
1- No código é possível observar a existência do **correlation_id** que identifica cada transação como única, além de ordená-las. Para garantir que as transações sejam executadas na ordem cronológica a variável **TRANSACOES** é ordenada pelo **correlation_id** antes de ser processada.
2- Ao invés de usar a função **Parallel.ForEach** foi implementado **Foreach** padrão para garantir que cada transação seja processada sequencialmente eliminando a concorrência de theads.
3- Por fim foi implementado o objeto de bloqueio **lock** para garantir que o acesso a transferência de saldo seja único.

```
Transacao numero 1 foi efetivada com sucesso! Novos saldos: Conta Origem:30 | Conta Destino: 150
Transacao numero 2 foi efetivada com sucesso! Novos saldos: Conta Origem:1 | Conta Destino: 159
Transacao numero 3 foi efetivada com sucesso! Novos saldos: Conta Origem:100 | Conta Destino: 1578
Transacao numero 4 foi cancelada por falta de saldo
Transacao numero 5 foi efetivada com sucesso! Novos saldos: Conta Origem:89 | Conta Destino: 1889
Transacao numero 6 foi efetivada com sucesso! Novos saldos: Conta Origem:738 | Conta Destino: 1249
Transacao numero 7 foi cancelada por falta de saldo
Transacao numero 8 foi efetivada com sucesso! Novos saldos: Conta Origem:588 | Conta Destino: 5050

[Execution complete with exit code 0]
```
#### Agora as transações estão executando na ordem correta! ✅

___

### 3. Aplique o code review e refatore conforme as melhores praticas(SOLID,Patterns,etc).
#### Segue no README.md do projeto refatorado ✅
___

### 4. Implemente os testes unitários que julgar efetivo.
#### Segue no README.md do projeto refatorado ✅
___

### 5. Crie um git hub e compartilhe o link respondendo o ultimo e-mail.

Obs: Voce é livre para implementar na linguagem de sua preferência desde que respeite as funcionalidades e saídas existentes, além de aplicar os conceitos solicitados.
